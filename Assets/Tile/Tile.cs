using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TowerHandler tower;
    [SerializeField] bool isPlaceable;

    GridManager gridManager;
    Vector2Int coordinates;
    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
    }
   
    void Start() {
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable) {
                gridManager.BlockPlaceable(coordinates, $"Tile.Start({coordinates.ToString()})");
            }
        }
    }

    void OnMouseDown() {
        Node self = gridManager.GetNode(coordinates);
        if (self != null && self.isPlaceable) {
            bool wasPlaced = tower.PlaceTower(tower, transform.position);
            if (wasPlaced) {
                gridManager.BlockPlaceable(coordinates, "Tile.OnMouseDown");
            }
        }
    }
}
