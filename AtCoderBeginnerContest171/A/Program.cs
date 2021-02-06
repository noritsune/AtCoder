using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            string alpha = Console.ReadLine();
            string ans = "A";
            if (!Char.IsUpper(alpha[0])) ans = "a";

            Console.WriteLine(ans);
            Console.ReadLine();
        }
    }
}
