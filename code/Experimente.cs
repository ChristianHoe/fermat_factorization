using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fermat
{
    public class Experimente
    {
        private void button1_Click(object sender, EventArgs e)
        {
            double a = 1777;
            double b = 13;

            double n = a * b;

            double m = Math.Floor(Math.Sqrt(n));


            //MessageBox.Show(string.Format("a : {0}, b : {1}, n : {2}, m : {3}, mm : {4}", a, b, n, m, m * m));


            HashSet<Tuple<int, int>> result = X((int)n);

            bool found = false;
            int iterationCount = 0;
            double aa;
            double bb;

            int hitCount = 0;

            while (!found)
            {
                m++;
                double x = Math.Sqrt(m * m - n);
                aa = m + x;
                bb = m - x;

                while (aa < 1000)
                {
                    aa *= 10;
                }
                int ia = (int)aa;

                while (bb < 1000)
                {
                    bb *= 10;
                }
                int ib = (int)bb;

                bool hit = false;

                if (result.Contains(Tuple.Create(ia, ib)))
                {
                    hitCount++;
                    hit = true;
                }
                if (result.Contains(Tuple.Create(ib, ia)))
                {
                    hitCount++;
                    hit = true;
                }


                if (!hit)
                {
                    //int i;
                }

                if (x == Math.Truncate(x))
                {
                    // MessageBox.Show(string.Format("{0}, {1}", result.Contains(Tuple.Create(ia, ib)), result.Contains(Tuple.Create(ib, ia))));
                    // MessageBox.Show(string.Format("Got it : {0} - Iterations :  {1}; Hits : {5}; Result {2}*{3}={4}", x, iterationCount, m + x, m - x, m * m - x * x, hitCount));
                    Console.WriteLine("{0}, {1}", result.Contains(Tuple.Create(ia, ib)), result.Contains(Tuple.Create(ib, ia)));
                    Console.WriteLine(("Got it : {0} - Iterations :  {1}; Hits : {5}; Result {2}*{3}={4}", x, iterationCount, m + x, m - x, m * m - x * x, hitCount));
                    break;
                }

                iterationCount++;
            }
        }

        HashSet<Tuple<int, int>> X(int ab)
        {
            HashSet<Tuple<int, int>> result = new HashSet<Tuple<int, int>>();

            // computes 2 digits
            int abShort = ab;
            while (abShort > 99)
            {
                abShort /= 10;
            }

            int ij;
            int first;
            int kicker;
            int tmp;
            for (int i = 1000; i < 10000; i++)
            {
                for (int j = i; j < 10000; j++)
                {
                    ij = i * j;
                    tmp = ij / ((ij >= 10000000) ? 100000 : 10000);

                    // if (tmp > 999 || tmp < 100)
                    //     MessageBox.Show("Fehler");

                    kicker = tmp % 10;
                    first = tmp / 10;

                    if (first == abShort)
                    {
                        result.Add(Tuple.Create(i, j));
                        continue;
                    }

                    if (first == abShort - 1)
                    {
                        if (kicker == 9)
                            result.Add(Tuple.Create(i, j));
                    }

                }
            }
            //MessageBox.Show(result.Count.ToString());
            Console.WriteLine(result.Count.ToString());

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            X(1777 * 3);
        }
    }
}
