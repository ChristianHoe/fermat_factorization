using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Fermat
{
    public class FermatExtended
    {
        static public (BigInteger A, BigInteger B) Run(BigInteger n, int[] zahlensysteme)
        {
            var startwert = Helper.Wurzel(n);
            if (startwert.Item2 == true)
            {
                Console.WriteLine(string.Format("Lösung ist die Wurzel {0}", startwert.Sqrt));
                return (startwert.Item1, startwert.Item1);
            }

            Zyklus zyklus = EndgueltigenZyklusBestimmen(n, zahlensysteme);

            zyklus.Sort();

            (BigInteger, bool) ergebnisDerWurzel;
            BigInteger offset = BigInteger.Add(startwert.Item1, 1);
            while (true)
            {
                for (int i = 0; i < zyklus.NumberOfElements; i++)
                {
                    // Formel: ab = SQRT( (x*x) - n) | mit x := x+1

                    ergebnisDerWurzel = Helper.Wurzel(BigInteger.Subtract(BigInteger.Pow(BigInteger.Add(offset, zyklus.Elemente[i]), 2), n));

                    if (ergebnisDerWurzel.Item2 == true)
                    {
                        BigInteger a = BigInteger.Add(BigInteger.Add(offset, zyklus.Elemente[i]), ergebnisDerWurzel.Item1);
                        BigInteger b = BigInteger.Subtract(BigInteger.Add(offset, zyklus.Elemente[i]), ergebnisDerWurzel.Item1);

                        return (a, b);
                    }
                }
                    
                offset = BigInteger.Add(offset, zyklus.ZyklusSumme);
            }
        }

        static public Zyklus EndgueltigenZyklusBestimmen(BigInteger n, int[] zahlensysteme)
        {
            List<Zyklus> zyklen = TrefferBestimmen(n, zahlensysteme);


            Zyklus endgueltigerZyklus = zyklen[0];

            for (int i = 1; i < zyklen.Count; i++)
            {
                endgueltigerZyklus = Zyklus.VerschmelzeZyklen(zyklen[i], endgueltigerZyklus);
            }

            Console.WriteLine($"Kombiniert: {endgueltigerZyklus.NumberOfElements,3:0} / {endgueltigerZyklus.ZyklusSumme,3:0} = {(endgueltigerZyklus.NumberOfElements / (double)endgueltigerZyklus.ZyklusSumme)}");

            return endgueltigerZyklus;
        }


        public struct MddPaarImZahlensystem
        {
            public int Zahlensystem;
            public List<Tuple<int, int>> MddPaar;

            public MddPaarImZahlensystem(int zahlensystem)
            {
                this.Zahlensystem = zahlensystem;
                MddPaar = new List<Tuple<int, int>>();
            }
        }


        static public List<MddPaarImZahlensystem> mdPaareBestimmen(BigInteger n, int[] zahlensysteme)
        {
            List<MddPaarImZahlensystem> mddPaare = new List<MddPaarImZahlensystem>();

            Console.WriteLine("Potentielle zu prüfende Zahlen");
            Console.WriteLine("HIT / MAX = Komprimierung");

            foreach (int zahlensystem in zahlensysteme)
            {
                int m;
                int d1;
                int d2;
                int dd;
                //int potenz = zahlensystem*zahlensystem;

                MddPaarImZahlensystem mddPaareImZahlensystem = new MddPaarImZahlensystem(zahlensystem);
                
                List<Tuple<int, int>> endungen = EndungenBestimmen(n, zahlensystem);
                foreach (Tuple<int, int> endung in endungen)
                {
                    // aller Wahrscheinlichkeit nach reichen zwei Überprüfungen
                    // 0*10**1+X+Y
                    // 1*10**1+X+Y
                    for (int i = 0; i < 2; i++)
                    {
                        m  = ((((i * zahlensystem) + endung.Item1) + endung.Item2) / 2) % zahlensystem;
                        d1 = ((((i * zahlensystem) + endung.Item1) - endung.Item2) / 2) % zahlensystem;
                        d2 = ((((i * zahlensystem) + endung.Item2) - endung.Item1) / 2) % zahlensystem;
                        
                        // pruefen, ob "m" eine Ganzzahl geblieben ist, sonst kann das Ergebnis verworfen werden
                        if (((endung.Item1 ^ endung.Item2 ^ (i * zahlensystem)) & 1) == 0)
                        {
                            dd = (d1 * d1) % zahlensystem;
                            if (!mddPaareImZahlensystem.MddPaar.Exists(x => x.Item1 == m && x.Item2 == dd))
                                mddPaareImZahlensystem.MddPaar.Add(new Tuple<int,int>(m,dd));

                            dd = (d2 * d2) % zahlensystem;
                            if (!mddPaareImZahlensystem.MddPaar.Exists(x => x.Item1 == m && x.Item2 == dd))
                                mddPaareImZahlensystem.MddPaar.Add(new Tuple<int,int>(m,dd));
                        }
                    } 
                }

                mddPaare.Add(mddPaareImZahlensystem);

                Console.WriteLine($"Treffer: {mddPaareImZahlensystem.MddPaar.Count,3:0} / {mddPaareImZahlensystem.Zahlensystem,3:0} = {(mddPaareImZahlensystem.MddPaar.Count / (double)mddPaareImZahlensystem.Zahlensystem):F3}");
            }

            return mddPaare;
        }

        static public List<Zyklus> TrefferBestimmen(BigInteger n, int[] zahlensysteme)
        {
            List<Zyklus> result = new List<Zyklus>();

            List<MddPaarImZahlensystem> mdPaare = mdPaareBestimmen(n, zahlensysteme);

            // Triviale Lösung wird an anderer Stelle geprüft
            var startwert = Helper.Wurzel(n);

            int maxSequenz = zahlensysteme.Max() * zahlensysteme.Max();
            BigInteger[] arbeitsSequenzM_ = new BigInteger[maxSequenz+1];
            BigInteger[] arbeitsSequenzDD = new BigInteger[maxSequenz+1];

            // da der Startwert sonst negativ ist, wenn nicht die Wurzel die Lösung ist
            BigInteger m = BigInteger.Add(startwert.Item1, 1);
            BigInteger wurzelDelta = BigInteger.Add(BigInteger.Multiply(2, m), 1);

            // Sequenz einmalig berechnen
            arbeitsSequenzM_[0] = m;
            arbeitsSequenzDD[0] = BigInteger.Subtract(BigInteger.Pow(m, 2), n);

            for (int i = 1; i <= maxSequenz; i++)
            {
                arbeitsSequenzM_[i] = BigInteger.Add(arbeitsSequenzM_[i-1], 1);
                arbeitsSequenzDD[i] = BigInteger.Add(arbeitsSequenzDD[i - 1], wurzelDelta);
                wurzelDelta = BigInteger.Add(wurzelDelta, 2);
            }

            // Die Zahlen ins entsprechende Zahlensystem umwandeln und "Treffer" merken
            // Es werden nur die Abstände zwischen zwei "Treffern" vermerkt
            //int potenz;
            BigInteger tmpM_;
            BigInteger tmpDD;
            foreach (MddPaarImZahlensystem paar in mdPaare)
            {
                List<long> sequenz = new List<long>();
                //potenz = paar.Zahlensystem * paar.Zahlensystem;
                int zahlensystem = paar.Zahlensystem;

                for (int j = 0; j < zahlensystem; j++)
                {
                    BigInteger.DivRem(arbeitsSequenzM_[j], zahlensystem, out tmpM_);
                    BigInteger.DivRem(arbeitsSequenzDD[j], zahlensystem, out tmpDD);

                    if (paar.MddPaar.Exists(x => ((x.Item1 == tmpM_) && (x.Item2 == tmpDD))))
                    {
                        sequenz.Add(j);
                    }
                }

                Zyklus zyklus = new Zyklus(zahlensystem, sequenz.ToArray<long>());
                Zyklus.ZyklusVerkuerzen(ref zyklus);

                result.Add(zyklus);
            }

            return result;
        }


        /// <summary>
        /// Für das gegebene Zahlensystem wird geschaut, für welche X und Y gilt: X * Y = n
        /// Wobei dies nur für die letzten Stellen betrachtet wird
        /// </summary>
        /// <param name="zahlensystem"></param>
        static public List<Tuple<int, int>> EndungenBestimmen(BigInteger n, int zahlensystem)
        {
            BigInteger.DivRem(n, zahlensystem, out BigInteger sh);
            int vergleichswert = (int)sh;

            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            for (int i = 0; i < zahlensystem; i++)
            {
                for (int j = i; j < zahlensystem; j++)
                {
                    if (((i * j) % zahlensystem) == vergleichswert)
                        result.Add(new Tuple<int, int>(i, j));
                }
            }

            return result;
        }
     }
}
