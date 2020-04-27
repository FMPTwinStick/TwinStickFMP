using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerFireBehaviour : MonoBehaviour
{
    //Private Variables:
    private Vector3 maxScale;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        maxScale = new Vector3(1f,1f,1f);
        moveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, maxScale, 0.5f * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        moveSpeed = Mathf.Lerp(moveSpeed, 0f, 0.99f * Time.deltaTime);

        if(moveSpeed < 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter( Collision collision )
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }

       
    }
}
