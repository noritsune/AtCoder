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
	public void Solve()
	{
		int[] HW = ria();
		int H = HW[0];
		int W = HW[1];
		int[] ChCw = ria();
		int Ch = ChCw[0] - 1;
		int Cw = ChCw[1] - 1;
		int[] DhDw = ria();
		int Dh = DhDw[0] - 1;
		int Dw = DhDw[1] - 1;

		string[,] Shw  = new string[H, W];
		int[,] minCosts = new int[H, W];
		for (int i = 0; i < H; i++) 
		{
			string Sh = rs();
			for (int j = 0; j < W; j++) 
			{
				Shw[i, j] = Sh[j].ToString();
				minCosts[i, j] = int.MaxValue;
			}
		}
		minCosts[Ch, Cw] = 0;

		int cost = 0;
		BothQueue q = new BothQueue();
		q.Enqueue(new Pos(Ch, Cw), false);
		for (int i = 0; i < H; i++) 
		{
			for (int j = 0; j < W; j++) 
			{
				CheckCosts(ref minCosts, i, j, cost);
			}
		}

		Console.WriteLine(cost);
		Console.ReadLine();
	}

	void CheckCosts(ref int[,] map, int h, int w, int cost_walk)
	{
		int cost_magic = cost_walk + 1;
		for (int i = -2; i <= 2; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				if(Math.Abs(i) == 1 && j == 0 ||
				   i == 0 && Math.Abs(j) == 1) {
					map[h + i, w + j] = cost_walk;
					continue;
				}
				
				map[h + i, w + j] = cost_magic;
			}
		}
	}

	class BothQueue {
		Queue q = new Queue();
		Stack s = new Stack();

		public void Enqueue(Object obj, bool isTail) {
			if(isTail) {
				q.Enqueue(obj);
				return;
			}
			
			s.Push(obj);
		}
		
		public Object Dequeue() {
			if(s.Count > 0) return s.Pop();

			return q.Dequeue();
		}
	}

	class Pos{
		public int h;
		public int w;

		public Pos(int h, int w)
		{
			this.h = h;
			this.w = w;
		}

		public static List<Pos> GetPosList_A(Pos pos) {
			List<Pos> posList = new List<Pos>();

			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if(i == 0 ^ j == 0) posList.Add(new Pos(pos.h + i, pos.w + j));
				}
			}

			return posList;
		}

		public static List<Pos> GetPosList_B(Pos pos) {
			List<Pos> posList = new List<Pos>();

			for (int i = -2; i <= 2; i++)
			{
				for (int j = -2; j <= 2; j++)
				{
					if(i != 0 || j != 0) posList.Add(new Pos(pos.h + i, pos.w + j));
				}
			}

			return posList;
		}
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
