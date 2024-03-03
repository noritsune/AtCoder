using System;
using KyoPro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class SegTreeTest
    {
        [TestMethod]
        public void 要素の上書きと区間の最大値を取得できること()
        {
            var seg = new SegTree<long>(5, Math.Max);
            seg.Set(1, 2);
            seg.Set(3, 5);
            seg.Set(4, 1);
            // 更新後: [-, 2, 3, 3, -]
            Assert.AreEqual(5, seg.QueryAll());
            Assert.AreEqual(2, seg.Query(0, 2));
            Assert.AreEqual(5, seg.Query(1, 4));
            Assert.AreEqual(5, seg.Query(3, 5));
            Assert.AreEqual(null, seg.Query(0, 1));
        }
    }
}
