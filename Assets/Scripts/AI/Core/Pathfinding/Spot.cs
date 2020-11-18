using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Spot
{
    public int X, Y;
    public int F, G, H;
    public int Height = 0;
    public List<Spot> Neighboors;
    public Spot previous = null;


    public Spot(int x, int y, int height)
    {
        F = 0;
        G = 0;
        H = 0;
        X = x;
        Y = y;
        Neighboors = new List<Spot>();
        Height = height;
    }

    public void AddNeighboors(Spot[,] grid, int x, int y){

        if(x < grid.GetUpperBound(0)) {
            Neighboors.Add(grid[x +1,y]);
        } 
        if(x > 0){
            Neighboors.Add(grid[x-1, y]);
        }
        if (y < grid.GetUpperBound(1))
        {
            Neighboors.Add(grid[x, y + 1]);
        }
        if (y > 0)
        {
            Neighboors.Add(grid[x, y - 1]);
        }

        #region diagonal
        //if (X > 0 && Y > 0)
        //    Neighboors.Add(grid[X - 1, Y - 1]);
        //if (X < Utils.Columns - 1 && Y > 0)
        //    Neighboors.Add(grid[X + 1, Y - 1]);
        //if (X > 0 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X - 1, Y + 1]);
        //if (X < Utils.Columns - 1 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X + 1, Y + 1]);
        #endregion
    }
}