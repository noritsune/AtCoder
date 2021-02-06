using System;
using System.Numerics;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            String[] strAs = Console.ReadLine().Split(' ');
            BigInteger  ans = 1;
            for(int i = 0; i < strAs.Length; i++) {
                if(strAs[i] == "0") {
                    Console.WriteLine("0");
                    return;
                }
            }

            for(int i = 0; i < strAs.Length; i++) {
                ans = ans * BigInteger .Parse(strAs[i]);
                if(ans > 1000000000000000000) {
                    Console.WriteLine("-1");
                    return;
                }
            }
            
            Console.WriteLine(ans.ToString());
        }
    }
}
