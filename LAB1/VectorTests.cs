using Vector;

namespace VectorTest
{
   
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void Constructor3Test()
        {
            var a = new Vector3D(1, 2, 3);
            Assert.AreEqual(1, a.X);
            Assert.AreEqual(2, a.Y);
            Assert.AreEqual(3, a.Z);
        }

        [TestMethod]
        public void Constructor0Test()
        {
            var a = new Vector3D();
            Assert.AreEqual(0, a.X);
            Assert.AreEqual(0, a.Y);
            Assert.AreEqual(0, a.Z);
        }

        [TestMethod]
        public void Constructor2Test()
        {
            var a = new Vector3D(1, 2);
            Assert.AreEqual(1, a.X);
            Assert.AreEqual(2, a.Y);
            Assert.AreEqual(0, a.Z);
        }

        [TestMethod]
        public void ConstructorCopyTest()
        {
            var a = new Vector3D(1, 2, 3);
            var b = new Vector3D(a);
            Assert.AreEqual(1, b.X);
            Assert.AreEqual(2, b.Y);
            Assert.AreEqual(3, b.Z);
        }

        [TestMethod]
        public void Constructor6Test()
        {
            var a = new Vector3D(10, 4, 9, 3, 2, 5);
            Assert.AreEqual(-7, a.X);
            Assert.AreEqual(-2, a.Y);
            Assert.AreEqual(-4, a.Z);
        }

        [TestMethod]
        public void Constructor6FTest()
        {
            var a = new Vector3D(10.56545, -4.4555, 9.0, 0.44585, 2, 5);
            Assert.AreEqual(-10.1196, a.X, 0.0001);
            Assert.AreEqual(6.4555, a.Y, 0.0001);
            Assert.AreEqual(-4, a.Z, 0.0001);
        }

        [TestMethod]
        public void LenghtGetTest1()
        {
            var a = new Vector3D(10, 2, 5);
            Assert.AreEqual(11.357816, a.Length, 0.001);
        }

        [TestMethod]
        public void LenghtGetTest2()
        {
            var a = new Vector3D(-8.055, 47, 4545.7);
            Assert.AreEqual(4545.950106746113, a.Length, 0.001);
        }

        [TestMethod]
        public void SumTest()
        {
            var a = new Vector3D(2, 7, 4);
            var b = new Vector3D(10, 1, 5);
            var c = a + b;
            Assert.AreEqual(12, c.X);
            Assert.AreEqual(8, c.Y);
            Assert.AreEqual(9, c.Z);
        }

        [TestMethod]
        public void DifferenceTest()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(2, 7, 4);
            var c = a - b;
            Assert.AreEqual(8, c.X);
            Assert.AreEqual(-6, c.Y);
            Assert.AreEqual(1, c.Z);
        }

        [TestMethod]
        public void DotProductTest()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(2, 7, 4);
            var c = a * b;
            Assert.AreEqual(47, c);
        }

        [TestMethod]
        public void CrossProductTest1()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(2, 7, 4);
            var c = Vector3D.CrossProduct(a, b);
            Assert.AreEqual(-31, c.X);
            Assert.AreEqual(-30, c.Y);
            Assert.AreEqual(68, c.Z);
        }

        [TestMethod]
        public void CrossProductTest2()
        {
            var a = new Vector3D(89.14, 1.0, 4);
            var b = new Vector3D(2.549, 7.5465, -78);
            var c = Vector3D.CrossProduct(a, b);
            Assert.AreEqual(-108.186, c.X, 0.001);
            Assert.AreEqual(6963.116, c.Y, 0.001);
            Assert.AreEqual(670.146, c.Z, 0.001);
            //; ; 
        }

        [TestMethod]
        public void MultiplierProductTest1()
        {
            var a = new Vector3D(10, 1, 5);
            var c = a * 5;
            Assert.AreEqual(50, c.X);
            Assert.AreEqual(5, c.Y);
            Assert.AreEqual(25, c.Z);
        }

        [TestMethod]
        public void MultiplierProductTest2()
        {
            var a = new Vector3D(-4.04, 7.25, 0);
            var c = a * 654.458;
            Assert.AreEqual(-4.04 * 654.458, c.X);
            Assert.AreEqual(7.25 * 654.458, c.Y);
            Assert.AreEqual(0, c.Z);
        }

        [TestMethod]
        public void GreaterThanTest1()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(2, 7, 4);
            Assert.IsTrue(a > b);
        }

        [TestMethod]
        public void GreaterThanTest2()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(10, 7, 5);
            Assert.IsFalse(a > b);
        }

        [TestMethod]
        public void GreaterLessThanTest()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(10, 1, 5);
            Assert.IsFalse(a > b);
            Assert.IsFalse(a < b);
        }

        [TestMethod]
        public void LessThanTest1()
        {
            var a = new Vector3D(9, 1, 120);
            var b = new Vector3D(10, 1, 5);
            Assert.IsTrue(a < b);
        }

        [TestMethod]
        public void LessThanTest2()
        {
            var a = new Vector3D(9, 5, 120);
            var b = new Vector3D(9, 1, 5);
            Assert.IsFalse(a < b);
        }

        [TestMethod]
        public void GreaterThanTest3()
        {
            var a = new Vector3D(9, 5, 120);
            var b = new Vector3D(9, 5, 5);
            Assert.IsTrue(a > b);
        }

        [TestMethod]
        public void LessThanTest3()
        {
            var a = new Vector3D(9, 5, -66);
            var b = new Vector3D(9, 5, 5);
            Assert.IsTrue(a < b);
        }

        [TestMethod]
        public void MultiplierDivisionTest()
        {
            var a = new Vector3D(10, 5, 7);
            var c = a / 5;
            Assert.AreEqual(2, c.X);
            Assert.AreEqual(1, c.Y);
            Assert.AreEqual(1.4, c.Z, 0.01);
        }


        [TestMethod]
        public void DivisionByZeroTest()
        {
            var a = new Vector3D(10, 5, 7);

            Assert.ThrowsException<DivideByZeroException>(() => { var c = a / 0; });
        }

        [TestMethod]
        public void DivisionTwoVectorsTest()
        {
            var a = new Vector3D(10, 1, 5);
            var b = new Vector3D(2, 7, 4);
            var c = a / b;
            Assert.AreEqual(5, c.X);
            Assert.AreEqual(0.1428, c.Y, 0.0001);
            Assert.AreEqual(1.25, c.Z, 0.001);
        }

        [TestMethod]
        public void CosTest()
        {
            var a = new Vector3D(28, 15.01, 64);
            var b = new Vector3D(-1.787, 2.5, -96.78);
            var cos = a ^ b;
            Assert.AreEqual(-0.8970710751407514, cos, 0.00001);
        }

        [TestMethod]
        public void EqualTest()
        {
            var a = new Vector3D(45, 15, 96);
            var b = new Vector3D(45, 15, 96);
            var c = a == b;
            Assert.IsTrue(c);
        }

        [TestMethod]
        public void NotEqualTest()
        {
            var a = new Vector3D(28, 15, 64);
            var b = new Vector3D(-1, 2, 96);
            var c = a != b;
            Assert.IsTrue(c);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var a = new Vector3D(-828, 15, 4);
            StringAssert.Equals("-828, 15, 4", a.ToString());
        }

        [TestMethod]
        public void PrintMethodTest()
        {
            var instance = new Vector3D(-0.12, 4, 5);
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            string expectedOutput = $"({instance.ToString()})";

            instance.Print();
            string actualOutput = sw.ToString().Trim();

            Assert.AreEqual(expectedOutput, actualOutput);

        }
    }
}