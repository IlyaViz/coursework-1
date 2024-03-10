namespace ClassLibrary
{
    public static class MazeSolver
    {
        public static List<MazeVertex> FindPath(SearchAlgorithm algorithm, MazeVertex start, MazeVertex end)
        {
            return algorithm.Search(start, end);
        }
    }
}
