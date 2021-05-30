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

class IndexWithPos {
    public int index;
    public int x;
    public int y;

    public IndexWithPos(int index, int x, int y) {
        this.index = index;
        this.x = x;
        this.y = y;
    }
}

class Sol{
	const int _mod = 1000000007;
    public void Solve(){
		int N = ri();
        List<int[]> xys = new List<int[]>();
        for (int i = 0; i < N; i++) xys.Add(ria());

        List<IndexWithPos> indexWithPoses = new List<IndexWithPos>();
        for (int i = 0; i < N; i++)
        {
            indexWithPoses.Add(new IndexWithPos(i, xys[i][0], xys[i][1]));
        }
        
        indexWithPoses.Sort((a, b) => a.x - b.x);

        List<int[]> candidates = new List<int[]>();

        IndexWithPos gPoses = indexWithPoses.Last();
        IndexWithPos lPoses = indexWithPoses.First();
        candidates.Add(new int[]{gPoses.x - lPoses.x, gPoses.index, lPoses.index});

        gPoses = indexWithPoses[N - 2];
        candidates.Add(new int[]{gPoses.x - lPoses.x, gPoses.index, lPoses.index});
        
        gPoses = indexWithPoses.Last();
        lPoses = indexWithPoses[1];
        candidates.Add(new int[]{gPoses.x - lPoses.x, gPoses.index, lPoses.index});
        

        
        indexWithPoses.Sort((a, b) => a.y - b.y);

        gPoses = indexWithPoses.Last();
        lPoses = indexWithPoses.First();
        candidates.Add(new int[]{gPoses.y - lPoses.y, gPoses.index, lPoses.index});

        gPoses = indexWithPoses[N - 2];
        candidates.Add(new int[]{gPoses.y - lPoses.y, gPoses.index, lPoses.index});
        
        gPoses = indexWithPoses.Last();
        lPoses = indexWithPoses[1];
        candidates.Add(new int[]{gPoses.y - lPoses.y, gPoses.index, lPoses.index});

        
        List<int> dirs = new List<int>();
        List<HashSet<int>> indexPairs = new List<HashSet<int>>();
        foreach (int[] candidate in candidates)
        {
            int dir     = candidate[0];
            int indexA  = candidate[1];
            int indexB  = candidate[2];

            HashSet<int> indexPair = new HashSet<int>();
            indexPair.Add(indexA);
            indexPair.Add(indexB);

            if(indexPairs.Contains(indexPair)) {
                continue;
            }

            indexPairs.Add(indexPair);
            dirs.Add(dir);
        }

        dirs.Sort((a, b) => b - a);
		
		Console.WriteLine(dirs[1]);
		Console.ReadLine();
	}

	public void SolveTest(){
		int N = ri();
        List<int[]> xys = new List<int[]>();
        for (int i = 0; i < N; i++) xys.Add(ria());

        int[] nums = generateSuretsu(N);
        int[][] houseCombis = Combination.Enumerate(nums, 2, withRepetition:false).ToArray();

        List<int> distances = new List<int>();
        foreach (int[] houseCombi in houseCombis)
        {
            int[] house1 = xys[houseCombi[0]];
            int[] house2 = xys[houseCombi[1]];

            int distance = CalcDistance(house1[0], house1[1], house2[0], house2[1]);
            distances.Add(distance);
        }

        distances.Sort((a, b) => b - a);
		
		Console.WriteLine(distances[1]);
		Console.ReadLine();
	}

    static int CalcDistance(int x1, int y1, int x2, int y2) {
        int deltaX = Math.Abs(x1 - x2);
        int deltaY = Math.Abs(y1 - y2); 
        return Math.Max(deltaX, deltaY);
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
