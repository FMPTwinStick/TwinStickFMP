using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBehaviour : MonoBehaviour
{
    //Public Variables:
    public GameObject bulletObject;

    //Private Variables:
    private Ray bulletPath;
    private RaycastHit objectHit;

    private Vector3 viewDirection;
    private float view_Z;
    private float view_X;
    private float rotateSpeed;

    private bool canStartZRotation;
    private bool canStartXRotation;

    private float deltaZ;
    private float deltaX;

    private Vector3 normal;

    // Start is called before the first frame update
    void Start()
    {
        view_Z = 1;
        view_X = 0;
        viewDirection = new Vector3(view_X, 0, view_Z);

        bulletPath = new Ray(transform.position, viewDirection);

        rotateSpeed = 1f;

        canStartZRotation = true;
        canStartXRotation = true;

        deltaZ = -rotateSpeed;
        deltaX = -rotateSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetection();
        ViewRotation();
        normal = viewDirection.normalized;
        Debug.DrawLine(transform.position, transform.position + normal* 10f);
        bulletPath.direction = normal;
    }

    void PlayerDetection()
    {
        if (Physics.Raycast(bulletPath , out objectHit, 10f))
        {
            
            if (objectHit.transform.tag == "Player")
            {
                Instantiate(bulletObject, transform.position + 1.5f * normal, Quaternion.LookRotation(normal, Vector3.up));
            }
        }
    }

    void ViewRotation()
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
    }
}
