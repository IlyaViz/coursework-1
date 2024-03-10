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

            foreach (MazeVertex vertex in Graph.Vertices)
            {
                vertex.Cost = int.MaxValue;
                vertex.IsVisited = false;
            }
            start.Cost = 0;

            priorityQueue.Enqueue(start, start.Cost);

            MazeVertex current;
            while (priorityQueue.Count > 0)
            {
                current = priorityQueue.Dequeue();

                if (!current.IsVisited)
                {
                    current.IsVisited = true;

                    if (current.Equals(end))
                    {
                        break;
                    }

                    foreach (MazeVertex neighbour in current.Neighbours)
                    {
                        int neighbourCost = neighbour.Cost;
                        int newCost = current.Cost + EDGE_COST;

                        if (newCost < neighbourCost)
                        {
                            neighbour.Cost = newCost;
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
