using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; }}
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; }}
    Node startNode;
    Node destinationNode;
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up };
    
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    public List<Node> CalculatePath() {
        return CalculatePath(startCoordinates);
    }

    public List<Node> CalculatePath(Vector2Int from) {
        gridManager.ResetNodes();
        ExploreWorld(gridManager.GetNode(from));
        return BuildPath();
    }

    void ExploreWorld(Node from) {
        Queue<Node> nodes = new Queue<Node>();
        from.isExplored = true;
        nodes.Enqueue(from);

        while(nodes.Count > 0) {
            if (ExploreNeighbors(nodes.Dequeue(), nodes)) {
                break;
            }
        }
    }

    bool ExploreNeighbors(Node start, Queue<Node> nodes) {
        foreach (Vector2Int direction in directions) {
            Node n = gridManager.GetNode(start.coordinates + direction);
            if (n != null && n.isWalkable && !n.isExplored) {
                n.isExplored = true;
                n.parent = start;
                nodes.Enqueue(n);
                if (n.coordinates == destinationCoordinates) {
                    return true;
                }
            }
        }
        return false;
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

    public bool WillBlockPath(Vector2Int coordinates) {
        if (gridManager.HasNode(coordinates)) {
            Node node = gridManager.GetNode(coordinates);
            bool previousWalkable = node.isWalkable;
            node.isWalkable = false;
            List<Node> newPath = CalculatePath();
            node.isWalkable = previousWalkable;
            if(newPath.Count <= 1) {
                CalculatePath();
                return true;
            }
        }
        return false;
    }

    public void NotifyListeners() {
        BroadcastMessage("GetPath", SendMessageOptions.DontRequireReceiver);
    }
}
