using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private Rigidbody m_rigidbody;

    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();

        moveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //m_rigidbody.velocity = moveSpeed * Vector3.right;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
