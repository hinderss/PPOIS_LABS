using GraphContainer;

namespace GraphContainerTest
{
    [TestClass]
    public class Operators
    {
        DirectedGraph<string> graph1;
        DirectedGraph<string> graph2;
        DirectedGraph<string> graph11;

        [TestInitialize]
        public void Init()
        {
            graph1 = new DirectedGraph<string>();
            graph1.AddNode("A");
            graph1.AddNode("B");
            graph1.AddNode("C");
            graph1.AddNode("D");
            graph1.AddNode("E");

            graph1.AddEdge("A", "B");
            graph1.AddEdge("B", "C");
            graph1.AddEdge("C", "D");
            graph1.AddEdge("D", "E");
            
            graph2 = new DirectedGraph<string>();
            graph2.AddNode("A");
            graph2.AddNode("B");
            graph2.AddNode("D");
            graph2.AddNode("E");

            graph2.AddEdge("A", "B");
            graph2.AddEdge("D", "E");

            graph11 = new DirectedGraph<string>();
            graph11.AddNode("A");
            graph11.AddNode("B");
            graph11.AddNode("C");
            graph11.AddNode("D");
            graph11.AddNode("E");

            graph11.AddEdge("A", "B");
            graph11.AddEdge("B", "C");
            graph11.AddEdge("C", "D");
            graph11.AddEdge("D", "E");
        }        

        [TestMethod]
        public void EqualTest()
        {
            Assert.IsTrue(graph1 == graph11);
            Assert.IsTrue(graph1 == graph1);
            Assert.IsFalse(graph1 == graph2);
        }

        [TestMethod]
        public void EqualsTest()
        {
            var x = new List<string>();
            Assert.IsTrue(graph1.Equals(graph11));
            Assert.IsTrue(graph1.Equals(graph1));
            Assert.IsFalse(graph1.Equals(graph2));
            Assert.IsFalse(graph1.Equals(x));
        }

        [TestMethod]
        public void NotEqualTest()
        {
            Assert.IsFalse(graph1 != graph11);
            Assert.IsFalse(graph1 != graph1);
            Assert.IsTrue(graph1 != graph2);
        }

        [TestMethod]
        public void GreaterTest()
        {
            Assert.IsFalse(graph1 > graph11);
            Assert.IsTrue(graph1 > graph2);
        }

        [TestMethod]
        public void LessTest()
        {
            Assert.IsFalse(graph1 < graph11);
            Assert.IsTrue(graph2 < graph1);
        }

        [TestMethod]
        public void LessOrEqualTest()
        {
            Assert.IsTrue(graph1 <= graph11);
            Assert.IsTrue(graph2 <= graph1);
            Assert.IsFalse(graph1 <= graph2);
        }

        [TestMethod]
        public void GreaterOrEqualTest()
        {
            Assert.IsTrue(graph1 >= graph11);
            Assert.IsFalse(graph2 >= graph1);
            Assert.IsTrue(graph1 >= graph2);
        }
    }
}
