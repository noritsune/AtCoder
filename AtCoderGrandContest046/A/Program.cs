using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            int X = int.Parse(Console.ReadLine());

            int currentDig = 0;
            int cnt = 0;
            do {
                currentDig += X;
                cnt++;
            } while(currentDig % 360 != 0);

            Console.WriteLine(cnt);
            Console.ReadLine();
        }
    }
}
