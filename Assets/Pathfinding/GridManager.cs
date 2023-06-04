using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] Vector2Int adjustedOrigin;
    [Tooltip("Unity grid size - Should match UnityEditor snap settings")]
    [SerializeField][Range(1,10)] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; }}
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid {
        get { return grid; }
    }

    void Awake() {
        CreateGrid();
    }

    void CreateGrid() {
        
        for (int x = adjustedOrigin.x; x < gridSize.x - adjustedOrigin.x; x++) {
            for (int y = adjustedOrigin.y; y < gridSize.y - adjustedOrigin.y; y++) {
                Vector2Int currCoords = new Vector2Int(x, y);
                Node n = new Node(currCoords, true);
                grid.Add(currCoords, n);
            }
        }
    }

    public void BlockWalkable(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            grid[coordinates].isWalkable = false;
        }
    }

    public void BlockPlaceable(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            grid[coordinates].isPlaceable = false;
        }
    }

    public void ResetNodes() {
        foreach (Vector2Int node in grid.Keys) {
            grid[node].isExplored = false;
            grid[node].isPath = false;
            grid[node].parent = null;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);
        return coordinates;
    }

    public Vector3Int GetPositionFromCoordinates(Vector2Int coordinates) {
        Vector3Int position = new Vector3Int();
        position.x = Mathf.RoundToInt(coordinates.x * unityGridSize);
        position.z = Mathf.RoundToInt(coordinates.y * unityGridSize);
        return position;
    }

    public Node GetNode(Vector2Int coordiantes) {
        if (grid.ContainsKey(coordiantes)) {
            return grid[coordiantes];
        } else {
            return null;
        }
    }

    public bool HasNode(Vector2Int coordinates) {
        return grid.ContainsKey(coordinates);
    }
}
