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
		int[] NM = ria();
        int N = NM[0];
        int M = NM[1];

        List<List<int>> tunnels = new List<List<int>>();
        for (int i = 0; i < N; i++) tunnels.Add(new List<int>());

        for (int i = 0; i < M; i++)
        {
            int[] fromTo = ria(); 
            tunnels[fromTo[0] - 1].Add(fromTo[1] - 1);
            tunnels[fromTo[1] - 1].Add(fromTo[0] - 1);
        }

        List<int> nextRooms = tunnels[0];
        List<int> room_bef = Enumerable.Repeat(0, nextRooms.Count).ToList();
        int[] arrows = Enumerable.Repeat(-1, N).ToArray();
        arrows[0] = 0;
        do {
            List<int> nextNextRooms_tmp = new List<int>();
            List<int> room_bef_tmp = new List<int>();
            for (int i = 0; i < nextRooms.Count; i++) {
                if(arrows[nextRooms[i]] != -1) continue;

                arrows[nextRooms[i]] = room_bef[i];

                List<int> room_bef_current = Enumerable.Repeat(nextRooms[i], tunnels[nextRooms[i]].Count).ToList();

                room_bef_tmp.AddRange(room_bef_current);

                nextNextRooms_tmp.AddRange(tunnels[nextRooms[i]]);
            }

            nextRooms = nextNextRooms_tmp;
            room_bef = room_bef_tmp;
        } while(!arrows.All((n) => n != -1));

		Console.WriteLine("Yes");
        for (int i = 1; i < arrows.Length; i++) Console.WriteLine(arrows[i] + 1);

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
