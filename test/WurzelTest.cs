using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fermat;
using System.Numerics;

namespace Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class WurzelTest
    {
        public WurzelTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Wurzel15Test()
        {
            Tuple<BigInteger,bool> result = null;
            
            result = Helper.Wurzel(15);
            Assert.AreEqual<BigInteger>(3, result.Item1, "");
            Assert.IsFalse(result.Item2);


            result = Helper.Wurzel(16);
            Assert.AreEqual<BigInteger>(4, result.Item1, "");
            Assert.IsTrue(result.Item2);


            result = Helper.Wurzel(17);
            Assert.AreEqual<BigInteger>(4, result.Item1, "");
            Assert.IsFalse(result.Item2);

            result = Helper.Wurzel(19);
            Assert.AreEqual<BigInteger>(4, result.Item1, "");
            Assert.IsFalse(result.Item2);

            result = Helper.Wurzel(23);
            Assert.AreEqual<BigInteger>(4, result.Item1, "");
            Assert.IsFalse(result.Item2);

            result = Helper.Wurzel(25);
            Assert.AreEqual<BigInteger>(5, result.Item1, "");
            Assert.IsTrue(result.Item2);

            result = Helper.Wurzel(29);
            Assert.AreEqual<BigInteger>(5, result.Item1, "");
            Assert.IsFalse(result.Item2);

            result = Helper.Wurzel(35);
            Assert.AreEqual<BigInteger>(5, result.Item1, "");
            Assert.IsFalse(result.Item2);

            result = Helper.Wurzel(36);
            Assert.AreEqual<BigInteger>(6, result.Item1, "");
            Assert.IsTrue(result.Item2);

            result = Helper.Wurzel(37);
            Assert.AreEqual<BigInteger>(6, result.Item1, "");
            Assert.IsFalse(result.Item2);

            // BIG NUM
            BigInteger t = BigInteger.Parse("1261170051398003589253823949865506481681");
            BigInteger quadrat = t * t;

            result = Helper.Wurzel(quadrat);
            Assert.AreEqual<BigInteger>(t, result.Item1, "");
            Assert.IsTrue(result.Item2);

            result = Helper.Wurzel(quadrat+1);
            Assert.AreEqual<BigInteger>(t, result.Item1, "");
            Assert.IsFalse(result.Item2);
        }
    }
}
