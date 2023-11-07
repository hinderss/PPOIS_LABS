using GraphContainer;

namespace GraphContainerTest
{
    [TestClass]
    public class NodesEdges
    {
        DirectedGraph<char> graph;

        [TestInitialize]
        public void Init()
        {
            graph = new DirectedGraph<char>();
            graph.AddNode('A');
            graph.AddNode('B');
            graph.AddNode('C');
            graph.AddNode('D');
            graph.AddNode('E');

            graph.AddEdge('A', 'B');
            graph.AddEdge('A', 'C');
            graph.AddEdge('A', 'D');
            graph.AddEdge('A', 'E');
        }

        [TestMethod]
        public void NodeCountTest()
        {
            Assert.AreEqual(5, graph.NodeCount);
        }

        [TestMethod]
        public void EdgeCountTest()
        {
            Assert.AreEqual(4, graph.EdgeCount);
        }

        [TestMethod]
        public void AInDegreeTest()
        {
            Assert.AreEqual(0, graph.GetInDegree('A'));
        }

        [TestMethod]
        public void AOutDegreeTest()
        {
            Assert.AreEqual(4, graph.GetOutDegree('A'));
        }

        [TestMethod]
        public void BDegreeTest()
        {
            Assert.AreEqual(1, graph.GetInDegree('B'));
            Assert.AreEqual(0, graph.GetOutDegree('B'));
        }

        [TestMethod]
        public void RemoveNodeTest()
        {
            graph.RemoveNode('B');
            Assert.AreEqual(4, graph.NodeCount);
            Assert.AreEqual(3, graph.EdgeCount);
        }

        [TestMethod]
        public void RemoveEdgeTest()
        {
            graph.RemoveEdge('A', 'C');
            Assert.AreEqual(5, graph.NodeCount);
            Assert.AreEqual(3, graph.EdgeCount);
        }

        [TestMethod]
        public void GetNodeIndexTest()
        {
            var i = graph.GetNodeIndex('A');
            Assert.AreEqual(0, i);
        }

        [TestMethod]
        public void GetNodeIndexExTest()
        {
            Assert.ThrowsException<ArgumentException>(() => graph.GetNodeIndex('X'));
        }

        [TestMethod]
        public void GetNodeByIndexTest()
        {
            var node = graph.GetNodeByIndex(1);
            Assert.AreEqual('B', node);
        }

        [TestMethod]
        public void ContainsNodeTest()
        {
            var test1 = graph.ContainsNode('A');
            var test2 = graph.ContainsNode('X');
            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
        }

        [TestMethod]
        public void ContainsEdgeTest()
        {
            var test1 = graph.ContainsEdge('A', 'B');
            var test2 = graph.ContainsEdge('B', 'A');
            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
        }


        [TestMethod]
        public void GetNodeByIndexExTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => graph.GetNodeByIndex(-10));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => graph.GetNodeByIndex(100));
        }

        [TestMethod]
        public void ClearTest()
        {
            graph.Clear();
            Assert.AreEqual(0, graph.NodeCount);
            Assert.AreEqual(0, graph.EdgeCount);
        }
    }
}
