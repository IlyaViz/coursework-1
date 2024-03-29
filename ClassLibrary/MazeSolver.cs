namespace ClassLibrary
{
    public static class MazeSolver
    {
        public const int EDGE_COST = 1;

        public static List<MazeVertex> Dijkstra(MazeGraph graph, MazeVertex start, MazeVertex end)
        {
            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, int> priorityQueue = new PriorityQueue<MazeVertex, int>();

            foreach (MazeVertex vertex in graph.vertices)
            {
                vertex.cost = int.MaxValue;
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
                        int neighbourCost = neighbour.cost;
                        int newCost = current.cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.cost = newCost;
                            parentMap[neighbour] = current;
                            priorityQueue.Enqueue(neighbour, neighbour.cost);
                        }
                    }

                }
            }

            return ReconstructPath(parentMap, start, end);
        }

        public static List<MazeVertex> AStar(MazeGraph graph, MazeVertex start, MazeVertex end)
        {
            Dictionary<MazeVertex, MazeVertex> parentMap = new Dictionary<MazeVertex, MazeVertex>();
            PriorityQueue<MazeVertex, int> priorityQueue = new PriorityQueue<MazeVertex, int>();

            foreach (MazeVertex vertex in graph.vertices)
            {
                vertex.cost = int.MaxValue;
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
                        int neighbourCost = neighbour.cost;
                        int newCost = current.cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.cost = newCost;
                            parentMap[neighbour] = current;
                            priorityQueue.Enqueue(neighbour, neighbour.cost + Heuristic(neighbour, end));
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

        private static int Heuristic(MazeVertex first, MazeVertex second)
        {
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y);
        }
    }
}
