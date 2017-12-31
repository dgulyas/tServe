using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tServe
{
	public class ChunkStore
	{
		//A chunk store is a folder that mirrors the data directory folder, except that each file in the file store is
		//represented by a folder with the same name as the file. Inside the folder are the chunks that make up the file.
		//eg:
		// <dataDirectory>\folder\file.txt -> <chunkDirectory>\folder\file.txt\0_file.txt and 1_file.txt

		public void GenerateStore(List<ManifestEntry> entries)
		{
			foreach(var entry in entries)
			{

			}
		}

	}
}
