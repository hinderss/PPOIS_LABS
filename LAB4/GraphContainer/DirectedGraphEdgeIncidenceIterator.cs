namespace GraphContainer
{
    public class DirectedGraphEdgeIncidenceIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentNodeIndex;
        private int currentEdgeIndex;

        public DirectedGraphEdgeIncidenceIterator(DirectedGraph<T> graph, T node)
        {
            this.graph = graph;
            currentNodeIndex = graph.GetNodeIndex(node);
            currentEdgeIndex = -1;
        }

        public bool MoveNext()
        {
            if (currentNodeIndex < 0 || currentNodeIndex >= graph.NodeCount)
            {
                throw new InvalidOperationException("Iterator is positioned before the first element or after the last element.");
            }

            currentEdgeIndex++;
            while (currentEdgeIndex < graph.NodeCount)
            {
                if (graph.ContainsEdge(graph.GetNodeByIndex(currentNodeIndex), graph.GetNodeByIndex(currentEdgeIndex)))
                {
                    return true;
                }
                currentEdgeIndex++;
            }
            return false;
        }

        public bool MovePrev()
        {
            if (currentEdgeIndex > 0)
            {
                currentEdgeIndex--;
                return true;
            }
            return false;
        }

        public (T, T) Current
        {
            get
            {
                if (currentNodeIndex >= 0 && currentNodeIndex < graph.NodeCount && currentEdgeIndex >= 0 && currentEdgeIndex < graph.NodeCount)
                {
                    return (graph.GetNodeByIndex(currentNodeIndex), graph.GetNodeByIndex(currentEdgeIndex));
                }
                throw new InvalidOperationException("Iterator is positioned before the first element or after the last element.");
            }
        }
    }
}
