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
	const long MOD = 1000000007;

	public void Solve(){
		long N = ri();

		// if(N == 1) 
		// {
		// 	Console.WriteLine(0);
		// 	Console.ReadLine();
		// 	return;
		// } 
		// else if(N == 2) 
		// {
		// 	Console.WriteLine(2);
		// 	Console.ReadLine();
		// 	return;
		// }

		// long one = (N * (N - 1)) % MOD;
		// long modPow = modpow(10, N - 2);
		// Console.WriteLine((N * (N - 1)) % MOD * modpow(10, N - 2) % MOD);
		Console.WriteLine((modpow(10, N) - 2 * modpow(9, N) + modpow(8, N)) % MOD);
		Console.ReadLine();
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

	static long modpow(long a, long b, long mod = MOD) {
	BigInteger res = 1;
	for (a %= mod; b > 0; a = a * a % mod, b >>= 1)
		if ((b & 1) > 0) res = res * a % mod;
	return (long)res;
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
