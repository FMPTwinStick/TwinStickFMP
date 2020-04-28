using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemyBehaviour : MonoBehaviour
{
    //Public Variables:
    public GameObject bulletObject;
    public Component cannonPivot;
    public LayerMask layerMask;

    //Private Variables:

    //Ray Variables:
    private Ray bulletPath;
    private RaycastHit objectHit;

    private Ray reflectionPath;
    private RaycastHit objectHitReflected;

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
    private float timePassed;

    //Tracking variables:
    private bool isTracking;
    private Vector3 playerPosition;

    //Sniper variables:
    private float maxSniperTime;
    private float currentSniperTime;

    private bool isSniping;

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

        timeBetweenShots = 1f;
        timePassed = 0f;

        maxSniperTime = 1f;
        currentSniperTime = 0f;

        isSniping = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSniping)
        {
            ViewRotation();
        }
        if(isSniping)
        {
            currentSniperTime+= Time.deltaTime;
        }
        PlayerDetection();

        ReRotateTimer();

        InvertRotation();

        Debug.DrawLine(transform.position, transform.position + viewDirection * 30f, Color.red);
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
                if (objectHit.transform.tag == "Player")
                {
                    isSniping = true;
                    if (currentSniperTime > maxSniperTime)
                    {
                        Destroy(objectHit.transform.gameObject);
                        currentSniperTime = 0;
                        isSniping = false;
                    }
                }
                else if (objectHit.transform.tag != "Player")
                {

                    if (currentSniperTime > maxSniperTime)
                    {
                        currentSniperTime = 0;
                        isSniping = false;
                    }
                }
            }
            if (objectHit.transform.tag != "Player")
            {
                isTracking = false;
                reflectionPath = new Ray(objectHit.point, Vector3.Reflect(bulletPath.direction, objectHit.normal));
                Debug.DrawRay(objectHit.point, reflectionPath.direction * 30f, Color.red);

                if (Physics.Raycast(reflectionPath, out objectHitReflected, 30f, layerMask))
                {
                    if (objectHitReflected.transform.tag == "Player")
                    {
                        isSniping = true;
                        if (currentSniperTime > maxSniperTime)
                        {
                            Destroy(objectHitReflected.transform.gameObject);
                            currentSniperTime = 0;
                            isSniping = false;
                        }
                    }
                    else if (objectHitReflected.transform.tag != "Player")
                    {
                        
                        if (currentSniperTime > maxSniperTime)
                        { 
                            currentSniperTime = 0;
                            isSniping = false;
                        }
                    }
                }

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
