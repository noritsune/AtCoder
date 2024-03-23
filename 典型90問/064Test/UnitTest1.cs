using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KyoPro;

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
		
		// [TestMethod]
		// public void Test3()
		// {
		// 	TestInOut(testCaseDirPath + "3In.txt", testCaseDirPath + "3Out.txt");
		// }
		
		// [TestMethod]
		// public void Test4()
		// {
		// 	TestInOut(testCaseDirPath + "4In.txt", testCaseDirPath + "4Out.txt");
		// }
		
		private static void TestInOut(string inputFileName, string outputFileName)
		{
			// if (!File.Exists(inputFileName)) return;
			
			using var input = new StreamReader(inputFileName);
			using var output = new StringWriter();
			Console.SetOut(output);
			Console.SetIn(input);

			EntryPoint.Main();

			string expected = File.ReadAllText(outputFileName);
			string actual = output.ToString();
			
			// 改行コードを揃える
			expected = expected.Replace("\r\n", "\n");
			actual = actual.Replace("\r\n", "\n");

			Assert.AreEqual(expected, actual);
		}
	}
}