using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] ABCK = Console.ReadLine().Split(' ');
            int A = int.Parse(ABCK[0]);
            int B = int.Parse(ABCK[1]);
            int C = int.Parse(ABCK[2]);
            int K = int.Parse(ABCK[3]);
            int answer = 0;

            int tmp = A - K;
            if(tmp < 0){
                K -= A;
                answer += A;
                tmp = B - K;
                if(tmp < 0){
                    K -= B;
                    answer -= K;
                }
                else {}
            }
            else answer += K;
            Console.WriteLine(answer);
        }
    }
}
