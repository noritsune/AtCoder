using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AtCoder;

namespace AtCoder {
    public class SolveExecuter {
        public static void Main() {
            var solver = new Solver();
            solver.Solve2();
        }
    }

    public class Solver {
        public void Solve2() {
            var NM = ria();
            var N = NM[0];
            var M = NM[1];
            var Hs = ria();
            var UVs = new List<(int U, int V)>();
            for (int i = 0; i < M; i++)
            {
                var UV = ria();
                UVs.Add((UV[0], UV[1]));
            }
            
            var dijkstra = new Dijkstra(N + 1);
            foreach (var (U, V) in UVs)
            {
                int HU = Hs[U - 1];
                int HV = Hs[V - 1];
                
                int higherIndex = HU > HV ? U : V;
                int lowerIndex  = HU > HV ? V : U;
                int higherH = Hs[higherIndex - 1];
                int lowerH  = Hs[lowerIndex  - 1];
                
                int diff = higherH - lowerH;
                dijkstra.Add(higherIndex, lowerIndex, 0);
                dijkstra.Add(lowerIndex, higherIndex, diff);
            }

            var result = dijkstra.GetMinCost(1);

            int maxCostIndex = 1;
            var costs = result["cost"];
            for (int i = 1; i < costs.Length; i++)
            {
                if(costs[i] > costs[maxCostIndex]) {
                    maxCostIndex = i;
                }
                
            }
            
            Console.WriteLine(costs[maxCostIndex] + Hs[maxCostIndex - 1]);
        }
        
        public void Solve1() {
            var NM = ria();
            var N = NM[0];
            var M = NM[1];
            var Hs = ria();
            var UVs = new List<(int U, int V)>();
            for (int i = 0; i < M; i++)
            {
                var UV = ria();
                UVs.Add((UV[0], UV[1]));
            }
            
            var HDiffMax = UVs
                .Select(UV => Math.Abs(Hs[UV.U - 1] - Hs[UV.V - 1]))
                .Max();
            int costOffset= HDiffMax * 2;
            
            var dijkstra = new Dijkstra(N + 1);
            foreach (var (U, V) in UVs)
            {
                int HU = Hs[U - 1];
                int HV = Hs[V - 1];
                
                int higherIndex = HU > HV ? U : V;
                int lowerIndex  = HU > HV ? V : U;
                int higherH = Hs[higherIndex - 1];
                int lowerH  = Hs[lowerIndex  - 1];
                
                int diff = higherH - lowerH;
                dijkstra.Add(higherIndex, lowerIndex, diff + costOffset);
                dijkstra.Add(lowerIndex, higherIndex, -2 * diff + costOffset);
            }

            // var result = dijkstra.GetMinCost(1, costOffset);
            // var costs = result["cost"];
            // Console.WriteLine(costs.Skip(1).Max());
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
        
        // 最大公約数をユークリッドの互除法で求める 
        private static long Gcd(long a, long b)
        {
            while (true)
            {
                if (a < b)
                {
                    (a, b) = (b, a);
                    continue;
                }

                while (b != 0)
                {
                    var remainder = a % b;
                    a = b;
                    b = remainder;
                }

                return a;
            }
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

        /// <summary>
        /// array内でtargetが最初に出現する要素番号を返す
        /// </summary>
        /// <param name="array">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        public static int BinarySearch<T>(IEnumerable<T> array, T target, Comparer<T> comparer)
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Count() - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = comparer.Compare(target, array.ElementAt(mid));
                if (compareResult > 0) {// 中央値より大きい場合
                    min = mid + 1;
                }
                else if (compareResult < 0) {// 中央値より小さい場合
                    max = mid - 1;
                } else {
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
        public static int LowerBound<T>(IEnumerable<T> array, T target, Comparer<T> comparer)
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Count() - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = comparer.Compare(target, array.ElementAt(mid));
                if(compareResult > 0) min = mid + 1;
                else max = mid - 1;
            }
            return min;
        }

        /// <summary>
        /// array内でtarget以下となる要素が最初に出現する要素番号を返す
        /// 存在しないなら-1
        /// </summary>
        /// <param name="array">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        public static int LowerBoundUnder<T>(IEnumerable<T> array, T target, Comparer<T> comparer)
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Count() - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = comparer.Compare(target, array.ElementAt(mid));
                if(compareResult < 0) max = mid - 1;
                else min = mid + 1;
            }
            return max;
        }

        /// <summary>
        /// array内でtargetより大きな要素が最初に出現する要素番号を返す
        /// </summary>
        /// <param name="array">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        public static int UpperBound<T>(IEnumerable<T> array, T target, Comparer<T> comparer)
        {
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Count() - 1;

            while (min <= max) // 範囲内にある限り探し続ける
            {
                var mid = min + (max - min) / 2;
                int compareResult = comparer.Compare(target, array.ElementAt(mid));
                if(compareResult < 0) max = mid - 1;
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
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y) {
            X = x;
            Y = y;
        }
        
        public override bool Equals(object obj)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Vector2 other = (Vector2)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
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
    
    public class Dijkstra
    {
        public int N { get; }               // 頂点の数
        private List<Edge>[] _graph;        // グラフの辺のデータ

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="n">頂点数</param>
        public Dijkstra(int n)
        {
            N = n;
            _graph = new List<Edge>[n];
            for (int i = 0; i < n; i++) _graph[i] = new List<Edge>();
        }

        /// <summary>
        /// 辺を追加
        /// </summary>
        /// <param name="a">接続元の頂点</param>
        /// <param name="b">接続先の頂点</param>
        /// <param name="cost">コスト</param>
        public void Add(int a, int b, long cost = 1)
                => _graph[a].Add(new Edge(b, cost));

        /// <summary>
        /// 最短経路のコストを取得
        /// </summary>
        /// <param name="start">開始頂点</param>
        public Dictionary<string, long[]> GetMinCost(int start)
        {
            // コストをスタート頂点以外を無限大に
            var cost = new long[N];
            for (int i = 0; i < N; i++) cost[i] = 1000000000000000000;
            cost[start] = 0;

            var prev = new long[N];

            // 未確定の頂点を格納する優先度付きキュー(コストが小さいほど優先度が高い)
            var q = new PriorityQueue<Vertex>(Comparer<Vertex>.Create((a, b) => a.CompareTo(b)));
            q.Enqueue(new Vertex(start, 0));

            while (q.Count > 0)
            {
                var v = q.Dequeue();

                // 記録されているコストと異なる(コストがより大きい)場合は無視
                if (v.cost != cost[v.index]) continue;

                // 今回確定した頂点からつながる頂点に対して更新を行う
                foreach (var e in _graph[v.index])
                {
                    if (cost[e.to] > v.cost + e.cost)
                    {
                        // 既に記録されているコストより小さければコストを更新
                        cost[e.to] = v.cost + e.cost;
                        prev[e.to] = v.index;
                        q.Enqueue(new Vertex(e.to, cost[e.to]));
                    }
                }
            }

            // 確定したコストを返す
            return new Dictionary<string, long[]>
            {
                {"cost", cost},
                {"prev", prev}
            };
        }

        public struct Edge
        {
            public int to;                      // 接続先の頂点
            public long cost;                   // 辺のコスト

            public Edge(int to, long cost)
            {
                this.to = to;
                this.cost = cost;
            }
        }

        public struct Vertex : IComparable<Vertex>
        {
            public int index;                   // 頂点の番号
            public long cost;                   // 記録したコスト

            public Vertex(int index, long cost)
            {
                this.index = index;
                this.cost = cost;
            }

            public int CompareTo(Vertex other)
                => cost.CompareTo(other.cost);
        }
    }

    class UnionFind
    {
        // 親要素のインデックスを保持する
        // 親要素が存在しない(自身がルートである)とき、マイナスでグループの要素数を持つ
        public int[] Parents { get; set; }
        public UnionFind(int n)
        {
            this.Parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                // 初期状態ではそれぞれが別のグループ(ルートは自分自身)
                // ルートなのでマイナスで要素数(1個)を保持する
                this.Parents[i] = -1;
            }
        }

        // 要素xのルート要素はどれか
        public int Find(int x)
        {
            // 親がマイナスの場合は自分自身がルート
            if (this.Parents[x] < 0) return x;
            // ルートが見つかるまで再帰的に探す
            // 見つかったルートにつなぎかえる
            this.Parents[x] = Find(this.Parents[x]);
            return this.Parents[x];
        }

        // 要素xの属するグループの要素数を取得する
        public int Size(int x)
        {
            // ルート要素を取得して、サイズを取得して返す
            return -this.Parents[this.Find(x)];
        }

        // 要素x, yが同じグループかどうか判定する
        public bool Same(int x, int y)
        {
            return this.Find(x) == this.Find(y);
        }

        // 要素x, yが属するグループを同じグループにまとめる
        public bool Union(int x, int y)
        {
            // x, y のルート
            x = this.Find(x);
            y = this.Find(y);
            // すでに同じグループの場合処理しない
            if (x == y) return false;

            // 要素数が少ないグループを多いほうに書き換えたい
            if (this.Size(x) < this.Size(y))
            {
                var tmp = x;
                x = y;
                y = tmp;
            }
            // まとめる先のグループの要素数を更新
            this.Parents[x] += this.Parents[y];
            // まとめられるグループのルートの親を書き換え
            this.Parents[y] = x;
            return true;
        }
    }
}