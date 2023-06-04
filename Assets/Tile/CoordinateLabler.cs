using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color walkableBlockedColor = Color.red;
    [SerializeField] Color structureBlockedColor = Color.black;
    [SerializeField] Color pathColor = Color.blue;
    [SerializeField] Color exploredColor = new Color(1f, 0.5f, 0f);
    TextMeshPro label;

    GridManager gridManager;
    Vector2Int coordinates;


    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
    }

    void Start() {
        if (gridManager) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.parent.position);
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void Update()
    {
        ToggleLabels();
        ColorCoordinates();
    }

    void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates() {
        if (gridManager == null) {
            Debug.Log("Coordinate Labeler could not find Grid Manager.");
            return;
        }
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }

    void ColorCoordinates() {
        if (gridManager == null) {
            return;
        }
        Node n = gridManager.GetNode(coordinates);
        if (n == null) {
            return;
        }
        if (!n.isWalkable) {
            label.color = walkableBlockedColor;
        } else if (n.isPath) {
            label.color = pathColor;
        } else if (!n.isPlaceable) {
            label.color = structureBlockedColor;
        } else if (n.isExplored) {
            label.color = exploredColor;
        } else {
            label.color = defaultColor;
        }
    }
}
