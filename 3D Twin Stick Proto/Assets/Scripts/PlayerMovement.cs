using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject   bulletObject;
    public GameObject   fireBulletObject;
    public GameObject   waterBulletObject;
    public GameObject   earthBulletObject;

    public enum bulletType
    {
        normal,
        fire,
        water,
        earth,
    };

    public bulletType bt;

    private int bulletTypeCount;

    private Rigidbody   m_rigidbody;

    private Vector3     moveInput;
    private float       moveSpeed;

    private int         framesBetweenShots;
    private int         framesPassed;

    // Start is called before the first frame update
    void Start()
    {
        //bt = 0;

        m_rigidbody         = GetComponent<Rigidbody>();

        moveSpeed           = 10f;

        framesBetweenShots  = 15;
        framesPassed        = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jumping();
        Rotating(bt);
    }

    void Movement()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));

        m_rigidbody.velocity = moveInput*moveSpeed;
    }

    void Jumping()
    {
        if(Input.GetAxis("Jump") > 0f)
        {
            m_rigidbody.velocity = new Vector3(0f, 10f, 0f);
        }
    }

    void Rotating( bulletType bt)
    {
        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("RHorizontal"), 0f, -Input.GetAxisRaw("RVertical"));

        if (playerDirection.sqrMagnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

            if (framesPassed > framesBetweenShots)
            {
                framesPassed = 0;

                switch (bt)
                {
                    case bulletType.normal:
                        Instantiate(bulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.fire:
                        Instantiate(fireBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.water:
                        Instantiate(waterBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.earth:
                        Instantiate(earthBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;

                }
            }
        }

        framesPassed++;
    }
}
