using System;
using System.Collections.Generic;
using System.Configuration;

namespace tServe
{
	public class Manifest : IManifest
	{
		public static int ChunkSize = Convert.ToInt32(ConfigurationManager.AppSettings["ChunkSize"]);
		public static string DataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
		public static string ChunkDirectory = ConfigurationManager.AppSettings["ChunkDirectory"];

		private static readonly List<ManifestEntry> m_entries = new List<ManifestEntry>();

		public static void GenerateEntries()
		{
			var filePaths = FileUtils.GetAllFilesNested(DataDirectory);
			filePaths.ForEach(fp => m_entries.Add(FileUtils.MakeManifestEntry(fp, DataDirectory)));
		}

		//Returns the location of the file on disk.
		public static string MakeDataPath(ManifestEntry entry)
		{
			var thing = DataDirectory + entry.File;
			return thing;
		}

		//Returns the location of the folder the chuncks should go in.
		public static string MakeChunkFolderPath(ManifestEntry entry)
		{
			return ChunkDirectory + entry.File;
		}


		//A chunk store is a folder that mirrors the data directory folder, except that each file in the file store is
		//represented by a folder with the same name as the file. Inside the folder are the chunks that make up the file.
		//eg:
		// <dataDirectory>\folderA\fileB.txt -> <chunkDirectory>\folderA\fileB.txt\1.chnk and 2.chnk
		public static void GenerateStore()
		{
			//TODO: Should we delete the old chunks?
			foreach (var entry in m_entries)
			{
				FileUtils.SplitFile(MakeDataPath(entry), MakeChunkFolderPath(entry), ChunkSize);
			}
		}

		public List<ManifestEntry> GetEntries()
		{
			return m_entries;
		}

	}
}
