namespace ClassLibrary
{
    public class MazeVertex
    {
        private List<MazeVertex> neighbours = new List<MazeVertex>();
        public List<MazeVertex> Neighbours
        {
            get { return neighbours; }
        }
        public int Cost { get; set; }
        public bool IsVisited { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void AddNeighbour(MazeVertex neighbour)
        {
            Neighbours.Add(neighbour);
        }
    }
}
