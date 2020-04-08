using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Private Variables:
    private Rigidbody m_rigidbody;

    private float   moveSpeed;
    private int     maxBounces;
    private int     currentBounces;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising Variables:
        moveSpeed = 20;

        maxBounces = 100;
        currentBounces = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //Collisions with walls:
    void OnCollisionEnter( Collision collision )
    {
        if (collision.gameObject.tag == "VerticalWall")
        {
            if (maxBounces >= currentBounces)
            {
                currentBounces++;
                transform.rotation = Quaternion.Euler(transform.rotation.x,(360f-transform.rotation.y*(180f/Mathf.PI)),transform.rotation.z);
            }

            if (maxBounces <= currentBounces)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "HorizontalWall")
        {
            if (maxBounces >= currentBounces)
            {
                Destroy(gameObject);
            }

            if (maxBounces <= currentBounces)
            {
                Destroy(gameObject);
            }
        }
    }

    //Bullet Movement:
    void Movement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
