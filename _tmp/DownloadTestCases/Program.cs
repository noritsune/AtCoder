using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DownloadTestCases
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("問題ページのURLを入力して下さい");
			string urlstring = Console.ReadLine();
			
			IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
			driver.Navigate().GoToUrl(urlstring);

			var samples_parent_lang_ja =
				driver.FindElement(By.Id("task-statement")).FindElement(By.ClassName("lang-ja"));
			for (int i = 0; i < 5; i++)
			{
				try
				{
					var inEle = samples_parent_lang_ja.FindElement(By.Id("pre-sample" + (i * 2)));
					var outEle = samples_parent_lang_ja.FindElement(By.Id("pre-sample" + (i * 2 + 1)));

					var path = $@"SampleInOut\{i + 1}\";
					Directory.CreateDirectory(path);
					
					File.WriteAllText(path + "In.txt", inEle.Text + "\r\n");
					File.WriteAllText(path + "Out.txt", outEle.Text + "\r\n");
				}
				catch
				{
					break;
				}
			}

			driver.Quit();
		}
	}
}