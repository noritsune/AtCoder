using System;
using KyoPro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class LazySegTreeTest
    {
        [TestMethod]
        public void 区間の上書きと区間の最大値を取得できること()
        {
            var seg = new LazySegTree<long>(5, Math.Max, (a, b) => b);
            seg.ApplyRange(1, 3, 2);
            seg.ApplyRange(2, 4, 3);
            // 更新後: [-, 2, 3, 3, -]
            Assert.AreEqual(3, seg.QueryAll());
            Assert.AreEqual(2, seg.Query(0, 2));
            Assert.AreEqual(3, seg.Query(1, 4));
            Assert.AreEqual(3, seg.Query(3, 5));
            Assert.AreEqual(null, seg.Query(4, 5));
        }

        [TestMethod]
        public void 区間への加算と区間の最小値を取得できること()
        {
            var seg = new LazySegTree<long>(5, Math.Min, (a, b) => a + b);

            // 更新後: [-, -, -, -, 4]
            seg.ApplyRange(4, 5, 4);
            Assert.AreEqual(4, seg.QueryAll());
            Assert.AreEqual(4, seg.Query(3, 5));

            // 更新後: [-, 1, 2, -, 4]
            seg.ApplyRange(1, 2, 1);
            seg.ApplyRange(2, 3, 2);
            Assert.AreEqual(1, seg.QueryAll());
            Assert.AreEqual(4, seg.Query(3, 5));

            // 更新後: [-, 5, 6, -, 4]
            seg.ApplyRange(0, 3, 4);
            Assert.AreEqual(4, seg.QueryAll());
            Assert.AreEqual(4, seg.Query(3, 5));
        }
    }
}
