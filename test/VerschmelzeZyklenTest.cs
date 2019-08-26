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
    public class VerschmelzeZyklenTest
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


        static private void SortZyklus(ref Zyklus zyklus, int size)
        {
            for (int i = 0; i < zyklus.NumberOfElements; i++)
            {
                if (zyklus.Elemente[i] < 0)
                    zyklus.Elemente[i] += size;
            }

            zyklus.Sort();
        }

        /// <summary>
        ///A test for VerschmelzeZyklen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void VerschmelzeZyklenTest1()
        {
            Zyklus zyklusA = new Zyklus(2, new long[] { 1, 2 });

            Zyklus zyklusB = new Zyklus(3, new long[] { 1, 2 });

            Zyklus expected = new Zyklus(6, new long[] { 1, 2, 4, 5 });

            Zyklus actual = Zyklus.VerschmelzeZyklen(zyklusA, zyklusB);
            Assert.IsTrue(expected.NumberOfElements == actual.NumberOfElements);

            SortZyklus(ref actual, 6);

            for (int i = 0; i < expected.NumberOfElements; i++)
            {
                Assert.IsTrue(actual.Exists(x => x == expected.Elemente[i]));
            }
        }

        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void VerschmelzeZyklenTest2()
        {
            Zyklus zyklusA = new Zyklus(3, new long[] { 1, 2 });

            Zyklus zyklusB = new Zyklus(5, new long[] { 1, 2, 5 });

            Zyklus expected = new Zyklus(15, new long[] { 1, 2, 5, 7, 10, 11 });

            Zyklus actual = Zyklus.VerschmelzeZyklen(zyklusA, zyklusB);
            Assert.IsTrue(expected.NumberOfElements == actual.NumberOfElements);

            SortZyklus(ref actual, 15);

            for (int i = 0; i < expected.NumberOfElements; i++)
            {
                // Bedenken, dass für den Zyklus der Länge 15 gilt: -1 == 14
                Assert.IsTrue(actual.Exists(x => x == expected.Elemente[i]));
            }
        }

    }
}
