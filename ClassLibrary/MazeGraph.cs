namespace ClassLibrary
{
    public class MazeGraph
    {
        public List<MazeVertex> Vertices { get; set; }

        public MazeGraph(List<MazeVertex> vertices)
        {
            Vertices = vertices;
        }
    }
}
