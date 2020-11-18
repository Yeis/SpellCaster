using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Astar
{
    public Spot[,] Spots;
    public Astar(Vector3Int[,] grid, int columns, int rows)
    {
        Spots = new Spot[columns, rows];
    }

    public List<Spot> CreatePath(Vector3Int[,] grid, Vector2Int start, Vector2Int end, int length)
    {
        Spot End = null;
        Spot Start = null;
        var columns = Spots.GetUpperBound(0) + 1;
        var rows = Spots.GetUpperBound(1) + 1;
        Spots = new Spot[columns, rows];

        //Initialize Spots
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j] = new Spot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }

        //Setup Neighboors
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j].AddNeighboors(Spots, i, j);
                if (Spots[i, j].X == start.x && Spots[i, j].Y == start.y)
                {
                    Start = Spots[i, j];
                }
                else if (Spots[i, j].X == end.x && Spots[i, j].Y == end.y)
                {
                    End = Spots[i, j];
                }
            }
        }

        if (!IsValidPath(grid, Start, End)) return null;
        List<Spot> OpenSet = new List<Spot>();
        List<Spot> ClosedSet = new List<Spot>();
        OpenSet.Add(Start);

        while (OpenSet.Count > 0)
        {
            //Find shortest step distance in the direction of your goal within the open set
            int winner = 0;
            for (int i = 0; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].F < OpenSet[winner].F)
                {
                    winner = i;
                }
                else if (OpenSet[i].F == OpenSet[winner].F)//tie breaking for faster routing
                    if (OpenSet[i].H < OpenSet[winner].H)
                        winner = i;
            }

            var current = OpenSet[winner];
            //Found the path, creates and returns path
            if (End != null && OpenSet[winner] == End)
            {
                return BuildPath(length, current);
            }

            OpenSet.Remove(current);
            ClosedSet.Add(current);

            //Finds the next closest step on the grid
            var neighboors = current.Neighboors;
            for (int i = 0; i < neighboors.Count; i++)
            {
                var neighboor = neighboors[i];
                if (!ClosedSet.Contains(neighboor) && neighboor.Height < 1)
                {
                    var tempG = current.G + 1; // gets a temp comparison integer for seeing if a route is shorter than our current path
                    bool newPath = false;

                    if (OpenSet.Contains(neighboor)) //Checks if the neighboor we are checking is within the openset
                    {
                        if (tempG < neighboor.G)//The distance to the end goal from this neighboor is shorter so we need a new path
                        {
                            neighboor.G = tempG;
                            newPath = true;
                        }
                    }
                    else//if its not in openSet or closed set, then it IS a new path and we should add it too openset
                    {
                        neighboor.G = tempG;
                        newPath = true;
                        OpenSet.Add(neighboor);
                    }

                    if (newPath)
                    {
                        neighboor.H = Heuristic(neighboor, End);
                        neighboor.F = neighboor.G + neighboor.H;
                        neighboor.previous = current;
                    }
                }

            }

        }
        return null;
    }

    private int Heuristic(Spot a, Spot b)
    {
        //manhattan
        var dx = Mathf.Abs(a.X - b.X);
        var dy = Mathf.Abs(a.Y - b.Y);
        return 1 * (dx + dy);

    }

    private List<Spot> BuildPath(int length, Spot current)
    {
        List<Spot> Path = new List<Spot>();
        var temp = current;
        Path.Add(temp);
        while (temp.previous != null)
        {
            Path.Add(temp.previous);
            temp = temp.previous;
        }
        //Based on length limit 
        if (length - (Path.Count - 1) < 0)
        {
            Path.RemoveRange(0, (Path.Count - 1) - length);

        }
        return Path;
    }

    private bool IsValidPath(Vector3Int[,] grid, Spot start, Spot end)
    {
        if (end == null || start == null || end.Height >= 1)
        {
            return false;
        }
        return true;
    }
}