using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShotEnemy : MonoBehaviour
{
    //Public Variables:
    public GameObject bulletObject;
    public Component cannonPivot;
    
    public LayerMask layerMask;

    //Private Variables:

    //Ray Variables:
    private Ray bulletPath;
    private RaycastHit objectHit;

    //ViewDirection Variables:
    private bool isRotating;

    private Vector3 viewDirection;
    private float view_Z;
    private float view_X;
    private float rotateSpeed;

    private float deltaZ;
    private float deltaX;

    //ReRotateVariables:
    private bool startReRotateTimer;
    private float currentRotTimer;
    private float reRotateTimer;

    //InvertRotation variables:
    private float currentInvertRotationTimer;
    private float maxInvertRotationTimer;

    //PlayerDetectionVariables:
    private float timeBetweenShots;
    private float timeBetweenBursts;
    private float timePassed;

    //Tracking variables:
    private bool isTracking;
    private Vector3 playerPosition;

    private int burstCounter;
    private float storedTimeBetweenShots;
    private int shotsInBurst;

    // Start is called before the first frame update
    void Start()
    {
        isRotating = true;

        view_Z = 1;
        view_X = 0;
        viewDirection = new Vector3(view_X, 0, view_Z);

        bulletPath = new Ray(transform.position, viewDirection);

        rotateSpeed = 1f;

        deltaZ = -rotateSpeed;
        deltaX = -rotateSpeed;

        startReRotateTimer = false;

        currentRotTimer = 0f;
        reRotateTimer = 1f;

        currentInvertRotationTimer = 0f;
        maxInvertRotationTimer = Random.Range(0f, 4f);

        isTracking = false;

        layerMask = ~layerMask;

        timeBetweenShots = 0.15f;
        storedTimeBetweenShots = 0.15f;
        timeBetweenBursts = 1f;
        timePassed = 0f;

        burstCounter = 0;
        shotsInBurst = 3;

    }

    // Update is called once per frame
    void Update()
    {
        ViewRotation();

        PlayerDetection();

        ReRotateTimer();

        InvertRotation();

        Debug.DrawLine(transform.position, transform.position + viewDirection * 30f);
    }


    //Player Detection function, checks to see if the ray cast intersects the player and fires a bullet accordingly:
    void PlayerDetection()
    {
        bulletPath.origin = transform.position;

        if (!isTracking)
        {
            bulletPath.direction = viewDirection;
        }
        if (isTracking)
        {
            bulletPath.direction = playerPosition - transform.position;
        }

        //Changes the way the cannon is facing to match:
        cannonPivot.transform.rotation = Quaternion.LookRotation(bulletPath.direction, Vector3.up);

        if (Physics.Raycast(bulletPath, out objectHit, 30f, layerMask))
        {

            if (objectHit.transform.tag == "Player")
            {
                if (timePassed > timeBetweenShots)
                {
                    Instantiate(bulletObject, transform.position + 1.5f * bulletPath.direction, Quaternion.LookRotation(bulletPath.direction, Vector3.up));
                    timePassed = 0f;
                    burstCounter++;
                    timeBetweenShots = storedTimeBetweenShots;

                }

                isTracking = true;
                playerPosition = objectHit.transform.position;

                if(burstCounter >= shotsInBurst)
                {
                    timeBetweenShots = timeBetweenBursts;
                    burstCounter = 0;
                }
            }
            if (objectHit.transform.tag != "Player")
            {
                isTracking = false;
            }
        }

        timePassed += Time.deltaTime;
    }


    //View Rotation function, controls the direction of the ray cast i.e. the way the enemy is looking:
    void ViewRotation()
    {
        if (isRotating)
        {
            //Z:
            if (view_Z <= -1f)
            {
                view_Z = -1f;
                deltaZ = -deltaZ;
            }
            if (view_Z >= 1f)
            {
                view_Z = 1f;
                deltaZ = -deltaZ;
            }

            //X:
            if (view_X <= -1f)
            {
                view_X = -1f;
                deltaX = -deltaX;
            }
            if (view_X >= 1f)
            {
                view_X = 1f;
                deltaX = -deltaX;
            }

            view_X += deltaX * Time.deltaTime;
            view_Z += deltaZ * Time.deltaTime;


            viewDirection.z = view_Z;
            viewDirection.x = view_X;

            viewDirection = viewDirection.normalized;


        }
    }

    void ReRotateTimer()
    {
        if (startReRotateTimer)
        {
            currentRotTimer += Time.deltaTime;

            if (currentRotTimer > reRotateTimer)
            {
                isRotating = true;
                currentRotTimer = 0f;
                startReRotateTimer = false;
            }
        }
    }

    void InvertRotation()
    {
        currentInvertRotationTimer += Time.deltaTime;

        if (currentInvertRotationTimer > maxInvertRotationTimer)
        {
            deltaZ = -deltaZ;
            deltaX = -deltaX;
            currentInvertRotationTimer = 0f;
            maxInvertRotationTimer = Random.Range(0f, 4f);
        }
    }
}

