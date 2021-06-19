using Microsoft.VisualStudio.TestTools.UnitTesting;
using util;

namespace _test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BinarySearchTest()
        {
            int[] array = new int[] {0, 5, 10};

            int index0 = Sol.BinarySearch(array, 0);
            Assert.AreEqual(0, index0);

            int index1 = Sol.BinarySearch(array, 5);
            Assert.AreEqual(1, index1);

            int index2 = Sol.BinarySearch(array, 10);
            Assert.AreEqual(2, index2);

            int indexMinus = Sol.BinarySearch(array, 3);
            Assert.AreEqual(-1, indexMinus);
        }

        [TestMethod]
        public void LowerBoundTest() {
            int[] array = new int[] {0, 5, 10};

            int index0 = Sol.LowerBound(array, -1);
            Assert.AreEqual(0, index0);

            index0 = Sol.LowerBound(array, 0);
            Assert.AreEqual(0, index0);

            int index1 = Sol.LowerBound(array, 1);
            Assert.AreEqual(1, index1);

            index1 = Sol.LowerBound(array, 5);
            Assert.AreEqual(1, index1);

            int index2 = Sol.LowerBound(array, 6);
            Assert.AreEqual(2, index2);

            index2 = Sol.LowerBound(array, 10);
            Assert.AreEqual(2, index2);

            int index3 = Sol.LowerBound(array, 11);
            Assert.AreEqual(3, index3);
        }

        [TestMethod]
        public void UpperBoundTest() {
            int[] array = new int[] {0, 5, 10};

            int index0 = Sol.UpperBound(array, -1);
            Assert.AreEqual(0, index0);

            int index1 = Sol.UpperBound(array, 0);
            Assert.AreEqual(1, index1);

            index1 = Sol.UpperBound(array, 4);
            Assert.AreEqual(1, index1);

            int index2 = Sol.UpperBound(array, 5);
            Assert.AreEqual(2, index2);

            index2 = Sol.UpperBound(array, 9);
            Assert.AreEqual(2, index2);

            int index3 = Sol.UpperBound(array, 10);
            Assert.AreEqual(3, index3);

            index3 = Sol.UpperBound(array, 11);
            Assert.AreEqual(3, index3);
        }
    }
}
