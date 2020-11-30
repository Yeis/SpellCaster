using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction {
    public static readonly Vector2 Up = Vector2.up;
    public static readonly Vector2 Down = Vector2.down;
    public static readonly Vector2 Left = Vector2.left;
    public static readonly Vector2 Right = Vector2.right;
    public static readonly Vector2 ForwardLeft = new Vector2(-1, 1);
    public static readonly Vector2 ForwardRight = new Vector2(1, 1);
    public static readonly Vector2 BackwardLeft = new Vector2(-1, -1);
    public static readonly Vector2 BackwardRight = new Vector2(1, -1);
}
