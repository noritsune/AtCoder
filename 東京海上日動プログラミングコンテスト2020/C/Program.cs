using System;
using System.Linq;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] N_K = Console.ReadLine().Split(' ');
            String[] strAs = Console.ReadLine().Split(' ');
            int N = int.Parse(N_K[0]);
            int K = int.Parse(N_K[1]);
            int[] As = new int[N];

            bool isfinish = true;
            for(int i = 0; i < N; i++) {
                As[i] = int.Parse(strAs[i]);
                if(isfinish) {
                    if(As[i] < N) isfinish = false;
                }
            }

            int[] final = Enumerable.Repeat(N, N).ToArray();

            if(isfinish) {
                As = final.Clone() as int[];
            } else {
                for(int i = 0; i < K; i++) {
                    int[] lights = Enumerable.Repeat(0, N).ToArray();
                    for(int j = 0; j < N; j++) {
                        if(As[j] == 0) {
                            lights[j]++;
                        } else {
                            for(int k = Math.Max(0, j-As[j]); k < Math.Min(N, j+As[j]+1); k++){
                                lights[k]++;
                            }
                        }
                    }
                    As = lights.Clone() as int[];
                    if(As.SequenceEqual(final)) break;
                }
            }

            for(int i = 0; i < N; i++) strAs[i] = As[i].ToString();
            Console.WriteLine(String.Join(' ', strAs));
            Console.ReadLine();
        }
    }
}
