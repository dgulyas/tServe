using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using tServe;

namespace tClient
{
	class Program
	{
		private static RestClient m_client;
		private static string m_downloadPath;

		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				m_downloadPath = Directory.GetCurrentDirectory();
			}
			else
			{
				if (!File.Exists(args[0]))
				{
					Console.WriteLine("The provided path doesn't exist.");
				}

				m_downloadPath = args[0];
			}


			m_client = new RestClient("http://localhost:1234");

			var request = new RestRequest(Method.GET);
			IRestResponse response = m_client.Execute(request);
			var mEntries = JsonConvert.DeserializeObject<List<ManifestEntry>>(response.Content);

			for (int i = 0; i < mEntries.Count; i++)
			{
				Console.WriteLine($"FileNum: {i} Bytes: {mEntries[i].FileSizeBytes} File: {mEntries[i].File}");
			}

			Console.Write("Which file do you want: ");
			var stringNum = Console.ReadLine();

			int fileNum;
			try
			{
				fileNum = Convert.ToInt32(stringNum);
			}
			catch (Exception e)
			{
				Console.WriteLine("Couldn't parse int.");
				Console.WriteLine(e);
				throw;
			}

			var entry = mEntries[fileNum];

			DownloadFile(entry);

		}

		private static void DownloadFile(ManifestEntry entry)
		{
			var fileLocation = m_downloadPath + entry.File;

			//TODO Should the file be deleted or over written it if exists? Throw an exception?
			using (System.IO.StreamWriter file =
				new System.IO.StreamWriter(fileLocation))
			{
				var chunkNum = 1;
				IRestResponse response;
				do
				{
					var request = new RestRequest($"{entry.Id}/{chunkNum}", Method.GET);
					response = m_client.Execute(request);

					file.Write(response.Content);
					file.Flush();

					chunkNum++;
				} while (response.Content.Length > 0);
			}
		}

	}
}
