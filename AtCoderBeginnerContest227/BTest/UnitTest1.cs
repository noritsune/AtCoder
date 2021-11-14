using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtCoder;

namespace Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void Test1()
		{
			const string pathOffset = "../../../SampleInOut/1/";
			TestInOut(pathOffset + "In.txt", pathOffset + "Out.txt");
		}
		
		[TestMethod]
		public void Test2()
		{
			const string pathOffset = "../../../SampleInOut/2/";
			TestInOut(pathOffset + "In.txt", pathOffset + "Out.txt");
		}
		
		// [TestMethod]
		// public void Test3()
		// {
		// 	const string pathOffset = "../../../SampleInOut/3/";
		// 	TestInOut(pathOffset + "In.txt", pathOffset + "Out.txt");
		// }
		//
		// [TestMethod]
		// public void Test4()
		// {
		// 	const string pathOffset = "../../../SampleInOut/4/";
		// 	TestInOut(pathOffset + "In.txt", pathOffset + "Out.txt");
		// }
        
		private static void TestInOut(string inputFileName, string outputFileName)
		{
			using var input = new StreamReader(inputFileName);
			using var output = new StringWriter();
			Console.SetOut(output);
			Console.SetIn(input);

			SolveExecuter.Main();

			Assert.AreEqual(File.ReadAllText(outputFileName), output.ToString());
		}
	}
}