using System.Diagnostics;

namespace ClassLibrary
{
    public static class MazeSolver
    {
        public const int EDGE_COST = 1;

        public static (List<MazeVertex>, TimeSpan, int, int) Solve(MazeGraph graph, MazeVertex start, MazeVertex end, Func<MazeVertex, MazeVertex, double> heuristic)
        {
            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, double> priorityQueue = new PriorityQueue<MazeVertex, double>();
            
            int visitedCounter = 0;
            int maxPriorirtyQueueLength = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

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
                maxPriorirtyQueueLength = priorityQueue.Count > maxPriorirtyQueueLength ? priorityQueue.Count : maxPriorirtyQueueLength;
                
                current = priorityQueue.Dequeue();
                current.isVisited = true;
                
                visitedCounter++;

                if (current.Equals(end))
                {
                    stopwatch.Stop();
                    return (ReconstructPath(parentMap, start, end), stopwatch.Elapsed, visitedCounter, maxPriorirtyQueueLength);
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
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y);
        }

        public static double EuclideanDistance(MazeVertex first, MazeVertex second)
        {
            return Math.Sqrt(Math.Pow(first.x - second.x, 2) + Math.Pow(first.y - second.y, 2));
        }

        public static double DijsktraNoDistance(MazeVertex first, MazeVertex second)
        {
            return 0;
        }
    }
}
