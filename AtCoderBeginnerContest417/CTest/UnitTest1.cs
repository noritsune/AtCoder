using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using KyoPro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class TestClass
    {
        const string testCaseDirPath = "../../../test/";

        public static IEnumerable<object[]> TestCases
        {
            get
            {
                var inputFiles = Directory.GetFiles(testCaseDirPath, "*in.txt");
                foreach (var inputFile in inputFiles)
                {
                    var caseNum = Regex.Match(inputFile, @"(\d+)[Ii]n\.txt").Groups[1].Value;
                    yield return new object[] { caseNum };
                }
            }
        }

        [TestMethod]
        [DynamicData(nameof(TestCases))]
        public void ExecTest(string caseNum)
        {
            var inputFileName = testCaseDirPath + caseNum + "in.txt";
            var outputFileName = testCaseDirPath + caseNum + "out.txt";
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
