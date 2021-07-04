using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace util {
    class Util{
        static void Main(){
            Sol mySol =new Sol();
            mySol.Solve();
        }
    }

    public class Sol{
        const int _mod = 1000000007;
        public void Solve(){
            int[] NM = ria();
            int N = NM[0];
            int M = NM[1];
            List<int[]> ABCs = new List<int[]>();
            for (int i = 0; i < M; i++) ABCs.Add(ria());

            Graph<int> graph = new Graph<int>(Graph<int>.Type.DirectedGraph);
            for (int i = 1; i <= N; i++)
            {
                graph.AddVertex(i);
            }

            Dictionary<int, int>[] dists = new Dictionary<int, int>[N + 1];
            for (int i = 1; i <= N; i++) dists[i] = new Dictionary<int, int>();

            foreach (int[] ABC in ABCs)
            {
                int A = ABC[0];
                int B = ABC[1];
                int C = ABC[2];
                graph.AddEdge(A, B);

                dists[A].Add(B, C);
            }

            long ans = 0;
            for (int s = 1; s <= N; s++)
            {
                for (int t = 1; t <= N; t++)
                {
                    if(t == s) continue;

                    for (int k = 1; k <= N; k++)
                    {
                        int[] minDists = Enumerable.Repeat(int.MaxValue, N + 1).ToArray();
                        minDists[s] = 0;
                        int[] prevVerts = new int[N + 1];

                        PriorityQueue<int> q = new PriorityQueue<int>(isDescending: false);
                        q.Enqueue(s);

                        while(q.Count > 0) {
                            int minVert = q.Dequeue();
                            foreach (int vert in graph.Vertices[minVert])
                            {
                                if(vert <= k || vert == s || vert == t) {
                                    int currentDist = minDists[minVert] + dists[minVert][vert];
                                    if(currentDist < minDists[vert]) {
                                        minDists[vert] = currentDist;
                                        prevVerts[vert] = minVert;
                                        q.Enqueue(vert);
                                    }
                                }
                            }
                        }

                        if(minDists[t] != int.MaxValue) ans += minDists[t];
                    }
                }
            }
            
            Console.WriteLine(ans);
            Console.ReadLine();
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
        
        // 2^nパターン分の全探索結果
        static IEnumerable<bool[]> BitFullSearch(int n)
        {
            if (n <= 0)
            {
                yield return new bool[] { };
                yield break;
            }

            for (int i = 0; i < Math.Pow(2, n); i++)
            {
                var array = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    var targetBit = (i >> j) & 1;
                    if (targetBit == 1) array[j] = true;
                }

                yield return array;
            }
        }

        public static int BinarySearch<T>(T[] array, T target) where T : IComparable<T>
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Length - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                switch (target.CompareTo(array[mid]))
                {
                    case 1:  // 中央値より大きい場合
                        min = mid + 1;
                        break;
                    case -1: // 中央値より小さい場合
                        max = mid - 1;
                        break;
                    case 0:
                        return mid;
                }
            }
            return -1; // 見つからなかった
        }

        /// <summary>
        /// array内でtarget以上となる要素が最初に出現する要素番号を返す
        /// </summary>
        /// <param name="array">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        public static int LowerBound<T>(T[] array, T target) where T : IComparable<T>
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Length - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = target.CompareTo(array[mid]);
                if(compareResult == 1) min = mid + 1;
                else max = mid - 1;
            }
            return min;
        }

        /// <summary>
        /// array内でtargetより大きな要素が最初に出現する要素番号を返す
        /// </summary>
        /// <param name="array">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        public static int UpperBound<T>(T[] array, T target) where T : IComparable<T>
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Length - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = target.CompareTo(array[mid]);
                if(compareResult == -1) max = mid - 1;
                else min = mid + 1;
            }
            return min;
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

    public class Vector2 {
        public int _x = 0;
        public int _y = 0;

        public Vector2(int x, int y) {
            _x = x;
            _y = y;
        }
    }

    /// <summary>
    /// グラフ
    /// </summary>
    /// <typeparam name="T">頂点のラベルの型</typeparam>
    public class Graph<T>
    {
        public enum Type
        {
            DirectedGraph,      // 有向グラフ
            UndirectedGraph,    // 無向グラフ
        }

        /// <summary>
        /// グラフ上の全頂点 <ラベル，接続先の頂点集合>
        /// </summary>
        public IReadOnlyDictionary<T, HashSet<T>> Vertices => _vertices;
        private readonly Dictionary<T, HashSet<T>> _vertices;

        /// <summary>
        /// グラフの種類(有向グラフ or 無向グラフ)
        /// </summary>
        public Type GraphType { get; }

        public Graph(Type type)
        {
            _vertices = new Dictionary<T, HashSet<T>>();
            GraphType = type;
        }

        public Graph(Type type, IEnumerable<(T, HashSet<T>)> vertices) : this(type)
        {
            foreach (var v in vertices)
                _vertices[v.Item1] = v.Item2;
        }

        /// <summary>
        /// 頂点を追加する
        /// </summary>
        public void AddVertex(T label)
            => _vertices[label] = new HashSet<T>();

        /// <summary>
        /// 頂点を追加する
        /// </summary>
        public void AddVertex(T label, IEnumerable<T> to)
        {
            AddVertex(label);
            foreach (var v in to)
                _vertices[label].Add(v);
        }

        /// <summary>
        /// 頂点数を取得する
        /// </summary>
        public int GetVertexCount()
            => _vertices.Count;

        /// <summary>
        /// 辺を追加する
        /// </summary>
        /// <param name="from">接続元の頂点</param>
        /// <param name="to">接続先の頂点</param>
        /// <returns>追加できたか</returns>
        public bool AddEdge(T from, T to)
        {
            if (!_vertices.ContainsKey(from) || !_vertices.ContainsKey(to)) return false;

            _vertices[from].Add(to);
            if (GraphType == Type.UndirectedGraph) _vertices[to].Add(from);
            return true;
        }

        /// <summary>
        /// 辺数を取得する
        /// </summary>
        /// <returns></returns>
        public int GetEdgeCount()
        {
            var count = _vertices.Aggregate(0, (sum, v) => sum += v.Value.Count);
            if (GraphType == Type.DirectedGraph) return count;
            else return count / 2;
        }

        /// <summary>
        /// グラフの辺集合を取得する
        /// </summary>
        /// <returns>辺のコレクション(<接続元のラベル, 接続先のラベル>)</returns>
        public IEnumerable<(T, T)> GetEdges()
        {
            if (GraphType == Type.DirectedGraph)
            {
                foreach (var v in _vertices)
                    foreach (var to in v.Value)
                        yield return (v.Key, to);
            }
            else if (GraphType == Type.UndirectedGraph)
            {
                var memo = new Dictionary<T, List<T>>();
                foreach (var v in _vertices)
                    foreach (var to in v.Value)
                    {
                        if (!memo.ContainsKey(to)) memo[to] = new List<T>();
                        if (memo[to].Contains(v.Key)) continue;
                        yield return (v.Key, to);
                        if (!memo.ContainsKey(v.Key)) memo[v.Key] = new List<T>();
                        memo[v.Key].Add(to);
                    }
            }
        }
    }

    public class PriorityQueue<T> : IEnumerable<T>
    {
        private readonly List<T> _data = new List<T>();
        private readonly IComparer<T> _comparer;
        private readonly bool _isDescending;

        public PriorityQueue(IComparer<T> comparer, bool isDescending = true)
        {
            _comparer = comparer;
            _isDescending = isDescending;
        }

        public PriorityQueue(Comparison<T> comparison, bool isDescending = true)
            : this(Comparer<T>.Create(comparison), isDescending)
        {
        }

        public PriorityQueue(bool isDescending = true)
            : this(Comparer<T>.Default, isDescending)
        {
        }

        public void Enqueue(T item)
        {
            _data.Add(item);
            var childIndex = _data.Count - 1;
            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (Compare(_data[childIndex], _data[parentIndex]) >= 0)
                    break;
                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            var lastIndex = _data.Count - 1;
            var firstItem = _data[0];
            _data[0] = _data[lastIndex];
            _data.RemoveAt(lastIndex--);
            var parentIndex = 0;
            while (true)
            {
                var childIndex = parentIndex * 2 + 1;
                if (childIndex > lastIndex)
                    break;
                var rightChild = childIndex + 1;
                if (rightChild <= lastIndex && Compare(_data[rightChild], _data[childIndex]) < 0)
                    childIndex = rightChild;
                if (Compare(_data[parentIndex], _data[childIndex]) <= 0)
                    break;
                Swap(parentIndex, childIndex);
                parentIndex = childIndex;
            }
            return firstItem;
        }

        public T Peek()
        {
            return _data[0];
        }

        private void Swap(int a, int b)
        {
            var tmp = _data[a];
            _data[a] = _data[b];
            _data[b] = tmp;
        }

        private int Compare(T a, T b)
        {
            return _isDescending ? _comparer.Compare(b, a) : _comparer.Compare(a, b);
        }

        public int Count => _data.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
