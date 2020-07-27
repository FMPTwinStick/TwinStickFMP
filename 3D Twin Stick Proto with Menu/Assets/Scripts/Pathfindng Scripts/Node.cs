using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Node class, used to create a grid for pathfinding:
public class Node
{
    //Private Variables:
    private bool    m_isTraversable;
    private Vector3 m_worldPosition;

    private int m_hCost;
    private int m_gCost;

    private Node m_parentNode;

    //grid position variables allowing the node to keep track of its position in a grid:
    private int m_gridPosX;
    private int m_gridPosY;

    //Constructors:
    public Node(bool isTraversable, Vector3 worldPosition )
    {
        m_isTraversable = isTraversable;
        m_worldPosition = worldPosition;
    }

    public Node( bool isTraversable, Vector3 worldPosition, int gridPosX, int gridPosY )
    {
        m_isTraversable = isTraversable;
        m_worldPosition = worldPosition;
        m_gridPosX      = gridPosX;
        m_gridPosY      = gridPosY;
    }

    //Getter and Setter functions:
    public Vector3 GetPosition()
    {
        return m_worldPosition;
    }

    public bool GetTraversability()
    {
        return m_isTraversable;
    }

    public int GetFCost()
    {
        return (m_gCost + m_hCost);
    }

    public void SetHCost(int hCost )
    {
        m_hCost = hCost;
    }

    public int GetHCost()
    {
        return m_hCost;
    }

    public void SetGCost( int gCost )
    {
        m_gCost = gCost;
    }

    public int GetGCost()
    {
        return m_gCost;
    }

    public int GetGridPosX()
    {
        return m_gridPosX;
    }

    public int GetGridPosY()
    {
        return m_gridPosY;
    }

    public void SetParentNode(Node parent )
    {
        m_parentNode = parent;
    }

    public Node GetParentNode()
    {
        return m_parentNode;
    }
}
