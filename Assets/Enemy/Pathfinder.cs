using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
    Node startNode;
    Node destinationNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up };
    
    GridManager gridManager;
    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        startNode = new Node(startCoordinates, true);
        destinationNode = new Node(destinationCoordinates, true);
    }

    void Start() {
        ExploreWorld();
        BuildPath();
    }

    void ExploreWorld() {
        Queue<Node> nodes = new Queue<Node>();
        nodes.Enqueue(startNode);
       
        while(nodes.Count > 0) {
            ExploreNeighbors(nodes.Dequeue(), nodes);
        }

    }

    void ExploreNeighbors(Node start, Queue<Node> nodes) {
        foreach (Vector2Int direction in directions) {
            Node n = gridManager.GetNode(start.coordinates + direction);
            if (n != null && n.isWalkable && !n.isExplored) {
                n.isExplored = true;
                n.parent = start;
                nodes.Enqueue(n);
            }
        }
    }

    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        if (gridManager.HasNode(destinationNode.coordinates)) {
            Node currentPath = gridManager.GetNode(destinationNode.coordinates);
            while(currentPath.parent != null) {
                currentPath.isPath = true;
                path.Add(currentPath);
                currentPath = currentPath.parent;
            }
        } else {
            Debug.Log("Explored grid did not contain destination.");
        }
        path.Reverse();
        return path;
    }
}
