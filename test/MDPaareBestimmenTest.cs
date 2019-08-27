using Fermat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test
{
    
    
    /// <summary>
    ///This is a test class for FermatExtendedTest and is intended
    ///to contain all FermatExtendedTest Unit Tests
    ///</summary>
    [TestClass]
    public class MDPaareBestimmenTest
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
        ///A test for mdPaareBestimmen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void mdPaareBestimmenTest()
        {
            int zahlensystem = 10;
            int n = 43 * 971;

            List<FermatExtended.MddPaarImZahlensystem> actual = FermatExtended.mdPaareBestimmen(n, new int[] { zahlensystem });
            FermatExtended.MddPaarImZahlensystem expected = this.BerechneMDInternal(n, zahlensystem);


            Assert.IsTrue(actual[0].MddPaar.Count == expected.MddPaar.Count);

            for (int i = 0; i < expected.MddPaar.Count; i++)
            {
                Assert.IsTrue(actual[0].MddPaar.Exists(x => ((x.Item1 == expected.MddPaar[i].Item1) && (x.Item2 == expected.MddPaar[i].Item2)))); 
            }
        }


        private FermatExtended.MddPaarImZahlensystem BerechneMDInternal(int n, int zahlensystem)
        {
            int m;
            int d1;
            int d2;
            int dd;
            //int potenz = zahlensystem * zahlensystem;

            FermatExtended.MddPaarImZahlensystem mddPaareImZahlensystem = new FermatExtended.MddPaarImZahlensystem(zahlensystem);

            List<Tuple<int, int>> endungen = FermatExtended.EndungenBestimmen(n, zahlensystem);
            
            foreach (Tuple<int, int> endung in endungen)
            {
                // aller Wahrscheinlichkeit nach reichen zwei Überprüfungen
                // 0*10**1+X+Y
                // 1*10**1+X+Y
                for (int i = 0; i < zahlensystem; i++)
                {
                    for (int j = 0; j < zahlensystem; j++)
                    {
                        m  = ((((i * zahlensystem) + endung.Item1) + ((j * zahlensystem) + endung.Item2)) / 2) % zahlensystem;
                        d1 = ((((i * zahlensystem) + endung.Item1) - ((j * zahlensystem) + endung.Item2)) / 2) % zahlensystem;
                        d2 = ((((i * zahlensystem) + endung.Item2) - ((j * zahlensystem) + endung.Item1)) / 2) % zahlensystem;

                        if (d1 < 0)
                            d1 += zahlensystem;

                        if (d2 < 0)
                            d2 += zahlensystem;


                        // pruefen, ob "m" eine Ganzzahl geblieben ist, sonst kann das Ergebnis verworfen werden
                        if (((2 * m) % zahlensystem) == (((i * zahlensystem) + endung.Item1) + ((j * zahlensystem) + endung.Item2)) % zahlensystem)
                        {
                            dd = (d1 * d1) % zahlensystem;
                            if (!mddPaareImZahlensystem.MddPaar.Exists(x => x.Item1 == m && x.Item2 == dd))
                                mddPaareImZahlensystem.MddPaar.Add(new Tuple<int, int>(m, dd));

                            dd = (d2 * d2) % zahlensystem;
                            if (!mddPaareImZahlensystem.MddPaar.Exists(x => x.Item1 == m && x.Item2 == dd))
                                mddPaareImZahlensystem.MddPaar.Add(new Tuple<int, int>(m, dd));
                        }
                    }
                }
            }

            return mddPaareImZahlensystem;
        }
    }
}
