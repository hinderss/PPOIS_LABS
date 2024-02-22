using System.Collections;

namespace GraphContainer
{
    public class DirectedGraph<T> : ICloneable, IEnumerable<T>
    {
        private List<GraphNode<T>> nodes;
        private int[,] adjacencyMatrix;

        public DirectedGraph()
        {
            nodes = new List<GraphNode<T>>(0);
            adjacencyMatrix = new int[0, 0];
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Empty()
        {
            return nodes.Count == 0;
        }

        public void Clear()
        {
            nodes.Clear();
            adjacencyMatrix = new int[0, 0];
        }

        public int NodeCount
        {
            get { return nodes.Count; }
        }

        public int EdgeCount
        {
            get 
            {
                int count = 0;

                for(int i = 0; i < nodes.Count; i++)
                {
                    for(int j=0; j < nodes.Count; j++)
                    {
                        if (adjacencyMatrix[i, j] == 1)
                            count++;
                    }
                }

                return count;
            }
        }

        public int GetInDegree(T data)
        {
            int nodeIndex = GetNodeIndex(data);

            int inDegree = 0;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (adjacencyMatrix[i, nodeIndex] == 1)
                {
                    inDegree++;
                }
            }

            return inDegree;
        }

        public int GetOutDegree(T data)
        {
            int nodeIndex = GetNodeIndex(data);

            int outDegree = 0;
            for (int j = 0; j < nodes.Count; j++)
            {
                if (adjacencyMatrix[nodeIndex, j] == 1)
                {
                    outDegree++;
                }
            }

            return outDegree;
        }


        public void AddNode(T data)
        {
            nodes.Add(new GraphNode<T>(data));

            int newSize = nodes.Count;
            var newMatrix = new int[newSize, newSize];

            for (int i = 0; i < newSize - 1; i++)
            {
                for (int j = 0; j < newSize - 1; j++)
                {
                    newMatrix[i, j] = adjacencyMatrix[i, j];
                }
            }

            adjacencyMatrix = newMatrix;
        }

        public void AddEdge(T from, T to)
        {
            int fromIndex = GetNodeIndex(from);
            int toIndex = GetNodeIndex(to);

            adjacencyMatrix[fromIndex, toIndex] = 1;
        }

        public void RemoveNode(T data)
        {
            int nodeIndex = GetNodeIndex(data);
            nodes.RemoveAt(nodeIndex);

            int newSize = nodes.Count;
            var newMatrix = new int[newSize, newSize];

            for (int i = 0; i < newSize; i++)
            {
                for (int j = 0; j < newSize; j++)
                {
                    if (i < nodeIndex && j < nodeIndex)
                    {
                        newMatrix[i, j] = adjacencyMatrix[i, j];
                    }
                    else if (i < nodeIndex && j >= nodeIndex)
                    {
                        newMatrix[i, j] = adjacencyMatrix[i, j + 1];
                    }
                    else if (i >= nodeIndex && j < nodeIndex)
                    {
                        newMatrix[i, j] = adjacencyMatrix[i + 1, j];
                    }
                    else if (i >= nodeIndex && j >= nodeIndex)
                    {
                        newMatrix[i, j] = adjacencyMatrix[i + 1, j + 1];
                    }
                }
            }
            adjacencyMatrix = newMatrix;
        }

        public void RemoveEdge(T from, T to)
        {
            int fromIndex = GetNodeIndex(from);
            int toIndex = GetNodeIndex(to);
            adjacencyMatrix[fromIndex, toIndex] = 0;
        }

        public T GetNodeByIndex(int index)
        {
            if (index < 0 || index >= nodes.Count)
                throw new ArgumentOutOfRangeException("Invalid node index");

            return nodes[index].Data;
        }

        public int GetNodeIndex(T node)
        {
            int index = nodes.FindIndex(node1 => node1.Data.Equals(node));
            if (index == -1)
            {
                throw new ArgumentException("Invalid node data");
            }
            return index;
        }

        public DirectedGraphNodeIterator<T> GetNodeIterator()
        {
            return new DirectedGraphNodeIterator<T>(this);
        }

        public DirectedGraphEdgeIterator<T> GetEdgeIterator()
        {
            return new DirectedGraphEdgeIterator<T>(this);
        }

        public DirectedGraphNodeReverseIterator<T> GetNodeReverseIterator()
        {
            return new DirectedGraphNodeReverseIterator<T>(this);
        }

        public DirectedGraphEdgeReverseIterator<T> GetEdgeReverseIterator()
        {
            return new DirectedGraphEdgeReverseIterator<T>(this);
        }

        public DirectedGraphEdgeIncidenceIterator<T> GetEdgeIncidenceIterator(T node)
        {
            return new DirectedGraphEdgeIncidenceIterator<T>(this, node);
        }

        public DirectedGraphNodeAdjacencyIterator<T> GetNodeAdjacencyIterator(T node)
        {
            return new DirectedGraphNodeAdjacencyIterator<T>(this, node);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return nodes.Select(node => node.Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetConstEnumerator()
        {
            foreach (T data in nodes.Select(node => node.Data))
            {
                yield return data;
            }
        }

        public static bool operator ==(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {
            if (ReferenceEquals(graph1, graph2))
                return true;
            if (ReferenceEquals(graph1, null) || ReferenceEquals(graph2, null))
                return false;

            if (graph1.NodeCount != graph2.NodeCount)
                return false;

            for (int i = 0; i < graph1.NodeCount; i++)
            {
                for (int j = 0; j < graph1.NodeCount; j++)
                {
                    if (graph1.adjacencyMatrix[i, j] != graph2.adjacencyMatrix[i, j])
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is DirectedGraph<T> otherGraph)
            {
                if (NodeCount != otherGraph.NodeCount)
                    return false;

                for (int i = 0; i < NodeCount; i++)
                {
                    for (int j = 0; j < NodeCount; j++)
                    {
                        if (adjacencyMatrix[i, j] != otherGraph.adjacencyMatrix[i, j])
                            return false;
                    }
                }

                return true;
            }
            return false;
        }

        public static bool operator !=(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {
            return !(graph1 == graph2);
        }

        public static bool operator >(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {

            return graph1.NodeCount > graph2.NodeCount;
        }

        public static bool operator <(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {
            return graph1.NodeCount < graph2.NodeCount;
        }

        public static bool operator >=(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {
            return graph1 > graph2 || graph1 == graph2;
        }

        public static bool operator <=(DirectedGraph<T> graph1, DirectedGraph<T> graph2)
        {
            return graph1 < graph2 || graph1 == graph2;
        }

        public bool ContainsNode(T data)
        {
            return nodes.Any(node => node.Data.Equals(data));
        }

        public bool ContainsEdge(T from, T to)
        {
            int fromIndex = GetNodeIndex(from);
            int toIndex = GetNodeIndex(to);

            return adjacencyMatrix[fromIndex, toIndex] > 0;
        }

        public List<GraphNode<T>> GetNodes()
        {
            return nodes;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + NodeCount.GetHashCode();
            for (int i = 0; i < NodeCount; i++)
            {
                for (int j = 0; j < NodeCount; j++)
                {
                    hash = hash * 31 + adjacencyMatrix[i, j].GetHashCode();
                }
            }
            return hash;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    result += adjacencyMatrix[i, j] + " ";
                }
                result += "\n";
            }
            return result;
        }
    }
}
