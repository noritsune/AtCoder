using System;

namespace KyoPro
{
    public static class EntryPoint
    {
        public static void Main()
        {
            var solver = new Solver();
            solver.Solve();
        }
    }

    public class Solver
    {

        public void Solve()
        {
            var N = Ri();
            var imos = new long[100002];
            for (int i = 0; i < N; i++)
            {
                var LR = Ria();
                var L = LR[0];
                var R = LR[1];
                imos[L]++;
                imos[R + 1]--;
            }

            long workCnt = 0;
            long continuousCnt = 0;
            long continuousMax = int.MinValue;
            bool isWorkingPrev = false;
            foreach (var imo in imos)
            {
                workCnt += imo;

                bool isWorking = workCnt > 0;
                if (isWorking) continuousCnt++;

                if (!isWorking && isWorkingPrev)
                {
                    continuousMax = Math.Max(continuousMax, continuousCnt);
                    continuousCnt = 0;
                }

                isWorkingPrev = isWorking;
            }

            continuousMax = Math.Max(continuousMax, continuousCnt);

            Console.WriteLine(continuousMax);
        }

        static int Ri()
        {
            return int.Parse(Console.ReadLine() ?? string.Empty);
        }

        static int[] Ria(char sep = ' ')
        {
            return Array.ConvertAll(Console.ReadLine().Split(sep), int.Parse);
        }
    }
}