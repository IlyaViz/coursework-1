using System.Diagnostics;

namespace ClassLibrary
{
    public static class MazeSolver
    {
        private const int EDGE_COST = 1;
        public delegate double HeuristicDistance(MazeVertex first, MazeVertex second);

        public static (List<MazeVertex>, TimeSpan, int) Solve(MazeGraph graph, MazeVertex start, MazeVertex end, HeuristicDistance heuristic)
        {
            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, double> priorityQueue = new PriorityQueue<MazeVertex, double>();
            
            int visitedCounter = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (MazeVertex vertex in graph.Vertices)
            {
                vertex.Cost = double.MaxValue;
                vertex.IsVisited = false;
            }
            start.Cost = 0;
            
            priorityQueue.Enqueue(start, start.Cost);
 
            MazeVertex current;
            while (priorityQueue.Count > 0)
            {
                current = priorityQueue.Dequeue();
                current.IsVisited = true;
                
                visitedCounter++;

                if (current.Equals(end))
                {
                    stopwatch.Stop();
                    return (ReconstructPath(parentMap, start, end), stopwatch.Elapsed, visitedCounter);
                }

                foreach (MazeVertex neighbour in current.Neighbours)
                {
                    if (!neighbour.IsVisited)
                    {
                        double neighbourCost = neighbour.Cost;
                        double newCost = current.Cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.Cost = newCost;
                            parentMap[neighbour] = current;
                            priorityQueue.Enqueue(neighbour, neighbour.Cost + heuristic(neighbour, end));
                        }
                    }
                }
            }
            stopwatch.Stop();
            
            throw new PathNotFoundException();
        }

        private static List<MazeVertex> ReconstructPath(Dictionary<MazeVertex, MazeVertex> parentMap, MazeVertex start, MazeVertex end)
        {
            List<MazeVertex> path = new List<MazeVertex>();
            MazeVertex current = end;

            while (current != start)
            {
                path.Add(current);
                current = parentMap[current];
            }

            path.Add(start);

            path.Reverse();
            return path;
        }

        public static double ManhattanDistance(MazeVertex first, MazeVertex second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }

        public static double EuclideanDistance(MazeVertex first, MazeVertex second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }

        public static double DijsktraDistance(MazeVertex first, MazeVertex second)
        {
            return 0;
        }
    }
}
