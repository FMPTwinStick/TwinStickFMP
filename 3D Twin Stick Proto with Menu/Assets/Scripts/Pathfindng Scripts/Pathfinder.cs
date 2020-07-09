using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //Public variables:
    public Transform player;
    public Transform enemyTank;
    public GameObject pathfindingGrid;

    //Private Variables:
    private Grid m_grid;
    private Node m_startNode;
    private Node m_targetNode;

    List<Vector3> m_path = new List<Vector3>();

    // Start is called before the first frame update
    void Awake()
    {
        m_grid = pathfindingGrid.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(enemyTank.position, player.position);
    }

    public void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        //setting the start and target nodes:
        m_startNode     = m_grid.GetNodeFromPosition(startPosition);
        m_targetNode    = m_grid.GetNodeFromPosition(targetPosition);

        //Creating the open and closed nodes lists/HashSets:
        List<Node> openNodes        = new List<Node>();
        HashSet<Node> closedNodes   = new HashSet<Node>();

        //adding the start node to the open list:
        openNodes.Add(m_startNode);

        //Main pathfinding loop:
        while(openNodes.Count > 0)
        {
            //Creating the current Node and setting it to the node with the lowest fCost in openNodes:
            Node currentNode = openNodes[0];
            for (int i = 1; i < openNodes.Count; i++)
            {
                //if the two nodes FCosts are the same the one with the lowest HCost will be selected:
                if (currentNode.GetFCost() > openNodes[i].GetFCost() || (currentNode.GetFCost() == openNodes[i].GetFCost() && currentNode.GetHCost() > openNodes[i].GetHCost()))
                {
                    currentNode = openNodes[i];
                }
            }

            //Moving the currentNode from the open to the closed list:
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            //Checking the current node against the target node:
            if(currentNode == m_targetNode)
            {
                //Pathfinding is complete
                RetracePath(m_startNode, m_targetNode);
                return;
            }

            //Going through the currentNodes neighbours:
            foreach (Node neighbour in m_grid.GetNeighbouringNodes(currentNode))
            {
                //If the neighbour is not traversable or is already in the closed list, skip to the next neighbour:
                if(!neighbour.GetTraversability() || closedNodes.Contains(neighbour))
                {
                    continue;
                }

                int newNeighbourGCost = currentNode.GetGCost() + GetDistanceBetweenNodes(currentNode, neighbour);

                if (newNeighbourGCost < neighbour.GetGCost() || !openNodes.Contains(neighbour))
                {
                    neighbour.SetGCost(GetDistanceBetweenNodes(neighbour, m_startNode));
                    neighbour.SetHCost(GetDistanceBetweenNodes(neighbour, m_targetNode));
                    neighbour.SetParentNode(currentNode);
                    if(!openNodes.Contains(neighbour))
                    {
                        openNodes.Add(neighbour);
                    }
                }
            }

        }
    }

    private void RetracePath(Node start, Node target )
    {
        Node current = target;
        List<Node> path = new List<Node>();
        while(current != start)
        {
            path.Add(current);
            m_path.Add(current.GetPosition());
            current = current.GetParentNode();
        }

        m_path.Reverse();
        path.Reverse();
        m_grid.SetPath(path);
        
    }

    //Function for getting grid distances between nodes:
    private int GetDistanceBetweenNodes(Node nodeA, Node nodeB )
    {
        //getting the horizontal and vertical distances:
        int horizontalDistance  = nodeA.GetGridPosX() - nodeB.GetGridPosX();
        int verticalDistance    = nodeA.GetGridPosY() - nodeB.GetGridPosY();

        int gridDistance;

        if (horizontalDistance >= verticalDistance)
        {
            gridDistance = (15 * verticalDistance) + 10 * (horizontalDistance - verticalDistance);
        }
        else
        {
            gridDistance = (15 * horizontalDistance) + 10 * (verticalDistance - horizontalDistance);
        }

        return Mathf.Abs(gridDistance);
    }

    public List<Vector3> GetVector3Path()
    {
        return m_path;
    }
}
