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
        int[] Xs = ria();

        Array.Sort(Xs);
        
        HashSet<int> primeFactors = new HashSet<int>();
        foreach (int X in Xs)
        {
            int[] xPrimeFactors = PrimeFactors(X).ToArray();

            if(!primeFactors.Contains(xPrimeFactors[0])) {
                primeFactors.Add(xPrimeFactors[0]);
            }
        }

        BigInteger ans = 1;
        foreach (int primeFactor in primeFactors)
        {
            ans *= primeFactor;
        }

        List<List<int>> primeFactorss = new List<List<int>>();
        foreach (int X in Xs)
        {
            List<int> primeFactors_test = PrimeFactors(X).ToList();
            primeFactors_test.Insert(0, X);
            primeFactorss.Add(primeFactors_test);
        }
		
		Console.WriteLine(ans);
		Console.ReadLine();
	}

	public long SolveTest(List<int> Xs){
        HashSet<int> primeFactors = new HashSet<int>();
        foreach (int X in Xs)
        {
            int[] xPrimeFactors = PrimeFactors(X).ToArray();

            if(!primeFactors.Contains(xPrimeFactors[0])) {
                primeFactors.Add(xPrimeFactors[0]);
            }
        }

        long ans = 1;
        foreach (int primeFactor in primeFactors)
        {
            ans *= primeFactor;
        }

        List<List<int>> primeFactorss = new List<List<int>>();
        foreach (int X in Xs)
        {
            List<int> primeFactors_test = PrimeFactors(X).ToList();
            primeFactors_test.Insert(0, X);
            primeFactorss.Add(primeFactors_test);
        }

        return ans;
	}

    public void Solve2() {
		int N = ri();
        int[] Xs = ria();

        for (long i = 1; i < long.MaxValue; i++)
        {
            bool isAllSo = false;
            foreach (int X in Xs)
            {
                long gcd = Gcd(i, X);
                if(gcd == 1) {
                    isAllSo = true;
                    break;
                }
            }

            if(!isAllSo) {
                Console.WriteLine(i);
		        Console.ReadLine();
                return;
            }
        }
    }
    public long Solve2Test(List<int> Xs) {
        for (long i = 1; i < long.MaxValue; i++)
        {
            bool isAllSo = false;
            foreach (int X in Xs)
            {
                long gcd = Gcd(i, X);
                if(gcd == 1) {
                    isAllSo = true;
                    break;
                }
            }

            if(!isAllSo)  return i;
        }

        return -1;
    }

    public void Test() {
		int N = 0;
        List<int> Xs = new List<int>();

        for (int i = 50; i >= 2; i--)
        {
            N++;
            Xs.Add(i);

            long solveTest = SolveTest(Xs);
            long solve2Test = Solve2Test(Xs);
        }
    }    
    
    public void TestWithInput() {
        while(true) {
            int[] Xs = ria();

            Console.WriteLine("Solve : " + SolveTest(Xs.ToList()));
            Console.WriteLine("Solve2: " + Solve2Test(Xs.ToList()));
        }
    }

    static long Gcd(long m, long n)
    {
        if (n == 0) return m;
        return Gcd(n, m % n);
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
    
    //深さ優先探索を行う
    //listから重複無しで任意の個数選んだものがjudge関数によって判定される条件に合致するかを調べる
    //※listの長さはn以上である必要がある
    //n: 深さの終わり
    //list: 探索対象の配列
    //i: 現在選んだ個数
    //num: 現在の値
    //judge: 条件判定
    //toNext(i, num, next): 次のもの(next)を選ぶ時にnumに加える操作
    public static bool DepthFirstSearch(int n, List<int> list, int i, int num, Func<int, bool> judge, Func<int, int, int, int> toNext) {
        if(i == n) {
            return judge(num);
        }

        if(DepthFirstSearch(n, list, i + 1, num, judge, toNext)) return true;

        if(DepthFirstSearch(n, list, i + 1, toNext(i, num, list[i]), judge, toNext)) return true;

        return false;
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
    

    //順列をすべて列挙する
    //使い方 var patterns = Enumerable.Range(1, N - 1).Perm().Select(x => x.ToArray()).ToArray();
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
}
