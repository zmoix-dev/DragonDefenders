using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TowerHandler tower;
    [SerializeField] bool isPlaceable;
    [SerializeField] int startingWeight;
    [SerializeField] int traversalSpeed;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;
    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
   
    void Start() {
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            gridManager.GetNode(coordinates).weight = startingWeight;
            gridManager.GetNode(coordinates).speed = traversalSpeed;
            if (!isPlaceable) {
                gridManager.BlockPlaceable(coordinates);
            }
        }
    }

    void OnMouseDown() {
        if (!gridManager || !pathfinder) {
            return;
        }
        Node self = gridManager.GetNode(coordinates);
        if (self != null && self.isPlaceable && !pathfinder.WillBlockPath(coordinates)) {
            bool wasPlaced = tower.PlaceTower(tower, transform.position);
            if (wasPlaced) {
                gridManager.BlockPlaceable(coordinates);
                gridManager.BlockWalkable(coordinates);
                pathfinder.NotifyListeners();
            }
        }
    }
}
