using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPathfinding : MonoBehaviour
{
    //Public Variables:
    public Transform tankBody;
    
    

    //Private Variables:
    private float m_moveSpeed;
    private Vector3 m_moveDirection;
    private float m_maxRotationSpeed;

    private Pathfinder m_pathfinder;
    private List<Vector3> m_path;

    // Start is called before the first frame update
    void Start()
    {
        m_moveSpeed = 1f;
        m_pathfinder = GetComponent<Pathfinder>();
        m_maxRotationSpeed = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        m_path = m_pathfinder.GetVector3Path();
        FollowPath();
    }

    private void FollowPath()
    {
        //Getting the direction toward the first node:
        m_moveDirection = new Vector3(m_path[0].x, 0f, m_path[0].z) - new Vector3(transform.position.x, 0f, transform.position.z);
        m_moveDirection = m_moveDirection.normalized;

        //Moving:
        transform.position = transform.position + m_moveDirection * m_moveSpeed * Time.deltaTime;

        //*   //Merlin's rotation code:
        if (m_moveDirection.sqrMagnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(m_moveDirection);
            tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, m_maxRotationSpeed * Time.deltaTime);
        }
        //*
    }
}
