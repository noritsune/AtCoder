using System;
using System.Linq;
using System.Collections.Generic;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong N = ulong.Parse(Console.ReadLine());
            const int a = 97;
            
            string ans = "";
            List<ulong> int_names = new List<ulong>();
            ulong tmpN = N - 26;
            
            ulong k = N;
            ulong x = k;
            do {
                x--;
                ulong q = 0;
                if(x != 0) q = x / 26;
                ulong r = x % 26;
                int_names.Add(r);
                x = q;
            } while(x != 0);

            int_names.Reverse();
            foreach(int int_name in int_names) ans += (char)(a + int_name);

            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
