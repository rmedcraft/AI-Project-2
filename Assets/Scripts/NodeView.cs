using UnityEngine;

public class NodeView : MonoBehaviour {
    public GameObject tile;
    public float borderSize = 0.15f;
    Node node;
    public void Init(Node node) {
        if (tile != null) {
            // gameObject refers to the NodeView gameObject
            // gameObject is kinda like saying this.something() in every other programming language
            this.node = node;
            tile.name = "Node (" + node.position.x + ", " + node.position.z + ")";
            tile.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f, 1f - borderSize);
        } else {
            Debug.LogWarning("Tile does not exist!");
        }
    }

    void OnMouseDown() {
        NodeType temp = node.nodeType;
        if (temp == NodeType.dead) {
            node.nodeType = NodeType.alive;
        } else {
            node.nodeType = NodeType.dead;
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
