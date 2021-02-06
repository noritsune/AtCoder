using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

class Util{
	static void Main(){
		Sol mySol =new Sol();
		mySol.Solve();
	}
}

class Sol{
	const int _mod = 1000000007;
	public void Solve(){
		int N = ri();
        List<int[]> xys = new List<int[]>(); 
        for (int i = 0; i < N; i++) xys.Add(ria());

        int[][] patterns = Combination.Enumerate(generateSuretsu(N), 3, withRepetition:false).ToArray();
        foreach (int[] pattern in patterns)
        {
            int[] A = xys[pattern[0]];
            int[] B = xys[pattern[1]];
            int[] C = xys[pattern[2]];
            
            double delta = Math.Pow(10, -10);
            // if(distance(A, B) + distance(A, C) == distance(B, C) ||
            //    distance(A, B) + distance(B, C) == distance(A, C) ||
            //    distance(A, C) + distance(B, C) == distance(A, B))
            // {
            // if(isStraight(A, B, C) || isStraight(A, C, B) || isStraight(B, A, C) || isStraight(B, C, A) || isStraight(C, A, B) || isStraight(C, B, A) ) {
            if(isNearyEqual(distance(A, B) + distance(A, C), distance(B, C), delta) ||
               isNearyEqual(distance(A, B) + distance(B, C), distance(A, C), delta) ||
               isNearyEqual(distance(A, C) + distance(B, C), distance(A, B), delta))
            {
                Console.WriteLine("Yes");
		        Console.ReadLine();
                return;
            }
        }
		
		Console.WriteLine("No");
		Console.ReadLine();
	}

    public static bool isNearyEqual(double a, double b, double delta) {
        return b >= a - delta  && b <= a + delta;
    }

    public static bool isStraight(int[] a, int[] b, int[] c) {
        int deltaX_ab = b[0] - a[0];

        if(deltaX_ab == 0) {
            return c[1] == b[1] * b[1] - a[1] && b[0] == c[0];
        }

        double tilt = (b[1] - a[1]) / deltaX_ab;
        int deltaX_bc = c[0] - b[0];
        double c_y_expected = b[1] + deltaX_bc * tilt;
        
        return c[1] == c_y_expected;
    }

    public static double distance(int[] a, int[] b) {
        return Math.Sqrt(Math.Pow(b[0] - a[0], 2) + Math.Pow(b[1] - a[1], 2));
    }

	static String rs(){return Console.ReadLine();}
	static int ri(){return int.Parse(Console.ReadLine());}
	static long rl(){return long.Parse(Console.ReadLine());}
	static double rd(){return double.Parse(Console.ReadLine());}
	static BigInteger rb(){return BigInteger.Parse(Console.ReadLine());}
	static String[] rsa(char sep=' '){return Console.ReadLine().Split(sep);}
	static int[] ria(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>int.Parse(e));}
	static long[] rla(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>long.Parse(e));}
	static double[] rda(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>double.Parse(e));}
	static BigInteger[] rba(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>BigInteger.Parse(e));}
    static int[] generateNums(int num, int N){return Enumerable.Repeat(num, N).ToArray();}
    static int[] generateSuretsu(int N){return Enumerable.Range(0, N).ToArray();}
    public static IEnumerable<int> PrimeFactors(int n)
    {
        int i = 2;
        int tmp = n;

        while (i * i <= n) //※1
        {
            if(tmp % i == 0){
                tmp /= i;
                yield return i;
            }else{
                i++;
            }
        }
        if(tmp != 1) yield return tmp;//最後の素数も返す
    }
}

public static class Combination {
	//使い方:int[][] Is = Combination.Enumerate(nums, k, withRepetition:false).ToArray();
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
