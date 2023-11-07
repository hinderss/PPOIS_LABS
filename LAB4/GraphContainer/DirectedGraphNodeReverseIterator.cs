namespace GraphContainer
{
    public class DirectedGraphNodeReverseIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentNodeIndex;
        private int previousNodeIndex;

        public DirectedGraphNodeReverseIterator(DirectedGraph<T> graph)
        {
            this.graph = graph;
            currentNodeIndex = graph.NodeCount;
            previousNodeIndex = -1;
        }

        public bool MoveNext()
        {
            previousNodeIndex = currentNodeIndex;
            currentNodeIndex--;
            return currentNodeIndex >= 0;
        }

        public bool MovePrev()
        {
            if (currentNodeIndex < graph.NodeCount - 1)
            {
                currentNodeIndex = previousNodeIndex;
                return true;
            }
            return false;
        }

        public T Current
        {
            get
            {
                if (currentNodeIndex >= 0 && currentNodeIndex < graph.NodeCount)
                {
                    return graph.GetNodeByIndex(currentNodeIndex);
                }
                throw new InvalidOperationException("Iterator is positioned before the first element or after the last element.");
            }
        }
    }
}
