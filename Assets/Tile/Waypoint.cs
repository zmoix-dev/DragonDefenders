using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable {
        get {
            return isPlaceable;
        }
    }

    GameObject structuresParent;

    void Start() {
        structuresParent = GameObject.FindWithTag("StructuresParent");
    }

    void OnMouseDown() {
        if (isPlaceable) {
            GameObject placed = Instantiate(tower, transform.position, Quaternion.identity);
            placed.transform.parent = structuresParent.transform;
            isPlaceable = false;
        }
    }

    bool GetIsPlaceable() {
        return isPlaceable;
    }
}
