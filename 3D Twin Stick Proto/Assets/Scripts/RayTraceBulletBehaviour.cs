using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceBulletBehaviour : MonoBehaviour
{
    //Private Variables:
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5;
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
        Ray bulletPath = new Ray(transform.position, transform.forward);
        RaycastHit objectHit;

        if( Physics.Raycast(bulletPath,out objectHit, moveSpeed * Time.deltaTime +0.5f))
        {
            Vector3 reflectDirection = Vector3.Reflect(bulletPath.direction, objectHit.normal);
            float newRotation = 90f - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
            transform.position = objectHit.point;
            transform.eulerAngles = new Vector3(0, newRotation, 0);
        }
    }
}
