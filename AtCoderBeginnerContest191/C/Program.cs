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

class Zahyo {
    public int x = 0;
    public int y = 0;
    public Zahyo(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public static Zahyo operator+(Zahyo a, Zahyo b) {
        return new Zahyo(a.x + b.x, a.y + b.y);
    }
    
    public override bool Equals(Object obj) {
        if(obj == null) return false;

        Zahyo other = (Zahyo)obj;
        return other.x == this.x && other.y == this.y;
    }
}

class Sol{
	const int _mod = 1000000007;
    List<char[]> _Shw = new List<char[]>();
    Zahyo _startZahyo;
	public void Solve(){
		int[] HW = ria();
        int H = HW[0];
        int W = HW[1];
        for (int i = 0; i < H; i++) {
            string row = rs();
            char[] chars = new char[W];
            for (int j = 0; j < W; j++) chars[j] = row[j];
            _Shw.Add(chars);
        }

        // 開始地点は一番左上の黒
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                Zahyo zahyo = new Zahyo(j, i);
                if(IsWhiteSquare(zahyo)) continue;
                
                _startZahyo = zahyo;
                break;
            }
            if(_startZahyo != null) break;
        }

        int edgeCnt = 1;
        Queue<Zahyo> q = new Queue<Zahyo>();
        q.Enqueue(_startZahyo);
        Zahyo nextOffset = ExploreNextOffset(_startZahyo);
        while(q.Count > 0) {
            Zahyo nowZahyo = q.Dequeue();
            _Shw[nowZahyo.y][nowZahyo.x] = '.';
            Zahyo nextZahyo = nowZahyo + nextOffset;

            // 探索終了
            if(nextZahyo.Equals(_startZahyo)) break;
            
            //折れ曲がるパターン
            if(IsWhiteSquare(nextZahyo)) {
                nextOffset = ExploreNextOffset(nowZahyo);
                edgeCnt++;
            }

            q.Enqueue(nowZahyo + nextOffset);
        }

		Console.WriteLine(edgeCnt);
		Console.ReadLine();
	}

    bool IsWhiteSquare(Zahyo zahyo) {
        return _Shw[zahyo.y][zahyo.x] == '.';
    }

    bool isValiedZahyo(Zahyo zahyo) {
        return  zahyo.x >= 0 && zahyo.x < _Shw[0].Length &&
                zahyo.y >= 0 && zahyo.y < _Shw.Count;
    }

    int StepToNextBlack(Zahyo nowZahyo, Zahyo nowOffset, int edgeCnt) {
        Zahyo nextZahyo = nowZahyo + nowOffset;

        // 探索終了
        if(nextZahyo == _startZahyo) return edgeCnt;
        
        //辺がそのまま伸びているパターン
        if(!IsWhiteSquare(nextZahyo)) StepToNextBlack(nextZahyo, nowOffset, edgeCnt);

        //折れ曲がるパターン
        else {
            Zahyo newOffset = ExploreNextOffset(nowZahyo);
            StepToNextBlack(nowZahyo + newOffset, newOffset, ++edgeCnt);
        }

        return -1;
    }

    // 隣接する黒を探索する右回りで探索して初めに見つかったものを返す
    Zahyo ExploreNextOffset(Zahyo nowZahyo) {
        Zahyo[] offsets = new Zahyo[]{
            new Zahyo(1, 0),
            new Zahyo(1, 1),
            new Zahyo(0, 1),
            new Zahyo(-1, 1),
            new Zahyo(-1, 0),
            new Zahyo(-1, -1),
            new Zahyo(0, -1),
            new Zahyo(1, -1)
        };

        foreach (var offset in offsets)
        {
            Zahyo nextZahyo = nowZahyo + offset;
            if(!isValiedZahyo(nextZahyo)) continue;
            if(IsWhiteSquare(nextZahyo)) continue;

            return offset;
        }

        return null;
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
