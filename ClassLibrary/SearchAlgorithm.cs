namespace ClassLibrary
{
    public abstract class SearchAlgorithm
    {
        protected const int EDGE_COST = 1; 
        public MazeGraph Graph{ get; set; }

        public SearchAlgorithm(MazeGraph graph) { 
            Graph = graph;
        }

        public abstract List<MazeVertex> Search(MazeVertex start, MazeVertex end);
    }
}
