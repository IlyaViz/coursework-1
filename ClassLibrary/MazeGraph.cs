namespace ClassLibrary
{
    public class MazeGraph
    {
        public List<MazeVertex> vertices = new List<MazeVertex>();

        public MazeGraph(List<MazeVertex> vertices)
        {
            this.vertices = vertices;
        }

        public MazeGraph(List<List<MazeVertex>> vertices)
        {
            foreach (List<MazeVertex> row in vertices)
            {
                foreach(MazeVertex vertex in row)
                {
                    this.vertices.Add(vertex);
                }
            }
        }

        public void AddVertex(MazeVertex vertex)
        {
            vertices.Add(vertex);
        }
    }
}
