using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TowerHandler tower;
    [SerializeField] bool isPlaceable = true;
    [SerializeField] bool isWalkable = true;
    [SerializeField] int startingWeight;
    [SerializeField] float timeToTraverse;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;
    ParticleSystem hoverIndicator;
    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        hoverIndicator = GetComponentInChildren<ParticleSystem>();
    }
   
    void Start() {
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            gridManager.SetNodeWeight(coordinates, startingWeight);
            gridManager.SetNodeTraversalTime(coordinates, timeToTraverse);
            if (!isPlaceable) {
                gridManager.BlockPlaceable(coordinates);
            }
            if (!isWalkable) {
                gridManager.BlockWalkable(coordinates);
            }
        }
    }

    void OnMouseDown() {
        
        hoverIndicator.Play();
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

    void OnMouseOver() {
        if(isPlaceable) {
            hoverIndicator.Play();
        }
    }

    void OnMouseExit() {
        if (hoverIndicator.isEmitting) {
            hoverIndicator.Stop();
        }
    }
}
