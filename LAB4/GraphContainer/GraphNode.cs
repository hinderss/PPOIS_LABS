namespace GraphContainer
{
    public class GraphNode<T>
    {
        public T Data { get; set; }

        public GraphNode(T data)
        {
            Data = data;
        }
    }
}
