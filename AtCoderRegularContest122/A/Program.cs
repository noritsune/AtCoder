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
        int[] As = ria();

        IEnumerable<bool[]> fomulas = BitFullSearch(N - 1);

        IEnumerable<bool[]> goodFomulas = fomulas.Where(fomula => {
            bool last = fomula.First();
            for (int i = 1; i < fomula.Count(); i++)
            {
                bool current = fomula[i];
                if(last && current) return false;
                last = current;
            }
            return true;
        });

        int[] plusCnt = new int[N];
        plusCnt[0] = goodFomulas.Count();
        foreach (bool[] goodFomula in goodFomulas)
        {
            for (int i = 0; i < goodFomula.Length; i++)
            {
                if(goodFomula[i])   plusCnt[i + 1]--;
                else                plusCnt[i + 1]++;
            }
        }

        long ans = 0;
        for (int i = 0; i < N; i++)
        {
            ans += As[i] * (long)plusCnt[i];
            ans %= _mod;
        }
		
		Console.WriteLine(ans);
		Console.ReadLine();
	}

    public void SolveTest(int n){
        for (int N = 2; N <= n; N++)
        {
            IEnumerable<bool[]> fomulas = BitFullSearch(N - 1);

            IEnumerable<bool[]> goodFomulas = fomulas.Where(fomula => {
                bool last = fomula.First();
                for (int i = 1; i < fomula.Count(); i++)
                {
                    bool current = fomula[i];
                    if(last && current) return false;
                    last = current;
                }
                return true;
            });

            int[] plusCnt = new int[N];
            int[] minusCnt = new int[N];
            plusCnt[0] = goodFomulas.Count();
            foreach (bool[] goodFomula in goodFomulas)
            {
                for (int i = 0; i < goodFomula.Length; i++)
                {
                    if(goodFomula[i])   minusCnt[i + 1]++;
                    else                plusCnt[i + 1]++;
                }
            }

            string plusStr = "";
            string minusStr = "";
            string totalStr = "";
            for (int i = 0; i < N; i++)
            {
                plusStr += plusCnt[i] + " ";
                minusStr += minusCnt[i] + " ";
                totalStr += (plusCnt[i] - minusCnt[i]) + " ";
            }
            
            Console.WriteLine("N = " + N);
            Console.WriteLine("plusCnt:  " + plusStr);
            Console.WriteLine("minusCnt: " + minusStr);
            Console.WriteLine("totalCnt: " + totalStr);
            Console.ReadLine();
        }

		Console.ReadLine();
	}
    public void SolveTest2(int n){
        for (int N = 2; N <= n; N++)
        {
            int[] plusCnt = new int[N];
            double root5 = Math.Sqrt(5);
            plusCnt[0] = (int)((Math.Pow((1 + root5) / 2, (N + 1)) - Math.Pow((1 - root5) / 2, (N + 1))) / root5);
            plusCnt[1] = (int)((Math.Pow((1 + root5) / 2, N) - Math.Pow((1 - root5) / 2, N)) / root5);
        }

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

    static IEnumerable<bool[]> BitFullSearch(int n)
    {
        if (n <= 0)
        {
            yield return new bool[] { };
            yield break;
        }

        for (int i = 0; i < Math.Pow(2, n); i++)
        {
            var array = new bool[n];
            for (int j = 0; j < n; j++)
            {
                var targetBit = (i >> j) & 1;
                if (targetBit == 1) array[j] = true;
            }

            yield return array;
        }
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
