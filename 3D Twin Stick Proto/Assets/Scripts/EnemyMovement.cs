using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Private Variables:
    private float moveSpeed;
    private float moveTimer;
    private float maxDirectionTime;
    private Vector3 moveDirection;
    private Quaternion targetRotation;
    public float maxRotationSpeed = 200.0f;
    public Transform tankBody;

    private Ray movementPath;
    private RaycastHit objectHit;

    private bool isReversing;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1f;
        moveTimer = 0f;
        maxDirectionTime = 7.5f;
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        moveDirection = moveDirection.normalized;

        movementPath = new Ray(transform.position, moveDirection);

        isReversing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReversing)
        {
            transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        }
        if (isReversing)
        {
            transform.position = transform.position - moveDirection * moveSpeed * Time.deltaTime;
        }
        moveTimer += Time.deltaTime;
        if(moveTimer > maxDirectionTime)
        {
            moveTimer = 0f;
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            moveDirection = moveDirection.normalized;
            isReversing = false;
        }
        if (moveDirection.sqrMagnitude > 0f)
        {
            targetRotation = Quaternion.LookRotation(moveDirection);
            tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, maxRotationSpeed * Time.deltaTime);
        }

        //update movement path:
        movementPath.origin = transform.position;
        movementPath.direction = moveDirection;

        if (Physics.Raycast(movementPath, out objectHit, 5f))
        {
            if (objectHit.transform.tag == "Wall")
            {
                isReversing = true;
                
            }
        }

        Debug.DrawRay(transform.position, moveDirection * 5f);
        Debug.Log(isReversing);

    }
}
