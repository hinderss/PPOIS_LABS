using GraphContainer;

namespace GraphContainerTest
{
    [TestClass]
    public class ToString
    {
        DirectedGraph<char> graph;

        [TestInitialize]
        public void Init()
        {
            graph = new DirectedGraph<char>();
            graph.AddNode('A');
            graph.AddNode('B');

            graph.AddEdge('A', 'B');
        }

        [TestMethod]
        public void ToStringTest()
        {
            string text = "0 1 \n0 0 \n";
            Assert.AreEqual(text, graph.ToString());
        }
    }
}
