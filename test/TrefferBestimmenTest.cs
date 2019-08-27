using Fermat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Test
{
    
    
    /// <summary>
    ///This is a test class for FermatExtendedTest and is intended
    ///to contain all FermatExtendedTest Unit Tests
    ///</summary>
    [TestClass]
    public class TrefferBestimmenTest
    {
        /// <summary>
        ///A test for TrefferBestimmen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void TrefferBestimmenGrobTest()
        {
            int n = 499 * 41;

            List<Zyklus> result = FermatExtended.TrefferBestimmen(n, new int[] { 10 });

            List<FermatExtended.MddPaarImZahlensystem> paare = FermatExtended.mdPaareBestimmen(n, new int[] { 10 });

            List<Tuple<int, int, int>> vergleichsliste = new List<Tuple<int,int, int>>();

            int m = (int)Math.Sqrt(n) + 1;
            FermatExtended.MddPaarImZahlensystem aktuellesPaar = paare[0];
            for (int i = 0; i < (aktuellesPaar.Zahlensystem); i++)
            {
                int dd = m * m - n;
                if (aktuellesPaar.MddPaar.Exists(x => ((x.Item1 == m % aktuellesPaar.Zahlensystem) && (x.Item2 == dd % aktuellesPaar.Zahlensystem))))
                {
                    vergleichsliste.Add(new Tuple<int, int, int>(m % aktuellesPaar.Zahlensystem, dd % aktuellesPaar.Zahlensystem, i));
                }

                m++;
            }
            vergleichsliste.Add(new Tuple<int, int, int>(0, 0, aktuellesPaar.Zahlensystem + vergleichsliste[0].Item3));
            
            Zyklus aktuellerZyklus = result[0];

            for (int i = 0; i < aktuellerZyklus.NumberOfElements; i++)
            {
                Assert.IsTrue(vergleichsliste[i].Item3 == aktuellerZyklus.Elemente[i]);
            }
        }

        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void TrefferBestimmenTest2()
        {
            int[] zahlensysteme = new int[] { 3, 4, 5, 7 };
            int[] primA = new int[] { 29, 109, 2087, 3253, 6449, 9697 };
            int[] primB = new int[] { 31, 139, 3251, 4799, 7757, 9973 };

            for (int i = 0; i < primA.Length; i++)
            {
                for (int j = 0; j < primB.Length; j++)
                {
                    BigInteger n = primA[i] * primB[j];
                    List<Zyklus> result = FermatExtended.TrefferBestimmen(n, zahlensysteme);
                    var wurzel = Helper.Wurzel(n);

                    long mExpected = ((long)primA[i] + (long)primB[j]) / 2;
                    for (int k = 0; k < result.Count; k++)
                    {
                        long offset = (mExpected - (long)wurzel.Item1 - 1) / result[k].ZyklusSumme;
                        bool bFound = result[k].Exists(x => ((offset * result[k].ZyklusSumme) + x) == (mExpected - (long)wurzel.Item1 - 1));

                        Assert.IsTrue(bFound);
                    }

                }
            }
        }
    }
}
