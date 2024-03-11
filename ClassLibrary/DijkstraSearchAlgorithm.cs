namespace ClassLibrary
{
    public class DijkstraSearchAlgorithm : SearchAlgorithm
    {
        public DijkstraSearchAlgorithm(MazeGraph graph) : base(graph)
        {
        }

        public override List<MazeVertex> Search(MazeVertex start, MazeVertex end)
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

                if (!current.isVisited)
                {
                    current.isVisited = true;

                    if (current.Equals(end))
                    {
                        break;
                    }

                    foreach (MazeVertex neighbour in current.neighbours)
                    {
                        int neighbourCost = neighbour.cost;
                        int newCost = current.cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.cost = newCost;
                            parentMap.Add(neighbour, current);
                            priorityQueue.Enqueue(neighbour, newCost);
                        }
                    }
                }
            }

            return ReconstructPath(parentMap, start, end);
        }

        private List<MazeVertex> ReconstructPath(Dictionary<MazeVertex, MazeVertex> parentMap, MazeVertex start, MazeVertex end)
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
    }
}
