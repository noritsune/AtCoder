using System;
using System.Linq;
using System.Collections.Generic;

namespace D
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());

            long ans = 1;
            for(int i = 2; i <= N; i++) {
                List<int> primes = PrimeFactors(i).ToList();
                primes.Add(-1);

                int n = 0;
                long cnt = 1;
                int current = primes[0];
                foreach(int prime in primes) {
                    if(current == prime) n++;
                    else {
                        cnt *= n+1;
                        
                        n = 1;
                        current = prime;
                    }
                }

                ans += i * cnt;
            }

            Console.WriteLine(ans);
            Console.ReadLine();
        }

        public static IEnumerable<int> PrimeFactors(int n)
        {
            int i = 2;
            int tmp = n;

            while (i * i <= n)
            {
                if(tmp % i == 0){
                    tmp /= i;
                    yield return i;
                }else{
                    i++;
                }
            }
            if(tmp != 1) yield return tmp;
        }
    }
}
