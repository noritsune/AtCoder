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
		Console.ReadLine();
        string csString = Console.ReadLine();
        char[] cs = new char[csString.Length];
        for (int i = 0; i < csString.Length; i++) cs[i] = csString[i];

        int ans = 0;
        bool isOut = true;
        int RIndex = -1;
        do {
            isOut = false;

            for (int i = cs.Length - 1; i > 0; i--)
            {
                if(cs[i] == 'R' && cs[i - 1] == 'W') {
                    ans++;
                    isOut = true;

                    for (int j = RIndex + 1; j < cs.Length; j++)
                    {
                        if(j == cs.Length - 1) {
                            isOut = false;
                            continue;
                        }

                        if(cs[j] == 'W') {
                            RIndex = j;
                            cs[RIndex] = 'R';
                            cs[i] = 'W';
                            break;
                        }
                    }

                    break;
                }
            }

        } while(isOut);

		Console.WriteLine(ans);
		Console.ReadLine();
	}

    static bool IsOut(string cs) {
        bool ans = false;

        for (int i = 0; i < cs.Length - 1; i++)
        {
            if(cs[i] == 'R') continue;
            if(cs[i + 1] == 'R') {
                ans = true;
                break;
            }
        }

        return ans;
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
