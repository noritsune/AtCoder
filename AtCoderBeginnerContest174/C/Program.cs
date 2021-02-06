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
	public void Solve(){
        int K = ri();
        if(K / 2 * 2 == K || K / 5 * 5 == K) {
            Console.WriteLine("-1");
            Console.ReadLine();
            return;
        } else {
            int luckyNum = 7 % K;
            int firstMod = luckyNum;
            int ans = 0;
            for (int i = 1; i < K - 1; i++)
            {
                if(luckyNum == 0) {
                    ans = i;
                    break;
                }

                if(i != 0) {
                    if(luckyNum == firstMod) {
                        break;
                    }
                }

                luckyNum %= K;
            }
        }

        // BigInteger ans = 0;
        // while (true)
        // {
        //     ans++;

        //     BigInteger sevens = 0;

        //     for (int j = 0; j < ans; j++)
        //     {
        //         sevens += (BigInteger)Math.Pow(10, j);
        //     }
        //     sevens *= 7;

        //     if(sevens % K == 0) {
        //         Console.WriteLine(ans);
        //         Console.ReadLine();
        //         return;
        //     }

            // int[] primes = PrimeFactors(sevens).ToArray();
            // Console.Write(i + ": ");
            // foreach (var prime in primes)
            // {
            //    Console.Write(prime + " × ");
            // }
            // Console.WriteLine("");
        // }
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

    static IEnumerable<int> Primes(int maxnum) {
        int[] sieve = Enumerable.Range(0, maxnum + 1).ToArray();
        sieve[1] = 0;  // 0 : 素数ではない
        int squareroot = (int)Math.Sqrt(maxnum);
        for (int i = 2; i <= squareroot; i++) {
            if (sieve[i] <= 0)
                continue;
            for (int n = i * 2; n <= maxnum; n += i)
                sieve[n] = 0;
        }
        return sieve.Where(n => n > 0);
    }

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
