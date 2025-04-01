using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    // Translates 1's and 0's from MapData.cs to an array of nodes
    public Node[,] nodes; //Array of nodes
    public List<Node> walls = new List<Node>();

    int[,] m_mapData;
    int m_width = -1;
    int m_height = -1;

    public List<Node> aliveNodes = new List<Node>();
    public List<Node> deadNodes = new List<Node>();

    public static readonly Vector2[] allDirections = {
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(1f, -1f),
        new Vector2(0f, 1f),
        new Vector2(0f, -1f),
        new Vector2(-1f, 1f),
        new Vector2(-1f, 0f),
        new Vector2(-1f, -1f),
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    public int getWidth() {
        return m_width;
    }
    public int getHeight() {
        return m_height;
    }

    public void Init(int[,] mapData) {
        m_mapData = mapData;
        m_width = mapData.GetLength(0);
        m_height = mapData.GetLength(1);
        nodes = new Node[m_width, m_height];
        for (int c = 0; c < m_height; c++) {
            for (int r = 0; r < m_width; r++) {
                NodeType nodeType = (NodeType)mapData[r, c];
                Node newNode = new Node(r, c, nodeType);
                nodes[r, c] = newNode;
                newNode.position = new Vector3(r, 0, c);
                Debug.Log("Node (" + newNode.position.x + ", " + newNode.position.z + ")");
            }
        }

        UpdateDeadAndAlive();

        for (int c = 0; c < m_height; c++) {
            for (int r = 0; r < m_width; r++) {
                nodes[r, c].neighbors = GetNeighbors(r, c, nodes);
            }
        }
    }

    public bool IsWithinBounds(int x, int y) {
        return x >= 0 && x < m_width && y >= 0 && y < m_height;
    }

    public List<Node> GetNeighbors(int x, int y, Node[,] nodeArray) {
        List<Node> neighbors = new List<Node>();

        // Debug.Log("Current Node (" + nodeArray[x, y].position.x + ", " + nodeArray[x, y].position.z + ")");

        foreach (Vector2 dir in allDirections) {
            int newX = x + (int)dir.x;
            int newY = y + (int)dir.y;
            if (IsWithinBounds(newX, newY) && nodeArray[newX, newY] != null) {
                neighbors.Add(nodeArray[newX, newY]);
            }
        }

        return neighbors;
    }

    public Graph Copy() {
        Graph copy = new Graph();
        copy.Init(m_mapData);
        return copy;
    }

    public void UpdateDeadAndAlive() {
        aliveNodes.Clear();
        deadNodes.Clear();
        foreach (Node n in nodes) {
            if (n.nodeType == NodeType.alive) {
                aliveNodes.Add(n);
            } else if (n.nodeType == NodeType.dead) {
                deadNodes.Add(n);
            }
            // m_mapData[(int)n.position.x, (int)n.position.y] = (int)n.nodeType;
        }

    }
}