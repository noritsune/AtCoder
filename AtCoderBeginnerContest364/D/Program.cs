using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace KyoPro
{
public static class CONST
{
    public const long MOD = 998244353;
}

public static class EntryPoint {
    public static void Main() {
        var solver = new Solver();
        solver.Solve();
    }
}

public class Solver {
    public void Solve()
    {
        var NQ = Rla();
        var N = NQ[0]; var Q = NQ[1];
        var As = Rla().OrderBy(x => x).ToArray();

        for (int i = 0; i < Q; i++)
        {
            var BK = Rla();
            var B = BK[0]; var K = BK[1];

            long min = 0;
            long max = (long)2e8;
            while (min <= max)
            {
                var mid = min + (max - min) / 2;
                var lb = LowerBound(As, B - mid, Comparer<long>.Default);
                var ub = UpperBound(As, B + mid, Comparer<long>.Default);
                var cnt = ub - lb;
                if (K - cnt > 0) min = mid + 1;
                else max = mid - 1;
            }

            Console.WriteLine(min);
        }
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

    static long Lcm(long a, long b)
    {
        return a * b / Gcd(a, b);
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

    /// <summary>
    /// doubleより高精度な平方根を求める
    /// </summary>
    public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
    {
        if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

        decimal current = (decimal)Math.Sqrt((double)x), previous;
        do
        {
            previous = current;
            if (previous == 0.0M) return 0;
            current = (previous + x / previous) / 2;
        }
        while (Math.Abs(previous - current) > epsilon);
        return current;
    }

    /// <summary>
    /// 繰り返し二乗法で累乗のmodを求める
    /// </summary>
    long ModPow(long a, long n, long mod)
    {
        long res = 1;
        while (n > 0)
        {
            if ((n & 1) == 1) res = res * a % mod;
            a = a * a % mod;
            n >>= 1;
        }
        return res;
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

public class CombCalculator
{
    readonly long[] _fact;
    readonly long[] _factInv;

    /// <summary>
    /// 後の計算を高速化するために階乗とその逆元を求めておく
    /// </summary>
    /// <param name="N">Calc関数を使うときに最大でnがいくつまで入力されうるか</param>
    public CombCalculator(int N)
    {
        _fact = new long[N + 1];
        _factInv = new long[N + 1];

        _fact[0] = 1;
        for (int i = 1; i <= N; i++)
        {
            _fact[i] = _fact[i - 1] * i % CONST.MOD;
        }

        _factInv[N] = ModInt.Power(_fact[N], CONST.MOD - 2);
        for (int i = N - 1; i >= 0; i--)
        {
            _factInv[i] = _factInv[i + 1] * (i + 1) % CONST.MOD;
        }
    }

    public long Calc(int n, int r)
    {
        if (n < r) return 0;
        if (n < 0 || r < 0) return 0;
        return _fact[n] * _factInv[r] % CONST.MOD * _factInv[n - r] % CONST.MOD;
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

public enum GraphType
{
    Directed,      // 有向グラフ
    Undirected,    // 無向グラフ
}

/// <summary>
/// グラフ
/// </summary>
/// <typeparam name="T">頂点のラベルの型</typeparam>
public class Graph<T>
{
    /// <summary>
    /// グラフ上の全頂点 (ラベル，接続先の頂点集合)
    /// </summary>
    public IReadOnlyDictionary<T, HashSet<T>> Vertices => _vertices;
    private readonly Dictionary<T, HashSet<T>> _vertices;

    /// <summary>
    /// グラフの種類(有向グラフ or 無向グラフ)
    /// </summary>
    public GraphType GraphType { get; }

    public Graph(GraphType type)
    {
        _vertices = new Dictionary<T, HashSet<T>>();
        GraphType = type;
    }

    public Graph(GraphType type, IEnumerable<(T, HashSet<T>)> vertices) : this(type)
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
        if (GraphType == GraphType.Undirected) _vertices[to].Add(from);
        return true;
    }

    /// <summary>
    /// 辺数を取得する
    /// </summary>
    /// <returns></returns>
    public int GetEdgeCount()
    {
        var count = _vertices.Aggregate(0, (sum, v) => sum += v.Value.Count);
        if (GraphType == GraphType.Directed) return count;
        else return count / 2;
    }

    /// <summary>
    /// グラフの辺集合を取得する
    /// </summary>
    /// <returns>辺のコレクション(接続元のラベル, 接続先のラベル)</returns>
    public IEnumerable<(T, T)> GetEdges()
    {
        if (GraphType == GraphType.Directed)
        {
            foreach (var v in _vertices)
                foreach (var to in v.Value)
                    yield return (v.Key, to);
        }
        else if (GraphType == GraphType.Undirected)
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

public class IntGraph : Graph<int>
{
    public IntGraph(GraphType type) : base(type) { }

    public void InitFromStdin(int N, int M)
    {
        for (int i = 1; i <= N; i++) AddVertex(i);

        for (int i = 0; i < M; i++)
        {
            var uv = Console.ReadLine().Split().Select(int.Parse).ToArray();
            AddEdge(uv[0], uv[1]);
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

public class GenericDijkstra<T> where T : notnull
{
    private readonly Dictionary<T, List<Edge>> _graph;        // グラフの辺のデータ

    /// <summary>
    /// 初期化
    /// </summary>
    public GenericDijkstra()
    {
        _graph = new Dictionary<T, List<Edge>>();
    }

    public void AddVertex(T vertex)
    {
        _graph.TryAdd(vertex, new List<Edge>());
    }

    /// <summary>
    /// 辺を追加
    /// </summary>
    /// <param name="from">接続元の頂点</param>
    /// <param name="to">接続先の頂点</param>
    /// <param name="cost">コスト</param>
    public void AddEdge(T from, T to, long cost = 1)
    {
        _graph[from].Add(new Edge(to, cost));
    }

    /// <summary>
    /// 最短経路のコストを取得
    /// </summary>
    /// <param name="start">開始頂点</param>
    public Dictionary<T, long> GetMinCost(T start)
    {
        // コストをスタート頂点以外を無限大に
        var cost = new Dictionary<T, long>();
        foreach (var v in _graph.Keys) cost[v] = long.MaxValue;
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
        public readonly T to;                      // 接続先の頂点
        public readonly long cost;                   // 辺のコスト

        public Edge(T to, long cost)
        {
            this.to = to;
            this.cost = cost;
        }
    }

    public readonly struct Vertex : IComparable<Vertex>
    {
        public readonly T index;                   // 頂点の番号
        public readonly long cost;                   // 記録したコスト

        public Vertex(T index, long cost)
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

public struct ModInt : IEquatable<ModInt>
{
    public long Num;
    const bool ModIsPrime = true;

    public ModInt(long num)
    {
        Num = num;
    }

    public static ModInt GetInv(long f)
    {
        return ModIsPrime ? GetInvByFermat(f) : GetInvByEuclid(f);
    }

    // x^n
    public static long Power(long x, long n)
    {
        x %= CONST.MOD;
        long pow = 1;
        while (n > 0)
        {
            if (n % 2 == 0)
            {
                x *= x;
                x %= CONST.MOD;
                n /= 2;
            }
            else
            {
                pow *= x;
                pow %= CONST.MOD;
                n--;
            }
        }

        return pow;
    }

    public static ModInt GetInvByFermat(long f)
    {
        // フェルマーの小定理 a^(-1) = a^(mod-2)
        // Modが素数であることが前提
        return new ModInt(Power(f, CONST.MOD - 2));
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static ModInt GetInvByEuclid(long a)
    {
        // 拡張ユークリッドの互除法
        long b = CONST.MOD, u = 1, v = 0;
        while (b != 0)
        {
            long t = a / b;
            a -= t * b;
            Swap(ref a, ref b);
            u -= t * v;
            Swap(ref u, ref v);
        }

        u %= CONST.MOD;
        if (u < 0)
        {
            u += CONST.MOD;
        }

        return u;
    }

    public ModInt GetInv()
    {
        return GetInv(Num);
    }

    public override string ToString()
    {
        return Num.ToString();
    }

    public override bool Equals(object obj)
    {
        return obj is ModInt @int && Num == @int.Num;
    }

    public override int GetHashCode()
    {
        return Num.GetHashCode();
    }

    public static bool operator ==(ModInt left, ModInt right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ModInt left, ModInt right)
    {
        return !(left == right);
    }

    public static ModInt operator +(ModInt l, ModInt r)
    {
        l.Num += r.Num;
        if (l.Num >= CONST.MOD)
        {
            l.Num -= CONST.MOD;
        }

        return l;
    }

    public static ModInt operator -(ModInt l, ModInt r)
    {
        l.Num -= r.Num;
        if (l.Num < 0)
        {
            l.Num += CONST.MOD;
        }

        return l;
    }

    public static ModInt operator *(ModInt l, ModInt r)
    {
        return new ModInt(l.Num * r.Num % CONST.MOD);
    }

    public static ModInt operator /(ModInt l, ModInt r)
    {
        return l * r.GetInv();
    }


    public static ModInt operator /(ModInt l, int r)
    {
        return l * ((ModInt)r).GetInv();
    }

    public static implicit operator ModInt(long n)
    {
        n %= CONST.MOD;
        if (n < 0)
        {
            n += CONST.MOD;
        }

        return new ModInt(n);
    }

    public bool Equals(ModInt other)
    {
        if (this == other)
        {
            return true;
        }

        return Num == other.Num;
    }
}

/// <summary>
/// セグメント木
/// 以下の操作をO(logN)で行えるデータ構造
/// - 任意の要素を上書きする
/// - 任意の区間上の最大値や合計値などを取得する
///
/// 使用例:
/// - 要素の上書きと区間の最大値を取得したい時
///   new SegTree<T>(N, Math.Max);
/// - 要素の上書きと区間の最小値を取得したい時
///   new SegTree<T>(N, Math.Min);
/// </summary>
public class SegTree<T> where T : struct
{
    /// <summary>
    /// 二分木を配列で表現したもの
    /// 葉の数がN個なら2N-1個のノードがある
    /// i番目の葉の要素番号はN - 1 + i
    /// i番目のノードの親は(i - 1) / 2
    /// i番目のノードの子は2i + 1と2i + 2
    /// </summary>
    readonly T?[] _nodes;
    readonly int _leafCnt;
    // 葉の何番目までが有効なデータか。それ以外の葉は完全二分木にするための無駄要素
    readonly int _dataCnt;
    // 子ノードを親ノードに反映させる更新する処理
    readonly Func <T, T, T> _fx;

    public SegTree(int dataCnt, Func<T, T, T> fx)
    {
        _dataCnt = dataCnt;
        _fx = fx;

        _leafCnt = 1;
        while (_leafCnt < dataCnt) _leafCnt *= 2;
        var nodeCnt = 2 * _leafCnt - 1;
        _nodes = new T?[nodeCnt];
    }

    /// <summary>
    /// 要素iにfx(_nodes[k], v)を適用する
    /// 計算量はO(longN)
    /// </summary>
    public void Set(int i, T v)
    {
        i += _leafCnt - 1;
        _nodes[i] = v;
        while (i > 0)
        {
            i = (i - 1) / 2;
            _nodes[i] = ExecFunc(_fx, _nodes[i * 2 + 1], _nodes[i * 2 + 2]);
        }
    }

    /// <summary>
    /// 全区間について更新関数で処理した結果を返す
    /// 計算量はO(longN)
    /// </summary>
    public T? QueryAll()
    {
        return Query(0, _dataCnt);
    }

    /// <summary>
    /// 指定した区間[l, r)について更新関数で処理した結果を返す
    /// 計算量はO(longN)
    /// </summary>
    public T? Query(int a, int b)
    {
        return QueryRec(a, b, 0, 0, _leafCnt);
    }

    T? QueryRec(int a, int b, int k, int l, int r)
    {
        if (r <= a || b <= l) return null;

        if (a <= l && r <= b) return _nodes[k];

        var vl = QueryRec(a, b, k * 2 + 1, l, (l + r) / 2);
        var vr = QueryRec(a, b, k * 2 + 2, (l + r) / 2, r);
        return ExecFunc(_fx, vl, vr);
    }

    // _fxなどの評価関数をnull許容型で扱うためのヘルパー
    T? ExecFunc(Func<T, T, T> func, T? a, T? b)
    {
        if (a.HasValue && b.HasValue) return func(a.Value, b.Value);
        if (a.HasValue) return a.Value;
        if (b.HasValue) return b.Value;
        return null;
    }
}

/// <summary>
/// 遅延評価セグメント木
/// 以下の操作をO(logN)で行えるデータ構造
/// - 任意の区間の値を更新する
/// - 任意の区間上の最大値や合計値などを取得する
///
/// 使用例:
/// - 区間の上書きと区間の最大値を取得したい時
///   new LazySegTree<T>(N, Math.Max, (a, b) => a);
/// - 区間への加算と区間の最小値を取得したい時
///   new LazySegTree<T>(N, Math.Min, (a, b) => a + b);
/// </summary>
public class LazySegTree<T> where T : struct
{
    /// <summary>
    /// 二分木を配列で表現したもの
    /// 葉の数がN個なら2N-1個のノードがある
    /// i番目の葉の要素番号はN - 1 + i
    /// i番目のノードの親は(i - 1) / 2
    /// i番目のノードの子は2i + 1と2i + 2
    /// </summary>
    readonly T?[] _nodes;
    readonly T?[] _lazys;
    readonly int _leafCnt;
    // 葉の何番目までが有効なデータか。それ以外の葉は完全二分木にするための無駄要素
    readonly int _dataCnt;
    // 子ノードを親ノードに反映させる更新する処理
    readonly Func <T, T, T> _fx;
    // 遅延評価を現ノードに反映する処理
    readonly Func <T, T, T> _fa;
    // 遅延評価を合算する処理
    readonly Func <T, T, T> _fm;

    public LazySegTree(int dataCnt, Func<T, T, T> fx, Func<T, T, T> fam)
    {
        _dataCnt = dataCnt;
        _fx = fx;
        // 現状は便宜、_faと_fmが同じのケースだけに対応している
        // 別にしたい時は引数を増やす
        _fa = fam;
        _fm = fam;

        _leafCnt = 1;
        while (_leafCnt < dataCnt) _leafCnt *= 2;
        var nodeCnt = 2 * _leafCnt - 1;
        _nodes = new T?[nodeCnt];
        _lazys = new T?[nodeCnt];
    }


    /// <summary>
    /// 現時点での葉の値を取得する
    /// 取得される数はコンストラクタで指定したデータ数分
    /// 計算量はO(N)
    /// </summary>
    public T?[] BuildLeafs()
    {
        // 遅延評価を全部反映する
        for (int k = 0; k < _nodes.Length; k++)
        {
            Eval(k);
        }
        return _nodes.Skip(_leafCnt - 1).Take(_dataCnt).ToArray();
    }

    /// <summary>
    /// i番目の葉の値を取得する
    /// 計算量はO(TN)
    /// O(1)で行う方法はない
    /// 全ての葉の値をまとめて取得する場合はBuildLeafsを使う
    /// </summary>
    public T? GetLeaf(int i)
    {
        return Query(i, i + 1);
    }

    /// <summary>
    /// 区間[l, r)にfa(_nodes[k], v)を適用する
    /// 計算量はO(TN)
    /// </summary>
    public void ApplyRange(int a, int b, T v)
    {
        ApplyRec(a, b, v, 0, 0, _leafCnt);
    }

    /// <param name="k">処理対象のノード番号</param>
    /// <param name="l">処理対象のノードが見ている範囲の左端</param>
    /// <param name="r">処理対象のノードが見ている範囲の右端</param>
    void ApplyRec(int a, int b, T v, int k, int l, int r)
    {
        Eval(k);
        // 区間[l, r)が区間[a, b)に完全に含まれる時
        if (a <= l && r <= b)
        {
            _lazys[k] = ExecFunc(_fm, _lazys[k], v);
            Eval(k);
        }
        // 区間[l, r)が区間[a, b)と交差する時
        else if (a < r && l < b)
        {
            var child1 = k * 2 + 1; var child2 = k * 2 + 2;
            ApplyRec(a, b, v, child1, l, (l + r) / 2);
            ApplyRec(a, b, v, child2, (l + r) / 2, r);
            _nodes[k] = ExecFunc(_fx, _nodes[child1], _nodes[child2]);
        }
    }

    /// <summary>
    /// 全区間について更新関数で処理した結果を返す
    /// 計算量はO(TN)
    /// </summary>
    public T? QueryAll()
    {
        return Query(0, _dataCnt);
    }

    /// <summary>
    /// 指定した区間[l, r)について更新関数で処理した結果を返す
    /// 計算量はO(TN)
    /// </summary>
    public T? Query(int a, int b)
    {
        return QueryRec(a, b, 0, 0, _leafCnt);
    }

    T? QueryRec(int a, int b, int k, int l, int r)
    {
        Eval(k);

        if (r <= a || b <= l) return null;

        if (a <= l && r <= b) return _nodes[k];

        var vl = QueryRec(a, b, k * 2 + 1, l, (l + r) / 2);
        var vr = QueryRec(a, b, k * 2 + 2, (l + r) / 2, r);
        return ExecFunc(_fx, vl, vr);
    }

    /// <summary>
    /// 遅延評価を反映する
    /// </summary>
    void Eval(int k)
    {
        if (!_lazys[k].HasValue) return;

        if (k < _leafCnt - 1)
        {
            // 葉でなければ子に伝播させる
            _lazys[k * 2 + 1] = ExecFunc(_fm, _lazys[k * 2 + 1], _lazys[k]);
            _lazys[k * 2 + 2] = ExecFunc(_fm, _lazys[k * 2 + 2], _lazys[k]);
        }

        // 現ノードに遅延評価を反映させる
        _nodes[k] = ExecFunc(_fa, _nodes[k], _lazys[k]);
        _lazys[k] = null;
    }

    // _fxなどの評価関数をnull許容型で扱うためのヘルパー
    T? ExecFunc(Func<T, T, T> func, T? a, T? b)
    {
        if (a.HasValue && b.HasValue) return func(a.Value, b.Value);
        if (a.HasValue) return a.Value;
        if (b.HasValue) return b.Value;
        return null;
    }
}

}