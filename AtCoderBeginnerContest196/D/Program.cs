﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

class Util{
	static void Main(){
		Sol mySol =new Sol();
		mySol.Solve2();
	}
}

enum Dir {
    top,
    right,
    bottom,
    left,
}

class Zahyo {
    public int _x = 0;
    public int _y = 0;

    public Zahyo(int x, int y) {
        _x = x;
        _y = y;
    }
}

class Sol{
	const int _mod = 1000000007;

    public void Solve2() {
		int[] HWAB = ria();
        int H = HWAB[0];
        int W = HWAB[1];
        int A = HWAB[2];
        int B = HWAB[3];

        // 各マスの辺に畳の境目が無いか
        bool[,] isAH = new bool[H    , W - 1];
        bool[,] isAW = new bool[H - 1, W    ];
        
    }

	public void Solve(){
		int[] HWAB = ria();
        int H = HWAB[0];
        int W = HWAB[1];
        int A = HWAB[2];
        int B = HWAB[3];

        if(A == 0) {
            Console.WriteLine(1);
            Console.ReadLine();
        }

        // B畳の配置パターンを生成
        int[] suuretsu = generateSuretsu(H * W);
        int[][] bHaichiss = Combination.Enumerate(suuretsu, B, withRepetition:false).ToArray();

        long ans = 0;
        foreach (int[] bHaichs in bHaichiss)
        {
            bool[,] isTatami = new bool[H,W];

            // B畳を配置
            foreach (int bHaich in bHaichs)
            {
                isTatami[bHaich / H, bHaich % W] = true;
            }

            // A畳を配置
            HashSet<HashSet<HashSet<Zahyo>>> aZahyoSetSetSet = new HashSet<HashSet<HashSet<Zahyo>>>();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine(aZahyoSetSetSet.Count + "パターン目");

                HashSet<HashSet<Zahyo>> aZahyoSetSet = new HashSet<HashSet<Zahyo>>();
                int aTatamiCnt = 0;
                for (int h = 0; h < H; h++)
                {
                    for (int w = 0; w < W; w++)
                    {
                        if (isTatami[h,w]) continue;
                        foreach (Dir dir in Enum.GetValues(typeof(Dir)))
                        {
                            int[] hwOffset = getOffset(dir);
                            int hDir = h + hwOffset[0];
                            int wDir = w + hwOffset[1];

                            if (    hDir < 0 || hDir >= H
                                ||  wDir < 0 || wDir >= W
                                ||  isTatami[hDir,wDir] ) continue;

                            isTatami[h, w] = true;
                            isTatami[hDir,wDir] = true;

                            
                            HashSet<Zahyo> aZahyoSet = new HashSet<Zahyo>();
                            aZahyoSet.Add(new Zahyo(h, w));
                            aZahyoSet.Add(new Zahyo(hDir, wDir));
                            aZahyoSetSet.Add(aZahyoSet);

                            Console.WriteLine(++aTatamiCnt + "枚目");
                            printTatamis(isTatami);
                            
                            break;
                        }
                    }
                }

                aZahyoSetSetSet.Add(aZahyoSetSet);
            }
            
            ans += aZahyoSetSetSet.Count();
        }
		
		Console.WriteLine(ans);
		Console.ReadLine();
	}

    int[] getOffset(Dir dir) {
        switch (dir)
        {
            case Dir.top:
                return new int[2] {-1,  0};
            case Dir.right:
                return new int[2] { 0,  1};
            case Dir.bottom:
                return new int[2] { 1,  0};
            case Dir.left:
                return new int[2] { 0, -1};
            default:
                return new int[2] { 0,  0};
        }
    }

    void printTatamis(bool[,] isTatami) {
        for (int i = 0; i < isTatami.GetLength(0); i++)
        {
            for (int j = 0; j < isTatami.GetLength(1); j++)
            {
                string masu = isTatami[i, j]
                    ? "#"
                    : ".";
                Console.Write(masu);
            }
            Console.Write("\n");
        }
        Console.Write("\n");
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
