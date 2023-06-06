using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    
    static Vector2Int[] cardinals = { 
        Vector2Int.right, 
        Vector2Int.down, 
        Vector2Int.left, 
        Vector2Int.up 
    };
    public static Vector2Int[] Cardinals { get { return cardinals; }}

    static Vector2Int[] ordinals = { 
        Vector2Int.right, 
        new Vector2Int(1, -1), 
        Vector2Int.down, 
        new Vector2Int(-1, -1), 
        Vector2Int.left, 
        new Vector2Int(-1, 1), 
        Vector2Int.up,
        new Vector2Int(1, 1), 
    };
    public static Vector2Int[] Ordinals { get { return ordinals; }}
}
