using System;
using System.Collections.Generic;
using System.Linq;

namespace tServe
{
	public class Manifest : IManifest
	{
		public static List<ManifestEntry> Entries = new List<ManifestEntry>();

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
