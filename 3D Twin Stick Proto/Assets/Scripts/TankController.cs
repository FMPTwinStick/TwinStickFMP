using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    //Public Game Objects:
    public GameObject bulletObject;
    public Component cannonPivot;

    //Private Class Variables:
    private Vector3 moveInput;
    private float moveSpeed;

    private float timeBetweenShots;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10f;

        timeBetweenShots = 0.15f;
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RightStick();
    }
    //Movement Function:
    void Movement()
    {
        //Getting the input from the controller:
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        transform.position = transform.position + moveInput * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(moveInput, Vector3.up);
    }

    //Rotating Function:
    void RightStick()
    {
        //Getting input from controller:
        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("RHorizontal"), 0f, -Input.GetAxisRaw("RVertical"));

        //Checking for any input at all:
        if (playerDirection.sqrMagnitude > 0f)
        {
            //rotating player:
            cannonPivot.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

            //Shooting:
            if (timePassed > timeBetweenShots)
            {
                timePassed = 0;

                Instantiate(bulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                
            }
        }

        timePassed += Time.deltaTime;
    }

}
