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
		string S = rs();

        if(S.Length < 3) {
            Console.WriteLine(int.Parse(S) % 8 == 0 ? "Yes" : "No");
            Console.ReadLine();
            return;
        }

        int[][] combis = Combination.Enumerate(generateSuretsu(S.Length), 3, withRepetition:false).ToArray();
        foreach (int[] combi in combis)
        {
            List<int[]> perms = GetAllPermutation(combi);
            foreach (int[] perm in perms)
            {
                if(isHachi(S[perm[0]], S[perm[1]], S[perm[2]])) {
                    Console.WriteLine("Yes");
                    Console.ReadLine();
                    return;
                }
            }
        }
		Console.WriteLine("No");
		Console.ReadLine();
	}

    public static bool isHachi(char a, char b, char c) {
        int num = (int)a + (int)b * 10 + (int)c * 100;
        for (int i = 112; i <= num; i += 8)
        {
            if(i == num) return true;
        }
        return false;
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
    public static bool NextPermutation(int[] array)
    {
        var size = array.Length;
        var ok = false;

        //array[i]<array[i+1]を満たす最大のiを求める
        int i = size - 2;
        for (; 0 <= i; i--)
        {
            if (array[i] < array[i + 1])
            {
                ok = true;
                break;
            }
        }

        //全ての要素が降順の場合、NextPermutationは存在しない
        if (ok == false) return false;

        //array[i]<array[j]を満たす最大のjを求める
        int j = size - 1;
        for (; ; j--)
        {
            if (array[i] < array[j]) break;
        }

        //iとjの要素をswapする
        var tmp = array[i];
        array[i] = array[j];
        array[j] = tmp;

        //i以降の要素を反転させる
        Array.Reverse(array, i + 1, size - (i + 1));

        return true;
    }
    //引数の配列を並び替えて得られるすべての組み合わせを返す
    public static List<int[]> GetAllPermutation(int[] array)
    {
        var size = array.Length;
        var res = new List<int[]>();
        do
        {
            var copy = new int[size];
            array.CopyTo(copy, 0);
            res.Add(copy);
        } while (NextPermutation(array));

        return res;
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
