using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] TowerHandler tower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable {
        get {
            return isPlaceable;
        }
    }

    void OnMouseDown() {
        if (isPlaceable) {
            bool isPlaced = tower.PlaceTower(tower, transform.position);
            isPlaceable = !isPlaced;
        }
    }

    bool GetIsPlaceable() {
        return isPlaceable;
    }
}
