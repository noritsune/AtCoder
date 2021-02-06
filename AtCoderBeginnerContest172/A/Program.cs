using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());

            Console.WriteLine(a + Math.Pow(a, 2) + Math.Pow(a, 3));
            Console.ReadLine();
        }
    }
}
