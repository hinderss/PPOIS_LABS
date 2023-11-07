namespace GraphContainer
{
    public class DirectedGraphEdgeReverseIterator<T>
    {
        private DirectedGraph<T> graph;
        private int currentI;
        private int currentJ;
        private int previousI;
        private int previousJ;

        public DirectedGraphEdgeReverseIterator(DirectedGraph<T> graph)
        {
            this.graph = graph;
            currentI = graph.NodeCount - 1;
            currentJ = graph.NodeCount;
            previousI = -1;
            previousJ = -1;
        }

        public bool MoveNext()
        {
            previousI = currentI;
            previousJ = currentJ;
            currentJ--;
            for (; currentI >= 0; currentI--)
            {
                var I = graph.GetNodeByIndex(currentI);
                for (; currentJ >= 0; currentJ--)
                {
                    var J = graph.GetNodeByIndex(currentJ);
                    if (graph.ContainsEdge(I, J))
                    {
                        return true;
                    }
                }
                if (currentJ < 0)
                    currentJ = graph.NodeCount - 1;
            }
            return false;
        }

        public bool MovePrev()
        {
            previousI = currentI;
            previousJ = currentJ;
            currentI++;
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
