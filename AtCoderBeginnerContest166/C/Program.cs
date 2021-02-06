using System;
using System.Collections.Generic;
using System.Linq;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] NM = Console.ReadLine().Split(' ');
            int N = int.Parse(NM[0]);
            int M = int.Parse(NM[1]);
            List<int> H = new List<int>();
            String[] strH = Console.ReadLine().Split(' ');
            foreach(String inH in strH){
                H.Add(int.Parse(inH));
            }

            List<bool> isGoodObs = Enumerable.Repeat(true, N).ToList();
            for(int i=0; i<M; i++){
                String[] strLoad = Console.ReadLine().Split(' ');
                int A = int.Parse(strLoad[0])-1;
                int B = int.Parse(strLoad[1])-1;
                if(H[A] <= H[B]) isGoodObs[A] = false;
                if(H[A] >= H[B]) isGoodObs[B] = false;
            }
            Console.WriteLine(isGoodObs.Count(isGood => isGood == true));
            Console.ReadLine();
        }
    }
}
