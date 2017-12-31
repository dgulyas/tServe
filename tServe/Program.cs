using System;
using System.Configuration;
using Nancy.Hosting.Self;

namespace tServe
{
	public class Program
	{
		static void Main(string[] args)
		{
			var chunkDirectory = ConfigurationManager.AppSettings["ChunkDirectory"];
			var chunkSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);
			var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];

			Manifest.GenerateEntries(dataDirectory);

			using (var host = new NancyHost(new Uri("http://localhost:1234")))
			{
				host.Start();
				Console.WriteLine("Running on http://localhost:1234");
				Console.ReadKey(); //letting cats shutdown systems since 1999.
			}

		}
	}
}
