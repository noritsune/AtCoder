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
        // mySol.Test();
	}
}

class Sol{
	const int _mod = 1000000007;
	public void Solve(){
		int N = ri();

        long ans = 1;
        var nums = generateSuretsu(N + 1).Skip(N / 2 + 1).ToList();

        // for (int i = 0; i < nums.Count(); i++)
        // {
        //     int num = nums[i];
        //     for (int j = num * 2; j <= N; j += num)
        //     {
        //         nums.Remove(j);
        //     }
        // }

        foreach (var num in nums)
        {
            ans *= num;
        }

        Console.WriteLine(ans + 1);
        Console.ReadLine();
	}

    public void Test() {
        int N = ri();
        long ans = rl();

        for (int i = 2; i <= N; i++)
        {
            Console.WriteLine(ans + " % " + i + " = " + (ans % i));
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
     // 下限、上限が指定できるbool型配列
    class BoundedBoolArray {
        private BitArray _array;
        private int _lower;

        public BoundedBoolArray(int lower, int upper) {
            _array = new BitArray(upper - lower + 1);
            _lower = lower;
        }

        public bool this[int index] {
            get {
                return _array[index - _lower];
            }
            set {
                _array[index - _lower] = value;
            }
        }
    }
    IEnumerable<int> Primes() {
        // 2,3は既知の素数とする
        var primes = new List<int>() { 2, 3 };
        foreach (var p in primes)
            yield return p;

        // 4以上の整数から素数を列挙する。int.MaxValueを超えたときには対処していない
        int ix = 0;
        while (true) {
            int prime1st = primes[ix];
            int prime2nd = primes[++ix];
            // ふるい用の配列の下限、上限を求め、配列を確保する。
            var lower = prime1st * prime1st;
            var upper = prime2nd * prime2nd - 1;
            // ふるいは、[4:8], [9:24], [25:48], [49:120]... と変化する。
            // []内の数値は、配列の下限と上限
            var sieve = new BoundedBoolArray(lower, upper);

            // 求まっている素数を使い、ふるいに掛ける 
            foreach (var prime in primes.Take(ix)) {
                var start = (int)Math.Ceiling((double)lower / prime) * prime;
                for (int index = start; index <= upper; index += prime)
                    sieve[index] = true;
            }

            // ふるいに掛けられて残った値が素数。これを列挙する。
            // 併せて、求まった素数は、primesリストに記憶していく。
            // この素数が次のステップ以降で、ふるいに掛ける際に利用される。
            for (int i = lower; i <= upper; i++) {
                if (sieve[i] == false) {
                    primes.Add(i);
                    yield return i;
                }
            }
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
