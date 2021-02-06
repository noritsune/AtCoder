using System;
using System.Linq;
using System.Collections.Generic;

namespace B
{
    enum Dir {
        tate, yoko
    }

    public static class Program
    {
        static void Main(string[] args)
        {
            // string[] ABCD = Console.ReadLine().Split(' ');
            // int A = int.Parse(ABCD[0]);
            // int B = int.Parse(ABCD[1]);
            // int C = int.Parse(ABCD[2]);
            // int D = int.Parse(ABCD[3]);

            // bool[] source = {true, false, false, false};
            string[] source = {"A", "B", "B", "B"};
            var perm = source.Perm();
            var results = perm.Select(x => x.ToArray()).ToArray();

            var strResults = new List<string>();
            foreach(string[] result in results) {
                strResults.Add(String.Join("", result));
            }
            strResults = strResults.Distinct().ToList();
            
            var newResults = new List<List<char>>();
            foreach(string strResult in strResults) {
                // newResult.Add(strResult.Split(""));
                var tmp = new List<char>();
                for(int i = 0; i < strResult.Length; i++) tmp.Add(strResult[i]);
                newResults.Add(tmp);
            }

            foreach(List<char> newResult in newResults){
                foreach(char AOrB in newResult){
                    if(AOrB == 'A') {
                        
                    }
                    else if(AOrB == 'B') {
                        
                    }
                }
            }
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

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

        public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int? k = null)
        {
            if (k == null)
                k = items.Count();

            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var i = 0;
                foreach (var x in items)
                {
                    var xs = items.Where((_, index) => i != index);
                    foreach (var c in Perm(xs, k - 1))
                        yield return c.Before(x);

                    i++;
                }
            }
        }

        // 要素をシーケンスに追加するユーティリティ
        public static IEnumerable<T> Before<T>(this IEnumerable<T> items, T first)
        {
            yield return first;

            foreach (var i in items)
                yield return i;
        }

        public static T[,] ToTwoDimensionalArray<T>(this List<List<T>> tuples)
        {
            var list = tuples.ToList();
            T[,] array = null;
            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                var row = list[rowIndex];
                if (array == null)
                {
                    array = new T[list.Count, row.Count];
                }
                for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
                {
                    array[rowIndex, columnIndex] = row[columnIndex];
                }
            }
            return array;
        }
    }
}
