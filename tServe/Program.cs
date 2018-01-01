using System;
using Nancy.Hosting.Self;

namespace tServe
{
	public class Program
	{
		static void Main()
		{
			Manifest.GenerateEntries();
			Manifest.GenerateStore();

			using (var host = new NancyHost(new Uri("http://localhost:1234")))
			{
				host.Start();
				Console.WriteLine("Running on http://localhost:1234");
				Console.ReadKey(); //letting cats shutdown systems since 1999.
			}

		}
	}
}
