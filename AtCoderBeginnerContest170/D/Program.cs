using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Util{
	static void Main(){
		Sol mySol =new Sol();
		mySol.Solve();
	}
}

class Sol{
	public void Solve(){
		int N = ri();
        int[] aArray = ria();
        Array.Sort(aArray);

        bool[] checkArray = Enumerable.Repeat(true, aArray.Length).ToArray();
        
        for (int i = 0; i < aArray.Length; i++)
        {
            if(checkArray[i]) {
                for (int j = 2; j * aArray[i] < aArray.Last(); j++)
                {
                    // if()
                    // checkArray[i]
                }
            }
        }

        // int limit = (int)Math.Sqrt(aList.Last());
        // for (int i = 0; i < aList.Count - 1; i++)
        // {
        //     if(aList.Count((n) => n == aList[i]) > 1) {
        //         //同じ数があればそれらをすべて消去
        //         aList.RemoveAll((n) => n == aList[i]);
        //     } else {
        //         for (int j = 2; j * aList[i] <= aList.Last(); j++)
        //         {
        //             aList.Remove(j * aList[i]);
        //         }
        //     }
        // }

        // Console.WriteLine(aList.Count);

        // int ans = 1;
        // if(aArray.Length > 1) {
        //     if(aArray[0] == aArray[1]) ans = 0;
        // }
        
        // for(int i = aArray.Length - 1; i > 0; i--) {
        //     bool canDevide = false;

        //     for (int j = i - 1; j >= 0; j--)
        //     {
        //         if(aArray[i] % aArray[j] == 0) {
        //             canDevide = true;
        //             break;
        //         }
        //     }

        //     if(!canDevide) ans++;
        // }

		// Console.WriteLine(ans);
		Console.ReadLine();
	}

	static String rs(){return Console.ReadLine();}
	static int ri(){return int.Parse(Console.ReadLine());}
	static long rl(){return long.Parse(Console.ReadLine());}
	static double rd(){return double.Parse(Console.ReadLine());}
	static String[] rsa(char sep=' '){return Console.ReadLine().Split(sep);}
	static int[] ria(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>int.Parse(e));}
	static long[] rla(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>long.Parse(e));}
	static double[] rda(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>double.Parse(e));}
    static int[] generateNums(int num, int N){return Enumerable.Repeat(num, N).ToArray();}
    static int[] generateSuretsu(int N){return Enumerable.Range(0, N).ToArray();}
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
