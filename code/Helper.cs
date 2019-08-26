﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Fermat
{
    public class Helper
    {
        static public BigInteger ErwarteteMengeZuUeberpruefenderElemente(BigInteger n)
        {
            // Recht willkürlich
            BigInteger kleinsterNichterwarteterTeiler = new BigInteger(19);

            Tuple<BigInteger, bool> startwertM = Wurzel(n);
            
            BigInteger endwertM = BigInteger.Divide(BigInteger.Add(BigInteger.Divide(n, kleinsterNichterwarteterTeiler), kleinsterNichterwarteterTeiler), 2);

            return BigInteger.Subtract(endwertM, startwertM.Item1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Rest vorhanden?</returns>
        static public Tuple<BigInteger, bool> Wurzel(BigInteger value)
        {
            BigInteger a = BigInteger.One;
            BigInteger b = (value >> 5) + 8;

            BigInteger m;
            while (b.CompareTo(a) >= 0)
            {
                m = BigInteger.Add(a, b) >> 1;
                if (BigInteger.Multiply(m, m).CompareTo(value) > 0)
                {
                    b = BigInteger.Subtract(m, BigInteger.One);
                }
                else
                {
                    a = BigInteger.Add(m, BigInteger.One);
                }
            }

            BigInteger sqrt = BigInteger.Subtract(a, BigInteger.One);

            Tuple<BigInteger, bool> result = new Tuple<BigInteger, bool>(sqrt, BigInteger.Pow(sqrt, 2).CompareTo(value) == 0);
            return result;
        }
    }
}
