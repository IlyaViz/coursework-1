using System.Diagnostics;

namespace ClassLibrary
{
    public static class MazeSolver
    {
        public delegate double HeuristicDistance(MazeVertex first, MazeVertex second);

        public static (List<MazeVertex>, double, TimeSpan, int) Solve(MazeGraph graph, MazeVertex start, MazeVertex end, HeuristicDistance heuristic)
        {
            Stopwatch stopwatch = new Stopwatch();
            int visitedCounter = 0;
            stopwatch.Start();

            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, double> priorityQueue = new PriorityQueue<MazeVertex, double>();
            
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
                    (List<MazeVertex>, double) pathInfo = ReconstructPath(parentMap, start, end);
                    stopwatch.Stop();
                    return (pathInfo.Item1, pathInfo.Item2, stopwatch.Elapsed, visitedCounter);
                }

                foreach (MazeVertex neighbour in current.Neighbours)
                {
                    if (!neighbour.IsVisited)
                    {
                        double neighbourCost = neighbour.Cost;
                        double newCost = current.Cost + EdgeCost(current, neighbour);

                        if (newCost < neighbourCost)
                        {
                            neighbour.Cost = newCost;
                            parentMap[neighbour] = current;
                            priorityQueue.Enqueue(neighbour, neighbour.Cost + heuristic(neighbour, end));
                        }
                    }
                }
            }
            
            throw new PathNotFoundException();
        }

        private static double EdgeCost(MazeVertex first, MazeVertex second)
        {
            if (Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y) == 1)
            {
                return 1;
            }
            return Math.Sqrt(2);
        }

        private static (List<MazeVertex>, double) ReconstructPath(Dictionary<MazeVertex, MazeVertex> parentMap, MazeVertex start, MazeVertex end)
        {
            double pathLength = 0;
            List<MazeVertex> path = new List<MazeVertex>();
            MazeVertex current = end;
            MazeVertex next;

            while (current != start)
            {
                path.Add(current);
                next = parentMap[current];
                pathLength += EdgeCost(current, next);
                current = next;
            }

            path.Add(start);

            path.Reverse();
            return (path, pathLength);
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
