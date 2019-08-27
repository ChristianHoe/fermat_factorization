using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fermat
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            //  4339,  4349,  4357,  4363,  4373,  4391,
            // 96763, 96769, 96779, 96787, 96797, 96799, 96821
            BigInteger n =  4339 * 96763;
            
            var result = FermatExtended.Run(n, new int[] { 5, 7, 11, 13, 17, 16, 19, 23, 27, 29 });

            Console.WriteLine("Lösung lautet: {0} * {1} = {2}", result.A, result.B, n );

        }
    }
}
