using Fermat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    
    
    /// <summary>
    ///This is a test class for FermatExtendedTest and is intended
    ///to contain all FermatExtendedTest Unit Tests
    ///</summary>
    [TestClass]
    public class BestimmeOffsetZahlenZweierZyklenTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for BestimmeOffsetZahlenZweierZyklen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest1()
        {
            int a = 7;
            int b = 3;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }

        /// <summary>
        ///A test for BestimmeOffsetZahlenZweierZyklen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest2()
        {
            int a = 100;
            int b = 13;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }

        /// <summary>
        ///A test for BestimmeOffsetZahlenZweierZyklen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest3()
        {
            int a = 121;
            int b = 15;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }

        /// <summary>
        ///A test for BestimmeOffsetZahlenZweierZyklen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest4()
        {
            int a = 7951;
            int b = 7949;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }


        /// <summary>
        /// Findet Lösung -1 vor +1
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest5()
        {
            int a = 5;
            int b = 3;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }

        /// <summary>
        /// Findet Lösung -1 vor +1
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest6()
        {
            int a = 3;
            int b = 2;
            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }


        /// <summary>
        /// 
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest7()
        {
            int b = 197;
            for (int a = 501; a < 800; a++)
            {
                // Überspringe Teiler
                if (a % b == 0)
                    continue;


                Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

                Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
            }
        }


        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void BestimmeOffsetZahlenZweierZyklenTest8()
        {
            int b = 197;
            int a = 590;

            Tuple<long, long> actual = Zyklus.BestimmeOffsetZahlenZweierZyklen(a, b);

            Assert.IsTrue(((actual.Item1 * a) % (actual.Item2 * b)) == 1);
        }
    }
}
