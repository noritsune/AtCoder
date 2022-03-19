using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtCoder;

namespace Test
{
	[TestClass]
	public class UnitTest1
	{
		const string testCaseDirPath = "../../../SampleInOut/";
		
		[TestMethod]
		public void Test1()
		{
			TestInOut(testCaseDirPath + "1In.txt", testCaseDirPath + "1Out.txt");
		}
		
		[TestMethod]
		public void Test2()
		{
			TestInOut(testCaseDirPath + "2In.txt", testCaseDirPath + "2Out.txt");
		}
		
		[TestMethod]
		public void Test3()
		{
			TestInOut(testCaseDirPath + "3In.txt", testCaseDirPath + "3Out.txt");
		}
		
		[TestMethod]
		public void Test4()
		{
			TestInOut(testCaseDirPath + "4In.txt", testCaseDirPath + "4Out.txt");
		}
		
		[TestMethod]
		public void Test5()
		{
			TestInOut(testCaseDirPath + "5In.txt", testCaseDirPath + "5Out.txt");
		}
		
		[TestMethod]
		public void Test6()
		{
			TestInOut(testCaseDirPath + "6In.txt", testCaseDirPath + "6Out.txt");
		}
		
		// [TestMethod]
		// public void Test7()
		// {
		// 	TestInOut(testCaseDirPath + "7In.txt", testCaseDirPath + "7Out.txt");
		// }
		
		[TestMethod]
		public void Test8()
		{
			TestInOut(testCaseDirPath + "8In.txt", testCaseDirPath + "8Out.txt");
		}
		
		private static void TestInOut(string inputFileName, string outputFileName)
		{
			// if (!File.Exists(inputFileName)) return;
			
			using var input = new StreamReader(inputFileName);
			using var output = new StringWriter();
			Console.SetOut(output);
			Console.SetIn(input);

			SolveExecuter.Main();

			string expected = File.ReadAllText(outputFileName);
			string actual = output.ToString();
			
			// 改行コードを揃える
			expected = expected.Replace("\r\n", "\n");
			actual = actual.Replace("\r\n", "\n");

			Assert.AreEqual(expected, actual);
		}
	}
}