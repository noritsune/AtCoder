using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] A_V = Console.ReadLine().Split(' ');
            String[] B_W = Console.ReadLine().Split(' ');
            long A = long.Parse(A_V[0]);
            long V = long.Parse(A_V[1]);
            long B = long.Parse(B_W[0]);
            long W = long.Parse(B_W[1]);
            long T = long.Parse(Console.ReadLine());
            String ans = "NO";

            if(V > W){
                long Kei = Math.Abs(V * T);
                long Doro = Math.Abs(W * T);
                if(Math.Abs(Kei - Doro) >= Math.Abs(A - B)) ans = "YES";
            }

            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
