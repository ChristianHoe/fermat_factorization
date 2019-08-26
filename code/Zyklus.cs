using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fermat
{
    public class Zyklus
    {
        public long ZyklusSumme { get; private set; }
        public List<long> Elemente { get; private set; }

        public Zyklus(long zyklusSumme, long numberOfElements)
        {
            this.ZyklusSumme = zyklusSumme;
            //this.Elemente = new long[numberOfElements];
            this.Elemente = new List<long>((int)numberOfElements);
        }

        public Zyklus(long zyklusSumme, long[] zyklus)
        {
            this.ZyklusSumme = zyklusSumme;
            this.Elemente = new List<long>(zyklus);
        }

        public int NumberOfElements { get { return this.Elemente.Count; } }

        public void Sort()
        {
            //Array.Sort<long>(this.Elemente);
            this.Elemente.Sort();
        }

        public bool Exists(Predicate<long> x)
        {
            //return Array.FindIndex<long>(this.Elemente, x) != -1;
            return this.Elemente.Exists(x);
        }

        public void Truncate(int newSize)
        {
            //Array.Resize<long>(ref this.Elemente, newSize);
            this.Elemente.RemoveRange(newSize, (int)this.NumberOfElements - newSize);
        }

        static public Zyklus VerschmelzeZyklen(Zyklus zyklusA, Zyklus zyklusB)
        {
            Zyklus A;
            Zyklus B;
            if (zyklusA.ZyklusSumme > zyklusB.ZyklusSumme)
            {
                A = zyklusA;
                B = zyklusB;
            }
            else
            {
                A = zyklusB;
                B = zyklusA;
            }

            long ergebnisZyklusSumme;
            if (A.ZyklusSumme % B.ZyklusSumme == 0)
            {
                ergebnisZyklusSumme = A.ZyklusSumme;
            }
            else
            {
                ergebnisZyklusSumme = A.ZyklusSumme * B.ZyklusSumme;
            }

            Tuple<long, long> offset = BestimmeOffsetZahlenZweierZyklen(A.ZyklusSumme, B.ZyklusSumme);

            Zyklus ergebnis = new Zyklus(ergebnisZyklusSumme, A.NumberOfElements * B.NumberOfElements);


            long tmp;
            long tmp2;
            for (int i = 0; i < A.NumberOfElements; i++)
            {
                for (int j = 0; j < B.NumberOfElements; j++)
                {
                    tmp = B.Elemente[j] - A.Elemente[i];
                    if (tmp == 0)
                    {
                        ergebnis.Elemente.Add(A.Elemente[i]);
                    }
                    else
                    {
                        tmp2 = ((offset.Item2 * tmp) % A.ZyklusSumme) * B.ZyklusSumme + B.Elemente[j];
                        tmp2 = (tmp2 + ergebnisZyklusSumme) % ergebnisZyklusSumme; // Zyklus positiv halten
                        if (tmp2 < 0)
                        {
                            int k = 0;
                            k++;
                        }

                        ergebnis.Elemente.Add(tmp2);
                    }
                }
            }

            return ergebnis;
        }


        static public void ZyklusVerkuerzen(ref Zyklus zyklus)
        {
            // Zyklus mit keinem oder einem Element werden auf sich selbst abgebildet
            if (zyklus.NumberOfElements < 2)
                return;

            // Prüfe, ob sich wiederholende Teilsequenzen finden lassen
            int alteAnzahlDerElemente = zyklus.NumberOfElements;

            // Erste Element
            int neueAnzahlDerElemente = 1;
            long differenz = zyklus.Elemente[1] - zyklus.Elemente[0];

            // Sucht ab dem zweiten Element nach einem Element, welches den gleichen Wert wie das Erste hat.
            // -> In dem Fall wird geschaut, ob alle folgenden Elemente gilt: E[X] == E[X+c] 
            for (int i = 2; i < alteAnzahlDerElemente; i++)
            {
                //if (zyklus.Elemente[i] != (zyklus.Elemente[i % neueZyklusLaenge]))
                //{
                //    neueZyklusLaenge = i+1;
                //}


                if (zyklus.Elemente[i] != (zyklus.Elemente[i - neueAnzahlDerElemente] + differenz))
                {
                    neueAnzahlDerElemente = i;
                    differenz = zyklus.Elemente[i] - zyklus.Elemente[0];
                }
            }

            // Letztes Element
            if ((zyklus.Elemente[0] + zyklus.ZyklusSumme) != (zyklus.Elemente[alteAnzahlDerElemente - neueAnzahlDerElemente] + differenz))
            {
                neueAnzahlDerElemente = alteAnzahlDerElemente;
            }

            // Prüfe, ob die Teilsequenzen als n-fache einen echten Zyklus bilden
            if (neueAnzahlDerElemente == alteAnzahlDerElemente)
            {
                return;
            }
            else
            {
                // Zyklus ist keine "sinnvoll teilbare" Teilmenge
                // Bsp: [7 5 8 8 7 5]
                if (alteAnzahlDerElemente % neueAnzahlDerElemente != 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"Zyklus komprimiert: {zyklus.ZyklusSumme} : {alteAnzahlDerElemente} -> {neueAnzahlDerElemente}");

                    zyklus.Truncate(neueAnzahlDerElemente);
                    //zyklus.Elemente.RemoveRange(neueAnzahlDerElemente, alteAnzahlDerElemente - neueAnzahlDerElemente);
                    zyklus.ZyklusSumme = (zyklus.ZyklusSumme * neueAnzahlDerElemente) / alteAnzahlDerElemente;
                    return;
                }
            }
        }

        /// <summary>
        /// Ziel ist es X und Y zu finden für die gilt: X*A % Y*B = ? R 1
        /// Zur Vereinfachung muss gelten: A > B
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        static public Tuple<long, long> BestimmeOffsetZahlenZweierZyklen(long a, long b)
        {
            if (a < b)
                throw new ApplicationException("Parameter 'A' muss immer größer sein, als Parameter 'B'.");

            // Zum besseren Verständnis die Parameter umbenennen
            long max = a;
            long min = b;

            // Bestimme den Rest für 1 * iMax % iMin
            long plus = max % min;
            long minus = min - plus;

            // Sicherheitsüberprüfung
            if (plus == 0 || minus == 0)
                throw new ApplicationException("Parameter 'B' ist teil von Parameter 'A'.");

            const long plusEins = 1;
            long minusEins = (min - 1);

            long i;
            long tmp = 0;
            for (i = 1; i < min; i++)
            {
                tmp = (tmp + plus) % min;

                if (tmp == plusEins || tmp == minusEins)
                {
                    break;
                }
            }

            long x = 0;

            if (tmp == plusEins)
            {
                x = i;
            }
            else
            {
                x = min - i;
            }

            return new Tuple<long, long>(x, (x * max) / min);
        }
    }
}
