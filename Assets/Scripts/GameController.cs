using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour {
    public MapData mapData;
    public Graph graph;
    GraphView graphView;
    int[,] mapCopy;
    float timeStep = 0.25f;
    bool paused = true;
    TextAsset mapText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (mapData != null && graph != null) {
            int[,] mapInstance = mapData.MakeMap();
            graph.Init(mapInstance);
            mapCopy = mapData.MakeMap();

            graphView = graph.GetComponent<GraphView>();
            if (graphView != null) {
                graphView.Init(graph, this);
            } else {
                Debug.Log("No graph view is found");
            }
            ShowColors();
            StartCoroutine(GameRoutine());
        }
    }
    public IEnumerator GameRoutine() {
        yield return null;
        while (true) {

            // stop the game while paused
            while (paused) {
                // foreach loop to update mapCopy when a node is clicked
                foreach (Node n in graph.nodes) {
                    mapCopy[(int)n.position.x, (int)n.position.z] = (int)n.nodeType;
                }

                yield return new WaitForSeconds(timeStep);
            }

            // standard game loop
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
            // put mapCopy into graph
            graph.UpdateMapData(mapCopy);
            ShowColors();

            yield return new WaitForSeconds(timeStep);
        }
    }

    public void Clear() {
        foreach (Node n in graph.nodes) {
            n.nodeType = NodeType.dead;
        }
        ShowColors();
    }
    public void ShowColors() {
        if (graphView == null || graph == null) {
            return;
        }

        // updates the graph.aliveNodes and graph.deadNodes lists
        graph.UpdateDeadAndAlive();

        // color dead and alive nodes
        graphView.ColorNodes(graph.aliveNodes, graphView.nodeViews[0, 0].aliveColor);
        graphView.ColorNodes(graph.deadNodes, graphView.nodeViews[0, 0].deadColor);
    }

    public void SetPaused(bool paused) {
        this.paused = paused;
    }
    public bool GetPaused() {
        return paused;
    }

    public void ApplyPreset(Preset p) {
        List<string> preset = new List<string>();

        // sets the value of the preset list based on the value from the dropdown
        if (p == Preset.Glider) {
            preset = new List<string>{  "000000000000",
                                        "000100000100",
                                        "010100010100",
                                        "001100001100",
                                        "000000000000",
                                        "000000000000",
                                        "000000000000"};
        } else if (p == Preset.Blinkers) {
            preset = new List<string>{  "000000000000",
                                        "011101110000",
                                        "000000000010",
                                        "000000000010",
                                        "000000000010",
                                        "011101110000",
                                        "000000000000"};
        } else if (p == Preset.Checkerboard) {
            preset = new List<string>{  "101010101010",
                                        "010101010101",
                                        "101010101010",
                                        "010101010101",
                                        "101010101010",
                                        "010101010101",
                                        "101010101010"};
        } else if (p == Preset.Random) {
            // fills the board with random values
            for (int r = 0; r < 7; r++) {
                string temp = "";
                for (int c = 0; c < 12; c++) {
                    temp += Random.Range(0, 2);
                }
                preset.Add(temp);
            }
        }

        // updates the nodetypes of every node in the graph
        for (int r = 0; r < preset.Count; r++) {
            for (int c = 0; c < preset[0].Length; c++) {
                int nodeType = preset[r][c] - '0'; // subtracting the char '0' converts a char to an int
                graph.nodes[c, r].nodeType = (NodeType)nodeType;
            }
        }

        // show the changed colors
        ShowColors();
    }
}
