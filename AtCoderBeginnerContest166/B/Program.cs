using System;
using System.Collections.Generic;
using System.Linq;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] NK = Console.ReadLine().Split(' ');
            int N = int.Parse(NK[0]);
            int K = int.Parse(NK[1]);
            List<int> children = Enumerable.Range(1, N).ToList();
            for(int i = 0; i < K; i++){
                Console.ReadLine();
                String[] strAs = Console.ReadLine().Split(' ');
                foreach(String strA in strAs){
                    children.Remove(int.Parse(strA));
                }
            }
            Console.WriteLine(children.Count);
        }
    }
}
