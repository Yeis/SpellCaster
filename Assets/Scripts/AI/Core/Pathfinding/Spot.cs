using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

public class Spot {
    public int X, Y;
    public int F, G, H;
    public int Height = 0;
    public List<Spot> Neighboors;
    public Spot previous = null;


    public Spot(int x, int y, int height) {
        F = 0;
        G = 0;
        H = 0;
        X = x;
        Y = y;
        Neighboors = new List<Spot>();
        Height = height;
    }

    //El orden en que los vecinos son agregados afecta la direccion que tomara el 
    //path al ser creado
    public void AddNeighboors(Spot[,] grid, int x, int y) {
        Random random = new Random();
        var neighboorsConditions = new Dictionary<int,Dictionary<bool, Action>>{
            {0,new Dictionary<bool,Action> {{y < grid.GetUpperBound(1), () => Neighboors.Add(grid[x, y + 1])}}},
            {1,new Dictionary<bool,Action> {{y > 0, () => Neighboors.Add(grid[x, y - 1]) }}},
            {2,new Dictionary<bool,Action> {{x < grid.GetUpperBound(0), () => Neighboors.Add(grid[x + 1, y]) }}},
            {3,new Dictionary<bool,Action> {{ x > 0, () => Neighboors.Add(grid[x - 1, y]) }}}
        };
        IOrderedEnumerable<int> orderedEnumerable = Enumerable.Range(0, neighboorsConditions.Count).OrderBy(z => random.Next());
        foreach (int i in orderedEnumerable) {
            Dictionary<bool, Action> neighboor = neighboorsConditions.Values.ElementAt(i);
            if(neighboor.Keys.ElementAt(0)){
                Action action = neighboor.Values.ElementAt(0);
                action();
            }
        }

        // //Add upper neighboor
        // if (y < grid.GetUpperBound(1)) {
        //     Neighboors.Add(grid[x, y + 1]);
        // }kj
        // //Add lower neighboor
        // if (y > 0) {
        //     Neighboors.Add(grid[x, y - 1]);
        // }
        //Add right neighboor
        // if (x < grid.GetUpperBound(0)) {
        //     Neighboors.Add(grid[x + 1, y]);
        // }
        // //Add left neighboor
        // if (x > 0) {
        //     Neighboors.Add(grid[x - 1, y]);
        // }
        // //Add upper neighboor
        // if (y < grid.GetUpperBound(1)) {
        //     Neighboors.Add(grid[x, y + 1]);
        // }
        // //Add lower neighboor
        // if (y > 0) {
        //     Neighboors.Add(grid[x, y - 1]);
        // }


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