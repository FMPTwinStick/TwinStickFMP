using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Private Variables:
    private Rigidbody m_rigidbody;

    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising Variables:
        m_rigidbody = GetComponent<Rigidbody>();

        moveSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //Collisions with walls:
    void OnCollisionEnter( Collision collision )
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    //Bullet Movement:
    void Movement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
