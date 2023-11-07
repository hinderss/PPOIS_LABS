namespace GraphContainer
{
    [TestClass]
    public class Create
    {
        [TestMethod]
        public void CreateTest()
        {
            DirectedGraph<char> graph = new DirectedGraph<char>();
            Assert.IsTrue(graph.Empty());
        }

        [TestMethod]
        public void CloneTest()
        {
            // Arrange
            DirectedGraph<char> instanceToClone = new DirectedGraph<char>();

            // Act
            object clonedInstance = instanceToClone.Clone();

            // Assert
            Assert.IsNotNull(clonedInstance);
            Assert.AreNotSame(instanceToClone, clonedInstance);
        }
    }
}
