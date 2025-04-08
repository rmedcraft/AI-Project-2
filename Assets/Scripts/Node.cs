using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType {
    dead = 0,
    alive = 1
}

public class Node {
    // this is a single node in a graph
    public NodeType nodeType = NodeType.dead;
    public int xIndex = -1;
    public int yIndex = -1;
    public Vector3 position;

    public List<Node> neighbors = new List<Node>();
    public Node prev = null;
    public Node(int xIndex, int yIndex, NodeType nodeType) {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.nodeType = nodeType;
    }

    public void Reset() {
        prev = null;
    }

    public int CountAliveNeighbors() {
        int liveCt = 0;
        foreach (Node n in neighbors) {
            if (n.nodeType == NodeType.alive) {
                liveCt++;
            }
        }
        return liveCt;
    }
}


