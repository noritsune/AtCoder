using System;

namespace AtCoderBeginnerContest165
{
    class Program
    {
        static void Main(string[] args)
        {
            Int64 X = Int64.Parse(Console.ReadLine());
            
            Int64 Y = 100;
            int year = 0;
            while(Y < X){
                year++; 
                Y = (Int64)(Y * 1.01);
            }
            Console.WriteLine(year);
        }
    }
}
