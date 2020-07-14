using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //Public Variables:
   
    public LayerMask untraversable;
    public Vector2 m_worldSize;
    public float m_nodeRadius;
    public bool drawGridInGizmos;

    //Private Variables:
    Node[,] m_grid;

    //width and length variables storing the number of nodes in the x and y direction respectively:
    private int m_length;
    private int m_width;

    private float m_nodeDiameter;
    private Vector3 m_originPosition;

    private List<Node> m_path = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        m_nodeDiameter = m_nodeRadius * 2;

        m_width     = Mathf.RoundToInt(m_worldSize.x / m_nodeDiameter);
        m_length    = Mathf.RoundToInt(m_worldSize.y / m_nodeDiameter);
        
        CreateGrid();
        drawGridInGizmos = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Create Grid:
    void CreateGrid()
    {
        m_grid = new Node[m_width, m_length];

        //Getting the bottom left point of the grid:
        m_originPosition = transform.position - (Vector3.right * 0.5f * m_worldSize.x) - (Vector3.forward * 0.5f * m_worldSize.y);

        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_length; y++)
            {
                //Calculating the position of the node:
                Vector3 position = m_originPosition + Vector3.right * ((x * m_nodeDiameter)+m_nodeRadius) + Vector3.forward * ((y * m_nodeDiameter) + m_nodeRadius);

                //Using CheckSphere to detect untraversable surfaces:
                bool isTraversable = !Physics.CheckSphere(position, m_nodeRadius, untraversable);

                //Creating the nodes in the grid:
                m_grid[x, y] = new Node(isTraversable, position, x, y);
            }
        }
    }

    //Function for getting node in the grid from position:
    public Node GetNodeFromPosition( Vector3 position)
    {
        //calculating the percentage of the grid the position is at in both axis:
        float widthPercentage   = (position.x - m_originPosition.x) / m_worldSize.x;
        float lengthPercentage  = (position.z - m_originPosition.z) / m_worldSize.y;

        //Calculating the corresponding node:

        int xNode = Mathf.FloorToInt(m_width * widthPercentage);
        int yNode = Mathf.FloorToInt(m_length * lengthPercentage);

        return m_grid[xNode,yNode];
    }

    //Get Neighbouring Nodes Function:
    public List<Node> GetNeighbouringNodes( Node centreNode)
    {
        List<Node> neighbouringNodes = new List<Node>();
        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1 ; y++)
            {
                //Preventing the loop from adding the centre node itself:
                if(x==0 && y == 0)
                {
                    continue;
                    
                }
                //Checking the neighbouring node exists:
                else if (m_grid[centreNode.GetGridPosX() + x, centreNode.GetGridPosY() + y] != null)
                {
                    //adding the neigbouring node to the list:
                    neighbouringNodes.Add(m_grid[centreNode.GetGridPosX() + x, centreNode.GetGridPosY() + y]);
                }
            }
        }

        //returning the completed list:
        return neighbouringNodes;
    }

    //Getter and setter for path of nodes:
    public void SetPath(List<Node> path )
    {
        m_path = path;
    }

    public List<Node> GetPath()
    {
        return m_path;
    }

    //Gizmos:
    
    void OnDrawGizmos()
    {
        if (drawGridInGizmos)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(m_worldSize.x, 1, m_worldSize.y));


            if (m_grid[0, 0] != null)
            {
                foreach (Node node in m_grid)
                {
                    Gizmos.color = Color.white;

                    if (!node.GetTraversability())
                    {
                        Gizmos.color = Color.red;
                    }

                    if (m_path != null)
                    {
                        if (m_path.Contains(node))
                        {
                            Gizmos.color = Color.cyan;
                        }

                    }

                    Gizmos.DrawCube(node.GetPosition(), (m_nodeDiameter - 0.05f) * Vector3.one);
                }
            }
        }
    }
}
