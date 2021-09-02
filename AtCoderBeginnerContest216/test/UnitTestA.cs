using System;
using Xunit;
using A;

namespace test
{
    public class UnitTestA
    {
        [Fact(DisplayName = "テストケース1")]
        public void Test1()
        {
            var XY = 15.8;
            
            var sol = new A.Sol();
            string ans = sol.Solve(XY);
            
            Assert.Equal("15+", ans);
        }
        
        [Fact(DisplayName = "テストケース2")]
        public void Test2()
        {
            var XY = 1.0;
            
            var sol = new A.Sol();
            string ans = sol.Solve(XY);
            
            Assert.Equal("1-", ans);
        }
        
        [Fact(DisplayName = "テストケース3")]
        public void Test3()
        {
            var XY = 12.5;
            
            var sol = new A.Sol();
            string ans = sol.Solve(XY);
            
            Assert.Equal("12", ans);
        }
        
        [Fact(DisplayName = "テストケース4")]
        public void Test4()
        {
            var XY = 12.9;
            
            var sol = new A.Sol();
            string ans = sol.Solve(XY);
            
            Assert.Equal("12+", ans);
        }
    }
}
