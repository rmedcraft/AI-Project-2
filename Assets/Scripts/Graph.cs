using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    // Translates 1's and 0's from MapData.cs to an array of nodes
    public Node[,] nodes; //Array of nodes
    public List<Node> walls = new List<Node>();

    int[,] mapData;
    int width = -1;
    int height = -1;

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
        return width;
    }
    public int getHeight() {
        return height;
    }

    public void Init(int[,] mapData) {
        this.mapData = mapData;
        width = mapData.GetLength(0);
        height = mapData.GetLength(1);
        nodes = new Node[width, height];
        for (int c = 0; c < height; c++) {
            for (int r = 0; r < width; r++) {
                NodeType nodeType = (NodeType)mapData[r, c];
                Node newNode = new Node(r, c, nodeType);
                nodes[r, c] = newNode;
                newNode.position = new Vector3(r, 0, c);
                Debug.Log("Node (" + newNode.position.x + ", " + newNode.position.z + ")");
            }
        }

        UpdateDeadAndAlive();

        for (int c = 0; c < height; c++) {
            for (int r = 0; r < width; r++) {
                nodes[r, c].neighbors = GetNeighbors(r, c, nodes);
            }
        }
    }

    public void UpdateMapData(int[,] mapData) {
        this.mapData = mapData;
        for (int r = 0; r < width; r++) {
            for (int c = 0; c < height; c++) {
                NodeType nodeType = (NodeType)mapData[r, c];
                nodes[r, c].nodeType = nodeType;
            }
        }
        UpdateDeadAndAlive();
    }

    public bool IsWithinBounds(int x, int y) {
        return x >= 0 && x < width && y >= 0 && y < height;
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

    // public Graph Copy() {
    //     Graph copy = new Graph();
    //     copy.Init(mapData);
    //     return copy;
    // }

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