using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isPlaceable;
    public bool isExplored;
    public bool isPath;
    public int weight;
    public float timeToTraverse;
    public Node parent;

    public Node(Vector2Int coordinates, bool isWalkable) {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
        this.isPlaceable = true;
    }

    public override string ToString()
    {
        return $"Node({coordinates.ToString()})";
    }
}
