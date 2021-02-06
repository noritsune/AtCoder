using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] NK = Console.ReadLine().Split(' ');
            string[] strPs = Console.ReadLine().Split(' ');
            int N = int.Parse(NK[0]);
            int K = int.Parse(NK[1]);
            int[] ps = new int[N];
            for(int i = 0; i < N; i++) ps[i] = int.Parse(strPs[i]);

            Array.Sort(ps);

            int ans = 0;
            for(int i = 0; i < K; i++) ans += ps[i];

            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
