using System;
using System.Collections.Generic;
using System.Configuration;

namespace tServe
{
	public class Manifest : IManifest
	{
		public static List<ManifestEntry> Entries = new List<ManifestEntry>();
		public static int ChunckSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);
		public static string DataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
		public static string chunkDirectory = ConfigurationManager.AppSettings["ChunkDirectory"];

		public static void GenerateEntries(string dataDirectory)
		{
			var filePaths = FileUtils.GetAllFilesNested(dataDirectory);
			filePaths.ForEach(fp => Entries.Add(FileUtils.MakeManifestEntry(fp)));
		}

		public List<ManifestEntry> GetEntries()
		{
			return Entries;
		}


	}
}
