using System;
using System.Linq;
using System.Collections.Generic;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] NMK = Console.ReadLine().Split(' ');
            int N = int.Parse(NMK[0]);
            int M = int.Parse(NMK[1]);
            int K = int.Parse(NMK[2]);

            string[] strAs = Console.ReadLine().Split(' ');
            List<int> As = new List<int>();
            As.Add(0);
            foreach(string strA in strAs) As.Add(As.Last() + int.Parse(strA));

            string[] strBs = Console.ReadLine().Split(' ');
            List<long> Bs = new List<long>();
            Bs.Add(0);
            foreach(string strB in strBs) Bs.Add(Bs.Last() + long.Parse(strB));

            int j = M;
            int ans = 0;
            for(int i = 0; i <= N; i++) {
                if(As[i] > K) break;

                while(Bs[j] > (long)(K - As[i])) j--;
                
                ans = Math.Max(ans, i + j);
            }
            
            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
