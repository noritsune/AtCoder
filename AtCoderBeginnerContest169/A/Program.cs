using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] AB = Console.ReadLine().Split(' ');
            int A = int.Parse(AB[0]);
            int B = int.Parse(AB[1]);
            Console.WriteLine(A * B);
            Console.ReadLine();
        }
    }
}
