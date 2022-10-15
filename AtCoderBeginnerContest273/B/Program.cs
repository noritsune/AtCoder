using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace KyoPro {
    public static class EntryPoint {
        public static void Main() {
            var solver = new Solver();
            solver.Solve2();
        }
    }

    public class Solver {
        public void Solve2()
        {
            var XK = Rla();
            var X = XK[0];
            var K = XK[1];

            for (int i = 0; i < K; i++)
            {
                X = Round(X, i);
            }

            Console.WriteLine(X);
        }

        long Round(long X, int k)
        {
            var pow10 = (long)Math.Pow(10, k);
            var dividedX = X / pow10;
            var keta1Num = dividedX % 10;
            if (keta1Num <= 4)
            {
                dividedX -= keta1Num;
            }
            else
            {
                dividedX += 10 - keta1Num;
            }

            return dividedX * pow10;
        }

        public void Solve1()
        {
            var XK = Rla();
            var X = XK[0];
            var K = XK[1];

            for (int i = 0; i < K; i++)
            {
                var XChars = Enumerable.Repeat('0', Math.Max((int)K + 1, X.ToString().Length)).ToArray();
                for (int j = XChars.Length - i - 2; j >= XChars.Length - X.ToString().Length; j--)
                {
                    XChars[j] = X.ToString()[j];
                }
                var strX = new string(XChars);

                long diffMin = long.MaxValue;
                long diffMinYMax = long.MinValue;
                for (int j = 0; j < 10; j++)
                {
                    var YChars = strX.ToArray();
                    YChars[^(i + 2)] = (char)('0' + j);
                    var Y = long.Parse(new string(YChars));

                    var diff = Math.Abs(Y - X);
                    if(diff <= diffMin)
                    {
                        diffMin = diff;
                        diffMinYMax = Math.Max(diffMinYMax, Y);
                    }
                }

                X = diffMinYMax;
            }

            Console.WriteLine(X);
        }

        static string Rs(){return Console.ReadLine();}
        static int Ri(){return int.Parse(Console.ReadLine() ?? string.Empty);}
        static long Rl(){return long.Parse(Console.ReadLine() ?? string.Empty);}
        static double Rd(){return double.Parse(Console.ReadLine() ?? string.Empty);}
        static BigInteger Rb(){return BigInteger.Parse(Console.ReadLine() ?? string.Empty);}
        static string[] Rsa(char sep=' '){return Console.ReadLine().Split(sep);}
        static int[] Ria(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),int.Parse);}
        static long[] Rla(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),long.Parse);}
        static double[] Rda(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),double.Parse);}
        static BigInteger[] Rba(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),BigInteger.Parse);}
        static int[] GenerateNums(int num, int N){return Enumerable.Repeat(num, N).ToArray();}
        static int[] GenerateSuretsu(int N){return Enumerable.Range(0, N).ToArray();}
        
        /// <summary>
        /// 素因数分解する
        /// </summary>
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
        
        /// <summary>
        /// 最大公約数をユークリッドの互除法で求める 
        /// </summary>
        static long Gcd(long a, long b)
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
        
        /// <summary>
        /// 2^nパターン分の全探索結果を返す
        /// </summary>
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
        /// <param name="enumerable">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        /// <param name="comparer"></param>
        public static int BinarySearch<T>(IEnumerable<T> enumerable, T target, Comparer<T> comparer)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            
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
        /// <param name="enumerable">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        /// <param name="comparer"></param>
        public static int LowerBound<T>(IEnumerable<T> enumerable, T target, Comparer<T> comparer)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            
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
        /// <param name="enumerable"></param>
        /// <param name="target">検索対象</param>
        /// <param name="comparer"></param>
        public static int LowerBoundUnder<T>(IEnumerable<T> enumerable, T target, Comparer<T> comparer)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            
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
        /// <param name="enumerable">昇順ソートされた配列</param>
        /// <param name="target">検索対象</param>
        /// <param name="comparer"></param>
        public static int UpperBound<T>(IEnumerable<T> enumerable, T target, Comparer<T> comparer)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            
            // 探索範囲のインデックス
            var min = 0;
            var max = array.Length - 1;

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
                var leftSide = new T[] { item };

                // item よりも前のものを除く （順列と組み合わせの違い)
                // 重複を許さないので、unusedから item そのものも取り除く
                var unused = withRepetition ? items : items.SkipWhile(e => !e.Equals(item)).Skip(1).ToList();

                foreach (var rightSide in Enumerate(unused, k - 1, withRepetition)) {
                    yield return leftSide.Concat(rightSide).ToArray();
                }
            }
        }
        

        //順列をすべて列挙する
        //使い方 var patterns = Enumerable.Range(1, N - 1).Perm().Select(x => x.ToArray()).ToArray();
        public static IEnumerable<IEnumerable<T>> Perm<T>(this IEnumerable<T> items, int? k = null) where T : IComparable
        {
            var list = items.ToList();
            int len = list.Count;
            yield return list;
            
            while(true)
            {
                int l = len-2;
                while(l>=0 && list[l].CompareTo(list[l+1])>=0) l--;
                if(l<0) break;
            
                int r = len-1;
                while(list[l].CompareTo(list[r])>0) r--;
                if(list[l].CompareTo(list[r])>=0) break;
            
                var tmp = list[l];
                list[l] = list[r];
                list[r] = tmp;
            
                list.Reverse(l+1,len-l-1);
                yield return list;
            }
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

        public override int GetHashCode() => X ^ Y;
        
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, int b) => new Vector2(a.X * b, a.Y * b);
        
        public double DistanceTo(Vector2 other) => Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        
        public double Length => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
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
        /// グラフ上の全頂点 (ラベル，接続先の頂点集合)
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
        /// <returns>辺のコレクション(接続元のラベル, 接続先のラベル)</returns>
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
        readonly List<T> _data = new List<T>();
        readonly IComparer<T> _comparer;
        readonly bool _isDescending;

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
            (_data[a], _data[b]) = (_data[b], _data[a]);
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
        int N { get; }               // 頂点の数
        private readonly List<Edge>[] _graph;        // グラフの辺のデータ

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
        public long[] GetMinCost(int start)
        {
            // コストをスタート頂点以外を無限大に
            var cost = new long[N];
            for (int i = 0; i < N; i++) cost[i] = 1000000000000000000;
            cost[start] = 0;

            // 未確定の頂点を格納する優先度付きキュー(コストが小さいほど優先度が高い)
            var q = new PriorityQueue<Vertex>(Comparer<Vertex>.Create((a, b) => b.CompareTo(a)));
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
                        q.Enqueue(new Vertex(e.to, cost[e.to]));
                    }
                }
            }

            // 確定したコストを返す
            return cost;
        }

        struct Edge
        {
            public readonly int to;                      // 接続先の頂点
            public readonly long cost;                   // 辺のコスト

            public Edge(int to, long cost)
            {
                this.to = to;
                this.cost = cost;
            }
        }

        public struct Vertex : IComparable<Vertex>
        {
            public readonly int index;                   // 頂点の番号
            public readonly long cost;                   // 記録したコスト

            public Vertex(int index, long cost)
            {
                this.index = index;
                this.cost = cost;
            }

            public int CompareTo(Vertex other)
                => cost.CompareTo(other.cost);
        }
    }

    internal class UnionFind
    {
        // 親要素のインデックスを保持する
        // 親要素が存在しない(自身がルートである)とき、マイナスでグループの要素数を持つ
        int[] Parents { get; }
        public UnionFind(int n)
        {
            Parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                // 初期状態ではそれぞれが別のグループ(ルートは自分自身)
                // ルートなのでマイナスで要素数(1個)を保持する
                Parents[i] = -1;
            }
        }

        // 要素xのルート要素はどれか
        public int Find(int x)
        {
            // 親がマイナスの場合は自分自身がルート
            if (Parents[x] < 0) return x;
            // ルートが見つかるまで再帰的に探す
            // 見つかったルートにつなぎかえる
            Parents[x] = Find(Parents[x]);
            return Parents[x];
        }

        // 要素xの属するグループの要素数を取得する
        public int Size(int x)
        {
            // ルート要素を取得して、サイズを取得して返す
            return -Parents[Find(x)];
        }

        // 要素x, yが同じグループかどうか判定する
        public bool Same(int x, int y)
        {
            return Find(x) == Find(y);
        }

        // 要素x, yが属するグループを同じグループにまとめる
        public bool Union(int x, int y)
        {
            // x, y のルート
            x = Find(x);
            y = Find(y);
            // すでに同じグループの場合処理しない
            if (x == y) return false;

            // 要素数が少ないグループを多いほうに書き換えたい
            if (Size(x) < Size(y))
            {
                (x, y) = (y, x);
            }
            // まとめる先のグループの要素数を更新
            Parents[x] += Parents[y];
            // まとめられるグループのルートの親を書き換え
            Parents[y] = x;
            return true;
        }
    }
    
    /// <summary>
    /// Self-Balancing Binary Search Tree
    /// (using Randomized BST)
    /// 平衡二分探索木
    /// https://yambe2002.hatenablog.com/entry/2017/02/07/122421
    /// </summary>
    public class SB_BinarySearchTree<T> where T : IComparable
    {
        public class Node
        {
            public T Value;
            public Node LChild;
            public Node RChild;
            public int Count;     //size of the sub tree

            public Node(T v)
            {
                Value = v;
                Count = 1;
            }
        }

        static readonly Random _rnd = new Random();

        public static int Count(Node t)
        {
            return t?.Count ?? 0;
        }

        static Node Update(Node t)
        {
            t.Count = Count(t.LChild) + Count(t.RChild) + 1;
            return t;
        }

        public static Node Merge(Node l, Node r)
        {
            if (l == null || r == null) return l ?? r;

            if ((double)Count(l) / (double)(Count(l) + Count(r)) > _rnd.NextDouble())
            {
                l.RChild = Merge(l.RChild, r);
                return Update(l);
            }
            else
            {
                r.LChild = Merge(l, r.LChild);
                return Update(r);
            }
        }

        /// <summary>
        /// split as [0, k), [k, n)
        /// </summary>
        public static Tuple<Node, Node> Split(Node t, int k)
        {
            if (t == null) return new Tuple<Node, Node>(null, null);
            if (k <= Count(t.LChild))
            {
                var s = Split(t.LChild, k);
                t.LChild = s.Item2;
                return new Tuple<Node, Node>(s.Item1, Update(t));
            }
            else
            {
                var s = Split(t.RChild, k - Count(t.LChild) - 1);
                t.RChild = s.Item1;
                return new Tuple<Node, Node>(Update(t), s.Item2);
            }
        }

        public static Node Remove(Node t, T v)
        {
            if (Find(t, v) == null) return t;
            return RemoveAt(t, LowerBound(t, v));
        }

        public static Node RemoveAt(Node t, int k)
        {
            var s = Split(t, k);
            var s2 = Split(s.Item2, 1);
            return Merge(s.Item1, s2.Item2);
        }

        public static bool Contains(Node t, T v)
        {
            return Find(t, v) != null;
        }

        public static Node Find(Node t, T v)
        {
            while (t != null)
            {
                var cmp = t.Value.CompareTo(v);
                if (cmp > 0) t = t.LChild;
                else if (cmp < 0) t = t.RChild;
                else break;
            }
            return t;
        }

        public static Node FindByIndex(Node t, int idx)
        {
            if (t == null) return null;

            var currentIdx = Count(t) - Count(t.RChild) - 1;
            while (t != null)
            {
                if (currentIdx == idx) return t;
                if (currentIdx > idx)
                {
                    t = t.LChild;
                    currentIdx -= (Count(t?.RChild) + 1);
                }
                else
                {
                    t = t.RChild;
                    currentIdx += (Count(t?.LChild) + 1);
                }
            }

            return null;
        }

        public static int UpperBound(Node t, T v)
        {
            var torg = t;
            if (t == null) return -1;

            var ret = int.MaxValue;
            var idx = Count(t) - Count(t.RChild) - 1;
            while (t != null)
            {
                var cmp = t.Value.CompareTo(v);
                    
                if (cmp > 0)
                {
                    ret = Math.Min(ret, idx);
                    t = t.LChild;
                    idx -= (Count(t?.RChild) + 1);
                }
                else
                {
                    t = t.RChild;
                    idx += (Count(t?.LChild) + 1);
                }
            }
            return ret == int.MaxValue ? Count(torg) : ret;
        }

        public static int LowerBound(Node t, T v)
        {
            var torg = t;
            if (t == null) return -1;

            var idx = Count(t) - Count(t.RChild) - 1;
            var ret = Int32.MaxValue;
            while (t != null)
            {
                var cmp = t.Value.CompareTo(v);
                if (cmp >= 0)
                {
                    if (cmp == 0) ret = Math.Min(ret, idx);
                    t = t.LChild;
                    if (t == null) ret = Math.Min(ret, idx);
                    idx -= t == null ? 0 : (Count(t.RChild) + 1);
                }
                else
                {
                    t = t.RChild;
                    idx += (Count(t?.LChild) + 1);
                    if (t == null) return idx;
                }
            }
            return ret == int.MaxValue ? Count(torg) : ret;
        }

        public static Node Insert(Node t, T v)
        {
            var ub = LowerBound(t, v);
            return InsertByIdx(t, ub, v);
        }

        static Node InsertByIdx(Node t, int k, T v)
        {
            var s = Split(t, k);
            return Merge(Merge(s.Item1, new Node(v)), s.Item2);
        }

        public static IEnumerable<T> Enumerate(Node t)
        {
            var ret = new List<T>();
            Enumerate(t, ret);
            return ret;
        }

        static void Enumerate(Node t, List<T> ret)
        {
            if (t == null) return;
            Enumerate(t.LChild, ret);
            ret.Add(t.Value);
            Enumerate(t.RChild, ret);
        }
    }
    
    /// <summary>
    /// C言語のsetクラスに相当するもの
    /// 追加、挿入、LowerBound、UpperBoundがO(logN)でできる
    /// </summary>
    public class Set<T> where T : IComparable
    {
        protected SB_BinarySearchTree<T>.Node _root;

        public T this[int idx] => ElementAt(idx);

        public int Count()
        {
            return SB_BinarySearchTree<T>.Count(_root);
        }

        public virtual void Insert(T v)
        {
            if (_root == null) _root = new SB_BinarySearchTree<T>.Node(v);
            else
            {
                if (SB_BinarySearchTree<T>.Find(_root, v) != null) return;
                _root = SB_BinarySearchTree<T>.Insert(_root, v);
            }
        }

        public void Clear()
        {
            _root = null;
        }

        public void Remove(T v)
        {
            _root = SB_BinarySearchTree<T>.Remove(_root, v);
        }

        public bool Contains(T v)
        {
            return SB_BinarySearchTree<T>.Contains(_root, v);
        }

        public T ElementAt(int k)
        {
            var node = SB_BinarySearchTree<T>.FindByIndex(_root, k);
            if (node == null) throw new IndexOutOfRangeException();
            return node.Value;
        }

        public int Count(T v)
        {
            return SB_BinarySearchTree<T>.UpperBound(_root, v) - SB_BinarySearchTree<T>.LowerBound(_root, v);
        }

        public int LowerBound(T v)
        {
            return SB_BinarySearchTree<T>.LowerBound(_root, v);
        }

        public int UpperBound(T v)
        {
            return SB_BinarySearchTree<T>.UpperBound(_root, v);
        }

        public Tuple<int, int> EqualRange(T v)
        {
            if (!Contains(v)) return new Tuple<int, int>(-1, -1);
            return new Tuple<int, int>(SB_BinarySearchTree<T>.LowerBound(_root, v), SB_BinarySearchTree<T>.UpperBound(_root, v) - 1);
        }

        public List<T> ToList()
        {
            return new List<T>(SB_BinarySearchTree<T>.Enumerate(_root));
        }
    }
    
    /// <summary>
    /// C言語のmultisetクラスに相当するもの
    /// 追加、挿入、LowerBound、UpperBoundがO(logN)でできる
    /// </summary>
    public class MultiSet<T> : Set<T> where T : IComparable
    {
        public override void Insert(T v)
        {
            _root = _root == null 
                ? new SB_BinarySearchTree<T>.Node(v)
                : SB_BinarySearchTree<T>.Insert(_root, v);
        }
    }
}