namespace GraphContainer
{
    public class DirectedGraphNodeAdjacencyIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentNodeIndex;
        private int neighborIndex;

        public DirectedGraphNodeAdjacencyIterator(DirectedGraph<T> graph, T node)
        {
            this.graph = graph;
            currentNodeIndex = graph.GetNodeIndex(node);
            neighborIndex = -1;
        }

        public bool MoveNext()
        {
            while (++neighborIndex < graph.NodeCount)
            {
                if (graph.ContainsEdge(graph.GetNodeByIndex(currentNodeIndex), graph.GetNodeByIndex(neighborIndex)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool MovePrev()
        {
            while (--neighborIndex >= 0)
            {
                if (graph.ContainsEdge(graph.GetNodeByIndex(currentNodeIndex), graph.GetNodeByIndex(neighborIndex)))
                {
                    return true;
                }
            }
            return false;
        }

        public T Current
        {
            get
            {
                if (neighborIndex >= 0 && neighborIndex < graph.NodeCount)
                {
                    return graph.GetNodeByIndex(neighborIndex);
                }
                throw new InvalidOperationException("Iterator is positioned before the first element or after the last element.");
            }
        }
    }
}
