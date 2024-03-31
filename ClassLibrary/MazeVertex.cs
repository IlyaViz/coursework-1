namespace ClassLibrary
{
    public class MazeVertex
    {
        public List<MazeVertex> neighbours = new List<MazeVertex>();
        public double cost;
        public bool isVisited;
        public int x;
        public int y;

        public void AddNeighbour(MazeVertex neighbour)
        {
            neighbours.Add(neighbour);
        }
    }
}
