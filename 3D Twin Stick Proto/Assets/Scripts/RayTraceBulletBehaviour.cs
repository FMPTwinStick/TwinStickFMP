using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceBulletBehaviour : MonoBehaviour
{
    //Public Variables:
    

    //Private Variables:
    private float   moveSpeed;

    private Ray bulletPath;
    private RaycastHit objectHit;
    private Vector3 reflectDirection;
    private float newRotation;

    private int currentBounces;
    private int maxBounces;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed       = 5;

        bulletPath      = new Ray(transform.position, transform.forward);

        currentBounces  = 0;
        maxBounces      = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Reflections();
    }

    //Bullet Movement:
    void Movement()
    {
       transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void Reflections()
    {
        bulletPath.origin = transform.position;
        bulletPath.direction = transform.forward;
        
        if( Physics.Raycast(bulletPath,out objectHit, moveSpeed * Time.deltaTime + .1f))
        {
            if (objectHit.collider.gameObject != gameObject)
            {
                if (objectHit.transform.tag == "Bullet")
                {
                    Destroy(objectHit.collider.gameObject);
                    Destroy(gameObject);
                }
                else if (currentBounces < maxBounces)
                {
                    reflectDirection = Vector3.Reflect(bulletPath.direction, objectHit.normal);
                    newRotation = 90f - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
                    transform.position = objectHit.point;
                    transform.eulerAngles = new Vector3(0, newRotation, 0);
                    currentBounces++;
                }
                else if (currentBounces >= maxBounces)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
