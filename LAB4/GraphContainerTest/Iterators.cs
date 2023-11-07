using GraphContainer;

namespace GraphContainerTest
{
    [TestClass]
    public class Iterators
    {
        DirectedGraph<string> graph;

        [TestInitialize]
        public void Init()
        {
            graph = new DirectedGraph<string>();
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");

            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("C", "D");
            graph.AddEdge("D", "E");
        }        
        private DirectedGraph<string> Init2()
        {
            var graph1 = new DirectedGraph<string>();
            graph1.AddNode("A");
            graph1.AddNode("B");
            graph1.AddNode("C");
            graph1.AddNode("D");
            graph1.AddNode("E");
                 
            graph1.AddEdge("A", "B");
            graph1.AddEdge("A", "C");
            graph1.AddEdge("A", "D");
            graph1.AddEdge("A", "E");

            return graph1;
        }

        [TestMethod]
        public void DirectedGraphNodeIteratorTest()
        {
            List<string> list1 = new List<string>();
            var nodeIterator = graph.GetNodeIterator();
            while (nodeIterator.MoveNext())
            {
                string nodeData = nodeIterator.Current;
                list1.Add(nodeData);
            }
            var nodes = graph.GetNodes();
            List<string> list2 = new List<string>();
            
            foreach (var item in nodes)
            {
                list2.Add(item.Data);
            }

            CollectionAssert.AreEqual(list2, list1);
        }
        
        [TestMethod]
        public void PrevDirectedGraphNodeIteratorTest()
        {
            List<string> list1 = new List<string>();
            bool flag = true;
            var nodeIterator = graph.GetNodeIterator();
            while (nodeIterator.MoveNext())
            {
                if (nodeIterator.Current == "D" && flag)
                {
                    flag = false;
                    nodeIterator.MovePrev();
                }
                string nodeData = nodeIterator.Current;
                list1.Add(nodeData);
            }
            List<string> list2 = new List<string>();
            list2.Add("A");
            list2.Add("B");
            list2.Add("C");
            list2.Add("C");
            list2.Add("D");
            list2.Add("E");

            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void DirectedGraphNodeReverseIteratorTest()
        {
            List<string> list1 = new List<string>();
            var nodeReverseIterator = graph.GetNodeReverseIterator();
            while (nodeReverseIterator.MoveNext())
            {
                string nodeData = nodeReverseIterator.Current;
                list1.Add(nodeData);
            }
            var nodes = graph.GetNodes();
            List<string> list2 = new List<string>();
            
            foreach (var item in nodes)
            {
                list2.Add(item.Data);
            }

            list2.Reverse();

            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void DirectedGraphEdgeIteratorTest()
        {
            List<(string, string)> list1 = new List<(string, string)>();
            var edgeIterator = graph.GetEdgeIterator();
            while (edgeIterator.MoveNext())
            {
                (string from, string to) = edgeIterator.Current;
                list1.Add((from, to));
            }
            List<(string, string)> list2 = new List<(string, string)>();

            list2.Add(("A", "B"));
            list2.Add(("B", "C"));
            list2.Add(("C", "D"));
            list2.Add(("D", "E"));


            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void PrevDirectedGraphEdgeIteratorTest()
        {
            List<(string, string)> list1 = new List<(string, string)>();
            bool flag = true;
            var edgeIterator = graph.GetEdgeIterator();
            while (edgeIterator.MoveNext())
            {
                (string from, string to) = edgeIterator.Current;
                if (from == "C" && flag)
                {
                    flag = false;
                    edgeIterator.MovePrev();
                }
                list1.Add((from, to));
            }
            List<(string, string)> list2 = new List<(string, string)>();

            list2.Add(("A", "B"));
            list2.Add(("B", "C"));
            list2.Add(("C", "D"));
            list2.Add(("C", "D"));
            list2.Add(("D", "E"));

            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void DirectedGraphEdgeReverseIteratorTest()
        {
            List<(string, string)> list1 = new List<(string, string)>();
            var edgeReverseIterator = graph.GetEdgeReverseIterator();
            while (edgeReverseIterator.MoveNext())
            {
                (string from, string to) = edgeReverseIterator.Current;
                list1.Add((from, to));
            }
            List<(string, string)> list2 = new List<(string, string)>();

            list2.Add(("A", "B"));
            list2.Add(("B", "C"));
            list2.Add(("C", "D"));
            list2.Add(("D", "E"));

            list2.Reverse();

            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void DirectedGraphNodeAdjacencyIteratorTest()
        {
            var graph1 = Init2();
            List<string> list1 = new List<string>();
            bool flag = true;
            var nodeAIterator = graph1.GetNodeAdjacencyIterator("A");
            while (nodeAIterator.MoveNext())
            {
                string nodeData = nodeAIterator.Current;
                list1.Add(nodeData);
            }
            List<string> list2 = new List<string>();
            list2.Add("B");
            list2.Add("C");
            list2.Add("D");
            list2.Add("E");

            CollectionAssert.AreEqual(list2, list1);
        }

        [TestMethod]
        public void DirectedGraphEdgeIncidenceIteratorTest()
        {
            var graph1 = Init2();
            List<(string, string)> list1 = new List<(string, string)>();
            var edgeIIterator = graph1.GetEdgeIncidenceIterator("A");
            while (edgeIIterator.MoveNext())
            {
                (string from, string to) = edgeIIterator.Current;
                list1.Add( (from, to));
            }
            List<(string, string)> list2 = new List<(string, string)>();

            list2.Add(("A", "B"));
            list2.Add(("A", "C"));
            list2.Add(("A", "D"));
            list2.Add(("A", "E"));

            CollectionAssert.AreEqual(list2, list1);
        }
    }
}
