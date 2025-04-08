using UnityEngine;

public class NodeView : MonoBehaviour {
    public GameObject tile;
    public float borderSize = 0.15f;

    public Color deadColor = Color.white;
    public Color aliveColor = Color.yellow;
    Node node;
    public GameController gameController;

    public void Init(Node node, GameController gameController) {
        if (tile != null) {
            // gameObject refers to the NodeView gameObject
            // gameObject is kinda like saying this.something() in every other programming language
            this.node = node;
            tile.name = "Node (" + node.position.x + ", " + node.position.z + ")";
            tile.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f, 1f - borderSize);

            // Adds a hitbox to each tile, necessary for click detection
            tile.AddComponent<BoxCollider>();

            this.gameController = gameController;
        } else {
            Debug.LogWarning("Tile does not exist!");
        }
    }

    void Update() {
        // handles click detection for each individual node with a raycaster
        // cant use the OnMouseDown method because we are using tile as the nodeview object.
        // you can only change a node state manually if the game is paused
        if (Input.GetMouseButtonDown(0) && gameController != null && gameController.GetPaused()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // out hit means to store the output in the hit object
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject == tile) {
                    node.nodeType = (node.nodeType == NodeType.dead) ? NodeType.alive : NodeType.dead;
                    ColorNode(node.nodeType == NodeType.dead ? deadColor : aliveColor);
                }
            }
        }
    }

    void ColorNode(Color color, GameObject gameObject) {
        if (gameObject != null) {
            Renderer gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObjectRenderer.material.color = color;
        }
    }

    public void ColorNode(Color color) {
        ColorNode(color, tile);
    }
}
