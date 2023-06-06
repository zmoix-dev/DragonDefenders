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
        Explore_AStar(gridManager.GetNode(from));
        return BuildPath();
    }

    void ExploreWorld(Node from) {
        Queue<Node> nodes = new Queue<Node>();
        from.isExplored = true;
        nodes.Enqueue(from);

        while(nodes.Count > 0) {
            if (Explore_BFS(nodes.Dequeue(), nodes)) {
                break;
            }
        }
    }

    bool Explore_BFS(Node start, Queue<Node> nodes) {
        foreach (Vector2Int direction in Direction.Cardinals) {
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

    bool Explore_AStar(Node start) {
        List<WeightedNode> nodes = new List<WeightedNode>();
        Dictionary<Node, int> discoveredNodes = new Dictionary<Node, int>();
        nodes.Add(new WeightedNode(start, 0, 0));
        discoveredNodes.Add(start, 0);
        bool destinationFound = false;
        while(nodes.Count > 0 && !destinationFound) {
            WeightedNode next = nodes[0];
            nodes.RemoveAt(0);
            destinationFound = ExploreNeighbors_AStar(next.Node, nodes, discoveredNodes);
        }
        return false;
    }

    bool ExploreNeighbors_AStar(Node start, List<WeightedNode> nodes, Dictionary<Node, int> discoveredNodes) {
        foreach(Vector2Int direction in Direction.Cardinals) {
            Node n = gridManager.GetNode(start.coordinates + direction);
            
            if (n != null && n.isWalkable) {
                n.isExplored = true;
                if (n.coordinates.Equals(destinationCoordinates)) {
                    n.parent = start;
                    return true;
                }
                int edgeWeight = n.weight;
                int parentPathWeight = discoveredNodes[start];
                int totalPathWeight = edgeWeight + parentPathWeight;
                int heuristicDistance = Mathf.Abs(n.coordinates.x - destinationCoordinates.x) + Mathf.Abs(n.coordinates.y - destinationCoordinates.y);
                int heurWeight = heuristicDistance + totalPathWeight;

                // if node hasn't been explored, or current path is cheaper
                if (discoveredNodes.ContainsKey(n) && discoveredNodes[n] > totalPathWeight) {     
                    discoveredNodes[n] = totalPathWeight;
                    n.parent = start;
                } else if (!discoveredNodes.ContainsKey(n)) {
                    discoveredNodes.Add(n, totalPathWeight);   
                    nodes.Add(new WeightedNode(n, totalPathWeight, heurWeight));      
                    n.parent = start;             
                }
            }
        }
        nodes.Sort(new WeightedNodeComparer());
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
