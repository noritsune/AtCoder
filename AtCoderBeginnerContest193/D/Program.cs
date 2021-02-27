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

class CardSet {
    public int _takaCard;
    public int _aokiCard;

    public CardSet(int takaCard, int aokiCard) {
        this._takaCard = takaCard;
        this._aokiCard = aokiCard;
    }
}

class Sol{
	const int _mod = 1000000007;
	public void Solve(){
		int K = ri();
        string S = rs();
        string T = rs();

        List<int> takaHand = StringHandToIntArray(S);
        List<int> aokiHand = StringHandToIntArray(T);

        //各カードの残数計算
        int[] remainCardCnts_beforeLastTurn = new int[10];
        for (int i = 1; i <= 9; i++) {
            int takaPickedCnt = takaHand.Count((num) => num == i);
            int aokiPickedCnt = aokiHand.Count((num) => num == i);
            remainCardCnts_beforeLastTurn[i] = K - takaPickedCnt - aokiPickedCnt;
        }

        List<CardSet> takaWinCardSets = new List<CardSet>();
        for (int takaCard = 1; takaCard <= 9; takaCard++){
            int[] remainCardCnts_afterLastTurn = remainCardCnts_beforeLastTurn.Clone() as int[];

            remainCardCnts_afterLastTurn[takaCard]--;
            if(remainCardCnts_afterLastTurn[takaCard] < 0) continue;

            for (int aokiCard = 1; aokiCard <= 9; aokiCard++)
            {
                remainCardCnts_afterLastTurn[aokiCard]--;
                if(remainCardCnts_afterLastTurn[aokiCard] < 0) continue;

                List<int> takaExHand = new List<int>(takaHand);
                List<int> aokiExHand = new List<int>(aokiHand);
                takaExHand.Add(takaCard);
                aokiExHand.Add(aokiCard);
                
                int takaExScore = CalcScoreByHand(takaExHand);
                int aokiExScore = CalcScoreByHand(aokiExHand);
                if(takaExScore <= aokiExScore) continue;

                takaWinCardSets.Add(new CardSet(takaCard, aokiCard));
            }
        }

        // 確率計算
        int remainCardCnt = 9 * K - 8;
        double ans = 0;
        foreach (CardSet cardSet in takaWinCardSets)
        {
            double probability = 1;
            
            int[] remainCardCnts_afterLastTurn = remainCardCnts_beforeLastTurn.Clone() as int[];
            if(remainCardCnts_afterLastTurn[cardSet._takaCard] < 1) continue;
            probability *= remainCardCnts_afterLastTurn[cardSet._takaCard] / (double)remainCardCnt;
            remainCardCnts_afterLastTurn[cardSet._takaCard]--;
            
            if(remainCardCnts_afterLastTurn[cardSet._aokiCard] < 1) continue;
            probability *= remainCardCnts_afterLastTurn[cardSet._aokiCard] / ((double)remainCardCnt - 1);
            remainCardCnts_afterLastTurn[cardSet._aokiCard]--;

            // if(ans == 0) ans = probability;
            ans += probability;
        }
		
		Console.WriteLine(ans);
		Console.ReadLine();
	}

    List<int> StringHandToIntArray(string stringHand) {
        List<int> intArrayHand = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            intArrayHand.Add((int)Char.GetNumericValue(stringHand[i]));
        }

        return intArrayHand;
    }

    int CalcScoreByHand(List<int> hand) {
        int score = 0;
        for (int i = 1; i <= 9; i++)
        {
            int iCardCnt = hand.Count((num) => num == i);
            score += i * (int)Math.Pow(10, iCardCnt);
        }

        return score;
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
