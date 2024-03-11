namespace ClassLibrary
{
    public abstract class SearchAlgorithm
    {
        protected const int EDGE_COST = 1;
        public MazeGraph graph;

        public SearchAlgorithm(MazeGraph graph) { 
            this.graph = graph;
        }

        public abstract List<MazeVertex> Search(MazeVertex start, MazeVertex end);
    }
}
