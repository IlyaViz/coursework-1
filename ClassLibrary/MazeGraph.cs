namespace ClassLibrary
{
    public class MazeGraph
    {
        public List<MazeVertex> Vertices { get; set; } = new List<MazeVertex>();

        public MazeGraph(List<MazeVertex> vertices)
        {
            Vertices = vertices;
        }

        public MazeGraph(List<List<MazeVertex>> vertices)
        {
            foreach (List<MazeVertex> row in vertices)
            {
                foreach(MazeVertex vertex in row)
                {
                    Vertices.Add(vertex);
                }
            }
        }
    }
}
