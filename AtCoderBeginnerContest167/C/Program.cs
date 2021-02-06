using System;
using System.Linq;
using System.Collections.Generic;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] NMX = Console.ReadLine().Split(' ');
            int N = int.Parse(NMX[0]);
            int M = int.Parse(NMX[1]);
            int X = int.Parse(NMX[2]);
            
            int[] Cs = new int[N];
            int[,] As = new int[N, M];

            for(int i = 0; i < N; i++) {
                String[] CandAs = Console.ReadLine().Split(' ');
                Cs[i] = int.Parse(CandAs[0]);
                for(int j = 0; j < M; j++) As[i, j] = int.Parse(CandAs[j + 1]);

            }
            
            var nums = Enumerable.Range(0, N).ToArray();
            int answer = -1;
            for(int k = 1; k <= N; k++){
                int[][] Is = Combination.Enumerate(nums, k, withRepetition:false).ToArray();
                foreach(int[] I in Is){
                    int tmpAnswer = -1;
                    int[] Ms = Enumerable.Repeat(0, M).ToArray();
                    for(int i = 0; i < I.Length; i++){
                        for(int j = 0; j < M; j++){
                            Ms[j] += As[I[i], j];
                        }
                    }
                    if(Ms.All(n => n >= X)){
                        tmpAnswer = 0;
                        for(int i = 0; i < I.Length; i++) tmpAnswer += Cs[I[i]];
                    }
                    if(tmpAnswer != -1) {
                        if(answer == -1) answer = tmpAnswer;
                        else if(answer != -1) answer = Math.Min(answer, tmpAnswer);
                    }
                }
            }

            Console.WriteLine(answer);
        }

        public static class Combination {
            public static IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items, int k, bool withRepetition) {
                if (k == 1) {
                    foreach (var item in items)
                        yield return new T[] { item };
                    yield break;
                }
                foreach (var item in items) {
                    var leftside = new T[] { item };

                    // item よりも前のものを除く （順列と組み合わせの違い)
                    // 重複を許さないので、unusedから item そのものも取り除く
                    var unused = withRepetition ? items : items.SkipWhile(e => !e.Equals(item)).Skip(1).ToList();

                    foreach (var rightside in Enumerate(unused, k - 1, withRepetition)) {
                        yield return leftside.Concat(rightside).ToArray();
                    }
                }
            }
        }
    }
}
