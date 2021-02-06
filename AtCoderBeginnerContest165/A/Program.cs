using System;

namespace AtCoderBeginnerContest165
{
    class Program
    {
        static void Main(string[] args)
        {
            int K = int.Parse(Console.ReadLine());
            String[] AandB= Console.ReadLine().Split(' ');
            int A = int.Parse(AandB[0]);
            int B = int.Parse(AandB[1]);
            
            for(int i = 0; K*i <= B; i++){
               if(K*i >= A && K*i <= B){
                    Console.WriteLine("OK");
                    return;
               }      
            }
            Console.WriteLine("NG");
        }
    }
}
