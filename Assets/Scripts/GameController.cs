using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public MapData mapData;
    public Graph graph;
    GraphView graphView;
    int[,] mapCopy;
    public float timeStep = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (mapData != null && graph != null) {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);
            mapCopy = mapData.MakeMap();

            graphView = graph.GetComponent<GraphView>();
            if (graphView != null) {
                graphView.Init(graph);
            } else {
                Debug.Log("No graph view is found");
            }
            ShowColors();
            // if (graph.IsWithinBounds(startx, starty) && graph.IsWithinBounds(goalx, goaly) && pathfinder != null) {
            //     Node startNode = graph.nodes[startx, starty];
            //     Node goalNode = graph.nodes[goalx, goaly];
            //     pathfinder.Init(graph, graphView, startNode, goalNode);
            //     StartCoroutine(pathfinder.SearchRoutine(timeStep));
            // } else {
            //     Debug.LogWarning("GameController Error: start or end nodes are not in bounds");
            // }
            StartCoroutine(GameRoutine(timeStep));
        }
    }

    public IEnumerator GameRoutine(float timeStep = 0.1f) {
        yield return null;
        // update graphCopy
        while (true) {
            for (int r = 0; r < graph.getWidth(); r++) {
                for (int c = 0; c < graph.getHeight(); c++) {
                    Node current = graph.nodes[r, c];
                    int liveNeighbors = current.CountAliveNeighbors();

                    // overpopulation
                    if (current.nodeType == NodeType.alive && liveNeighbors > 3) {
                        // graphCopy.nodes[r, c].nodeType = NodeType.dead;
                        mapCopy[r, c] = (int)NodeType.dead;
                    }
                    // underpopulation
                    if (current.nodeType == NodeType.alive && liveNeighbors < 2) {
                        // graphCopy.nodes[r, c].nodeType = NodeType.dead;
                        mapCopy[r, c] = (int)NodeType.dead;
                    }
                    if (current.nodeType == NodeType.dead && liveNeighbors == 3) {
                        // graphCopy.nodes[r, c].nodeType = NodeType.alive;
                        mapCopy[r, c] = (int)NodeType.alive;
                    }
                }
            }
            Debug.Log("here");

            // copy graphCopy into graph
            // graphCopy.UpdateDeadAndAlive();
            // graph = graphCopy.Copy();


            // put mapCopy into graph
            graph.UpdateMapData(mapCopy);
            // mapData. = mapCopy;
            ShowColors();
            yield return new WaitForSeconds(timeStep);
        }
    }

    public void ShowColors() {
        if (graphView == null) {
            return;
        }

        graphView.ColorNodes(graph.aliveNodes, graphView.aliveColor);
        graphView.ColorNodes(graph.deadNodes, graphView.deadColor);

        // NodeView startNodeView = graphView.nodeViews[start.xIndex, start.yIndex];
        // NodeView goalNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex];

        // if (frontierNodes != null) {
        //     graphView.ColorNodes(frontierNodes.ToList(), frontierColor);
        // }
        // if (exploreNodes != null) {
        //     graphView.ColorNodes(exploreNodes, exploreColor);
        // }
        // if (pathNodes != null) {
        //     graphView.ColorNodes(pathNodes, pathColor);
        // }

        // if (startNodeView != null) {
        //     startNodeView.ColorNode(startColor);
        // } else {
        //     Debug.LogWarning("StartNodeView does not exist");
        // }
        // if (goalNodeView != null) {
        //     goalNodeView.ColorNode(goalColor);
        // } else {
        //     Debug.LogWarning("GoalNodeView does not exist");
        // }
    }
}
