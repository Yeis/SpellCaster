using UnityEngine;

public class PathFinding
{
    public Spot[,] Spots;
    public List<Spot> openSet = new List<Spot>();
    public List<Spot> closedSet = new List<Spot>();
    public Spot start;

    public PathFinding(Vector3Int[,] grid, int columns, int rows)
    {
        Spots = new Spot[columns, rows];
    }
}

public class Spot
{
    public int F;
    public int G;
    public int H;

    public Spot()
    {
        F = 0;
        G = 0;
        H = 0;
    }
}