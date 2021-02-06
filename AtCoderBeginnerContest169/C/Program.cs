using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] AB = Console.ReadLine().Split(' ');
            ulong A = ulong.Parse(AB[0]);
            decimal B = decimal.Parse(AB[1]);
            ulong ans = (ulong)((decimal)(A * B));
            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
