using System;
using System.Collections.Generic;
using System.Linq;

namespace D
{
    class Program
    {
        static void Main(string[] args)
        {
            long N = long.Parse(Console.ReadLine());
            int cnt = 0;
            List<long> primes = GeneratePrime(N);
            // int index = 0;
            int e = 1;

            while(N > 1) {
                while(true) {
                    // if(N / Math.pow(primes[index], e));
                    e++;
                cnt++;
                }
                // index++;
                // e = 1;
            }

            Console.WriteLine(cnt);

            // List<long> primes = GeneratePrime(N);
            // foreach(long prime in primes){
            //     Console.WriteLine(prime);
            // }
            Console.ReadLine();
        }
        
        public static List<long> GeneratePrime(long max)
        {
            System.Diagnostics.Debug.Assert(max >= 2);  // maxは2以上の数

            long prime;
            double sqrtMax = Math.Sqrt(max);
            var primeList = new List<long>();

            // ■ステップ 1
            // 探索リストに2からxまでの整数を昇順で入れる。
            List<long> searchList = new List<long>();
            for(long i = 2; i < max; i++) searchList.Add(i);

            do
            {
                // ■ステップ 2
                // 探索リストの先頭の数を素数リストに移動し、その倍数を探索リストから篩い落とす。
                prime = searchList.First();
                // 素数リストに追加
                primeList.Add(prime);
                // 倍数をふるい落とす
                searchList.RemoveAll(n => n % prime == 0);

                // ■ステップ 3
                // 上記の篩い落とし操作を探索リストの先頭値がxの平方根に達するまで行う。
            } while (prime < sqrtMax);

            // ■ステップ 4
            // 探索リストに残った数を素数リストに移動して処理終了。
            primeList.AddRange(searchList);

            return primeList;
        }
    }
}
