namespace ClassLibrary
{
    public static class MazeSolver
    {
        public const int EDGE_COST = 1;

        public static List<MazeVertex> Solve(MazeGraph graph, MazeVertex start, MazeVertex end, Func<MazeVertex, MazeVertex, double> heuristic)
        {
            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, double> priorityQueue = new PriorityQueue<MazeVertex, double>();

            foreach (MazeVertex vertex in graph.vertices)
            {
                vertex.cost = double.MaxValue;
                vertex.isVisited = false;
            }
            start.cost = 0;

            priorityQueue.Enqueue(start, start.cost);
 
            MazeVertex current;
            while (priorityQueue.Count > 0)
            {
                current = priorityQueue.Dequeue();
                current.isVisited = true;

                if (current.Equals(end))
                {
                    break;
                }

                foreach (MazeVertex neighbour in current.neighbours)
                {
                    if (!neighbour.isVisited)
                    {
                        double neighbourCost = neighbour.cost;
                        double newCost = current.cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.cost = newCost;
                            parentMap[neighbour] = current;
                            priorityQueue.Enqueue(neighbour, neighbour.cost + heuristic(neighbour, end));
                        }
                    }
                }
            }

            return ReconstructPath(parentMap, start, end);
        }

        private static List<MazeVertex> ReconstructPath(Dictionary<MazeVertex, MazeVertex> parentMap, MazeVertex start, MazeVertex end)
        {
            List<MazeVertex> path = new List<MazeVertex>();
            MazeVertex current = end;

            while (current != start)
            {
                path.Add(current);

                try
                {
                    current = parentMap[current];
                }
                catch (KeyNotFoundException)
                {
                    throw new PathNotFoundException();
                }
            }

            path.Add(start);

            path.Reverse();
            return path;
        }

        public static double ManhattanDistance(MazeVertex first, MazeVertex second)
        {
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y);
        }

        public static double EuclideanDistance(MazeVertex first, MazeVertex second)
        {
            return Math.Sqrt(Math.Pow(first.x - second.x, 2) + Math.Pow(first.y - second.y, 2));
        }
    }
}
