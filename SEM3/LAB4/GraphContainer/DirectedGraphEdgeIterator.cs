namespace GraphContainer
{
    public class DirectedGraphEdgeIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentI;
        private int currentJ;
        private int previousI;
        private int previousJ;

        public DirectedGraphEdgeIterator(DirectedGraph<T> graph)
        {
            this.graph = graph;
            currentI = 0;
            currentJ = -1;
            previousI = -1;
            previousJ = -1;
        }

        public bool MoveNext()
        {
            previousI = currentI;
            previousJ = currentJ;
            currentJ++;
            for (; currentI < graph.NodeCount; currentI++)
            {
                var I = graph.GetNodeByIndex(currentI);
                for (; currentJ < graph.NodeCount; currentJ++)
                {
                    var J = graph.GetNodeByIndex(currentJ);
                    if (graph.ContainsEdge(I, J))
                    {
                        return true;
                    }
                }
                if (currentJ >= graph.NodeCount)
                    currentJ = 0;
            }
            return false;
        }

        public bool MovePrev()
        {
            if (currentJ > 0)
            {
                currentJ = previousJ;
                currentI = previousI;
                return true;
            }
            return false;
        }

        public (T, T) Current
        {
            get
            {
                if (currentI >= 0 && currentI < graph.NodeCount && currentJ >= 0 && currentJ < graph.NodeCount)
                {
                    return (graph.GetNodeByIndex(currentI), graph.GetNodeByIndex(currentJ));
                }
                throw new InvalidOperationException("Iterator is positioned before the first element or after the last element.");
            }
        }
    }
}
