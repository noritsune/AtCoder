using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            String S = Console.ReadLine();
            String T = Console.ReadLine();
            String answer = "No";
            String Tdash = T.Substring(0, T.Length - 1);
            if(Tdash == S){
                answer = "Yes";
            }
            Console.WriteLine(answer);
            Console.ReadLine();
        }
    }
}
