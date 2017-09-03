using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace tServe
{
	class Program
	{
		static void Main(string[] args)
		{
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
