using System.Collections.Generic;

namespace ClassLibrary
{
    public class MazeVertex
    {
        public List<MazeVertex> Neighbours { get; set; } = new List<MazeVertex>();
        public double Cost { get; set; }
        public bool IsVisited { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void AddNeighbour(MazeVertex neighbour)
        {
            Neighbours.Add(neighbour);
        }

        public double GetDistanceToNeighbour(MazeVertex neighbour)
        {
            if (Math.Abs(X - neighbour.X) + Math.Abs(Y - neighbour.Y) == 1)
            {
                return 1;
            }
            return Math.Sqrt(2);
        }
    }
}
