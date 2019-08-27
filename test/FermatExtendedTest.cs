using Fermat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;

namespace Test
{
    
    
    /// <summary>
    ///This is a test class for FermatExtendedTest and is intended
    ///to contain all FermatExtendedTest Unit Tests
    ///</summary>
    [TestClass]
    public class FermatExtendedTest
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
        ///A test for Run
        ///</summary>
        [TestMethod]
        public void RunTest()
        {
            int[] zahlensysteme = new int[] { 3, 4, 5, 7, 11 };
            int[] primA = new int[] { 29, 109, 2087, 3253, 6449, 9697 };
            int[] primB = new int[] { 31, 139, 3251, 4799, 7757, 9973 };


            for (int i = 0; i < primA.Length; i++)
            {
                for (int j = 0; j < primB.Length; j++)
                {
                    var actual = FermatExtended.Run(new BigInteger(primA[i] * primB[j]), zahlensysteme);
                    if (actual.Item1 == primA[i])
                    {
                        Assert.IsTrue(actual.Item1 == primA[i] && actual.Item2 == primB[j], string.Format("Erhalten: '{0}' '{1}'. Erwartet: '{2}' '{3}'.", actual.Item1, actual.Item2, primA[i], primB[j]));
                    }
                    else
                    {
                        Assert.IsTrue(actual.Item2 == primA[i] && actual.Item1 == primB[j], string.Format("Erhalten: '{0}' '{1}'. Erwartet: '{2}' '{3}'.", actual.Item1, actual.Item2, primA[i], primB[j]));
                    }
                }
            }

        }

        /// <summary>
        ///A test for EndgueltigenZyklusBestimmen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void EndgueltigenZyklusBestimmenTest()
        {
            int[] zahlensysteme = new int[] 
            {
                2, 2*2, 2*2*2, 2*2*2*2, 2*2*2*2*2, 2*2*2*2*2*2, 
                3, 3*3, 3*3*3, 3*3*3*3, 3*3*3*3*3, 3*3*3*3*3*3,
                5, 5*5, 5*5*5, 5*5*5*5,
                7, 7*7, 7*7*7, 7*7*7*7,
                11, 11*11, 11*11*11,
                13, 13*13, 13*13*13,              
            };
            int a = 29;
            int b = 31;

            // 3, 4, 5, 7, 11, 13, 17, 19, .. 23
            // int[] zahlensysteme = new int[] { 9, 16, 25, 49, 121, 169, 289, 361 };
            // int a = 29;
            // int b = 31;

            Zyklus actual = FermatExtended.EndgueltigenZyklusBestimmen(new BigInteger(a*b), zahlensysteme);

            SortZyklus(ref actual, a * b);

            Assert.IsTrue(actual.NumberOfElements== 45);
            Assert.IsTrue(actual.ZyklusSumme == 2310 );
        }
    }
}
