using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementCombination : MonoBehaviour
{
    //Public Variables:
    public bool isPathfinding;
    public float maxRotationSpeed = 200.0f;
    public Transform tankBody;

    //Private Variables:
    private float moveSpeed;
    private float moveTimer;
    private float maxDirectionTime;
    private Vector3 moveDirection;
    private Quaternion targetRotation;
    

    private int m_minimumDistanceForPathfinding;

    // Ray variables for wall detection:
    private Ray movementPath;
    private RaycastHit objectHit;

    private bool isReversing;

    private Pathfinder m_pathfinder;
    private List<Vector3> m_path;


    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
       

        moveSpeed = 1f;
        moveTimer = 0f;
        maxDirectionTime = 7.5f;
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        moveDirection = moveDirection.normalized;

        movementPath = new Ray(transform.position, moveDirection);

        isReversing = false;

        m_pathfinder = GetComponent<Pathfinder>();

        m_minimumDistanceForPathfinding = 4;

        isPathfinding = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the current path:
        m_path = m_pathfinder.GetVector3Path();

        Movement();

    }

    //Movement controls the basic random movement of the enemy tank:
    void Movement()
    {
        
        if (m_path.Count > m_minimumDistanceForPathfinding)
        {
            isPathfinding = true;
            FollowPath();
            Debug.Log(m_path.Count);
        }
        else
        {
            isPathfinding = false;

            //Checks to see if the tank is reversing or not, changes direction accordingly:
            if (!isReversing)
            {
                transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
            }
            if (isReversing)
            {
                transform.position = transform.position - moveDirection * moveSpeed * Time.deltaTime;
            }
            //Updating the move timer:
            moveTimer += Time.deltaTime;

            //If the move timer exceeds the max direction time, a new random direction with a random duration is generated:
            if (moveTimer > maxDirectionTime)
            {
                moveTimer = 0f;
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                moveDirection = moveDirection.normalized;
                isReversing = false;
            }

            //*   //Merlin's rotation code:
            if (moveDirection.sqrMagnitude > 0f)
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
                tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, maxRotationSpeed * Time.deltaTime);
            }
            //*

            WallDetection();
        }
    }

    void WallDetection()
    {
        //update movement path:
        movementPath.origin = transform.position;
        movementPath.direction = moveDirection;

        //If the movementPath intersects a wall start reversing:
        if (Physics.Raycast(movementPath, out objectHit, 2f))
        {
            if (objectHit.transform.tag == "Wall")
            {
                movementPath.direction = -moveDirection;
                moveDirection = -moveDirection;
            }
        }

        //Drawing the movement path for debugging:
        Debug.DrawRay(transform.position, moveDirection * 2f);

    }

    private void FollowPath()
    {
         //Getting the direction toward the first node:
         moveDirection = new Vector3(m_path[0].x, 0f, m_path[0].z) - new Vector3(transform.position.x, 0f, transform.position.z);
         moveDirection = moveDirection.normalized;

         //Moving:
         transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;

         //*   //Merlin's rotation code:
         if (moveDirection.sqrMagnitude > 0f)
         {
             Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
             tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, maxRotationSpeed * Time.deltaTime);
         }
    }
}
