using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            string S = Console.ReadLine();
            string T = Console.ReadLine();

            int cnt = 0;
            for(int i = 0; i < S.Length; i++) {
                if(S[i] != T[i]) cnt++;
            }

            Console.WriteLine(cnt);
            Console.ReadLine();
        }
    }
}
