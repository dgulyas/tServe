using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace tServe
{
	class Program
	{
		static void Main(string[] args)
		{
			var chunckDirectory = ConfigurationManager.AppSettings["ChunckDirectory"];
			var chunckSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunckSize"]);
			var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];

			FileUtils.SplitFile("f:\\test\\test.txt", "f:\\chunck", chunckSize);

			Manifest.GenerateEntries(dataDirectory);

			//Manifest.Entries.ForEach(f => Console.WriteLine(f));

			using (var host = new NancyHost(new Uri("http://localhost:1234")))
			{
				host.Start();
				Console.WriteLine("Running on http://localhost:1234");
				Console.ReadKey(); //letting cats shutdown systems since 1999.
			}

		}
	}
}
