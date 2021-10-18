using System;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DownloadTestCases
{
	internal static class Program
	{
		private const string NAME = "noritsune";
		private const string PASS = "microsoft4037";
		
		public static void Main(string[] args)
		{
			Console.WriteLine("コンテストページのURLを入力して下さい");
			string contestPageUrl = Console.ReadLine();
			var contestPageUrlSplit = contestPageUrl.Split('/').ToList();
			if (contestPageUrlSplit.Last().Contains("tasks"))
			{
				contestPageUrlSplit.RemoveAt(contestPageUrlSplit.Count - 1);
			}

			Console.WriteLine("何問目までのテストケースが必要ですか？");
			// ReSharper disable once AssignNullToNotNullAttribute
			int problemCnt = int.Parse(Console.ReadLine());

			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
			try
			{
				for (int i = 0; i < problemCnt; i++)
				{
					char problemNameLower = (char)('a' + i);
					string contestName = contestPageUrlSplit.Last();
					var problemPageUrl = string.Join('/', contestPageUrlSplit);
					problemPageUrl += $"/tasks/{contestName}_{problemNameLower}";
					SaveTestCases(driver, problemPageUrl, char.ToUpper(problemNameLower).ToString());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.WriteLine("終了するには何かキーを押して下さい");
				Console.ReadKey();
			}
			finally
			{
				driver.Quit();
			}
		}

		/// <summary>
		/// 問題ページからテストケースを読み取ってファイルに吐き出す
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="urlString"></param>
		/// <param name="folderName"></param>
		/// <exception cref="Exception"></exception>
		private static void SaveTestCases(IWebDriver driver, string urlString, string folderName)
		{
			driver.Navigate().GoToUrl(urlString);
			
			// ログインが必要なページならログインする
			var h1S = driver.FindElements(By.TagName("h1"));
			if (h1S.Any(h1 => h1.Text == "404 Page Not Found"))
			{
				var loginButton = driver
					.FindElements(By.TagName("a")).ToList()
					.Find(ele => ele.Text.Equals("ログイン"));
				if (loginButton == null) throw new Exception("ログインが必要ですが、ログインボタンが見つかりません");
					
				loginButton.Click();

				// ログイン操作
				driver.FindElement(By.Id("username")).SendKeys(NAME);
				driver.FindElement(By.Id("password")).SendKeys(PASS);
				driver.FindElement(By.Id("submit")).Submit();
			}
			
			var samplesParentLangJa = driver
				.FindElement(By.Id("task-statement"))
				.FindElement(By.ClassName("lang-ja"));
			for (int i = 0; i < 4; i++)
			{
				try
				{
					var inEle = samplesParentLangJa.FindElement(By.Id("pre-sample" + (i * 2)));
					var outEle = samplesParentLangJa.FindElement(By.Id("pre-sample" + (i * 2 + 1)));

					var path = $@"SampleInOut\{folderName}\{i + 1}\";
					Directory.CreateDirectory(path);

					File.WriteAllText(path + "In.txt", inEle.Text + "\r\n");
					File.WriteAllText(path + "Out.txt", outEle.Text + "\r\n");
				}
				catch
				{
					// これ以上テストケースが無ければ終わり
					break;
				}
			}
		}
	}
}