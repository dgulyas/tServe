using System;
using System.Configuration;
using Nancy.Hosting.Self;

namespace tServe
{
	class Program
	{
		static void Main(string[] args)
		{
			var chunkDirectory = ConfigurationManager.AppSettings["ChunkDirectory"];
			var chunckSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);
			var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];

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
