namespace GraphContainer
{
    public class DirectedGraphNodeIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentNodeIndex;
        private int previousNodeIndex;

        public DirectedGraphNodeIterator(DirectedGraph<T> graph)
        {
            this.graph = graph;
            currentNodeIndex = -1;
            previousNodeIndex = -1;
        }

        public bool MoveNext()
        {
            previousNodeIndex = currentNodeIndex;
            currentNodeIndex++;
            return currentNodeIndex < graph.NodeCount;
        }

        public bool MovePrev()
        {
            if (currentNodeIndex > 0)
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
