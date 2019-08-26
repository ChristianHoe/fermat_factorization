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
    public class ZyklusVerkuerzenTest
    {
        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstNichtVerkuerzbarTest()
        {
            const int zyklusSumme = 10;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 2, 5, 8 });
            long[] neu = new long[] { 0, 2, 5, 8 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == zyklusSumme);
        }

        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstNichtEchtVerkuerzbarTest()
        {
            const int zyklusSumme = 13;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 2, 5, 8, 10 });
            long[] neu = new long[] { 0, 2, 5, 8, 10 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == zyklusSumme);
        }


        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstVerkuerzbarTest()
        {
            const int zyklusSumme = 10;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 2, 5, 7 });
            long[] neu = new long[] { 0, 2 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == (zyklusSumme / 2));
        }


        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstVerkuerzbarTest2()
        {
            const int zyklusSumme = 4;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 2 });
            long[] neu = new long[] { 0 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == (zyklusSumme / 2));
        }

        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstVerkuerzbarTest3()
        {
            const int zyklusSumme = 12;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 1, 2, 4, 5, 6, 8, 9, 10 });
            long[] neu = new long[] { 0, 1, 2 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == (zyklusSumme / 3));
        }
        
        /// <summary>
        ///A test for ZyklusVerkuerzen
        ///</summary>
        [TestMethod]
        [DeploymentItem("Fermat.exe")]
        public void ZyklusIstNichtVerkuerzbarTest2()
        {
            const int zyklusSumme = 10;
            Zyklus zyklus = new Zyklus(zyklusSumme, new long[] { 0, 2, 4, 6 });
            long[] neu = new long[] { 0, 2, 4, 6 }; // da Referenztyp

            Zyklus.ZyklusVerkuerzen(ref zyklus);

            for (int i = 0; i < neu.Length; i++)
            {
                Assert.IsTrue(zyklus.Elemente[i] == neu[i]);
            }

            Assert.IsTrue(zyklus.ZyklusSumme == zyklusSumme);
        }
    }
}
