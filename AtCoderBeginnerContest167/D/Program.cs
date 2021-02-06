using System;
using System.Collections.Generic;

namespace D
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] NK = Console.ReadLine().Split(' ');
            int N = int.Parse(NK[0]);
            ulong K = ulong.Parse(NK[1]);
            String[] strAs = Console.ReadLine().Split(' ');
            List<int> As = new List<int>();
            foreach(String strA in strAs) As.Add(int.Parse(strA));
            
            int answer = 1;
            for(uint i = 0; i < K; i++){
                answer = As[answer - 1];
            }

            Console.WriteLine(answer);
            Console.ReadLine();
        }
    }
}
