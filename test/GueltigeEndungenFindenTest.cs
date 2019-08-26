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
    /// Summary description for GueltigeEndungenFinden
    /// </summary>
    [TestClass]
    public class GueltigeEndungenFindenTest
    {
        public GueltigeEndungenFindenTest()
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
        public void TestMethod1()
        {
            Check(new BigInteger(37 * 941), 2);
            Check(new BigInteger(37 * 941), 3);
            Check(new BigInteger(37 * 941), 4);
            Check(new BigInteger(37 * 941), 5);
            Check(new BigInteger(37 * 941), 6);
            Check(new BigInteger(37 * 941), 7);
            Check(new BigInteger(37 * 941), 8);
            Check(new BigInteger(37 * 941), 9);
            Check(new BigInteger(37 * 941), 10);
            Check(new BigInteger(37 * 941), 11);
            
            
            Check(new BigInteger(37 * 937), 6);
            Check(new BigInteger(37 * 929), 6);
        }

        private static void Check(BigInteger n, int zahlensystem)
        {
            int potenz = zahlensystem;

            FermatExtended target = new FermatExtended(n, new int[] { zahlensystem });

            List<Tuple<int, int>> actual = target.EndungenBestimmen(zahlensystem);

            BigInteger sh;
            BigInteger.DivRem(n, potenz, out sh);
            int vergleichswert = (int)sh;

            List<Tuple<int, int>> expected = new List<Tuple<int, int>>();

            for (int i = 0; i < potenz; i++)
            {
                for (int j = i; j < potenz; j++)
                {
                    if (((i * j) % potenz) == vergleichswert)
                        expected.Add(new Tuple<int, int>(i, j));
                }
            }


            Assert.IsTrue(actual.Count == expected.Count);

            foreach (Tuple<int, int> currentItem in expected)
            {
                // also include "switched" values
                Assert.IsTrue(actual.Exists(x => ((x.Item1 == currentItem.Item1) && (x.Item2 == currentItem.Item2)) || ((x.Item1 == currentItem.Item2) && (x.Item2 == currentItem.Item1))));
            }
        }

        static void Debug(List<Tuple<int,int>> list)
        {
            string text = string.Empty;
            foreach (Tuple<int, int> currentItem in list)
            {
                text += string.Format("{0} * {1} = {2} \n", currentItem.Item1, currentItem.Item2, currentItem.Item1 * currentItem.Item2);
            }

            //MessageBox.Show(text);
            Console.WriteLine(text);
        }
    }
}
