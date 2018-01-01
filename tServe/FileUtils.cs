using System;
using System.Collections.Generic;
using System.IO;

namespace tServe
{
	public static class FileUtils
	{

		/// <summary>
		/// Get a list of all files in or below a folder.
		/// </summary>
		/// <param name="folderPath">The location of the folder we want to look in.</param>
		/// <returns>All the files in a folder even if they're inside other folders. Returns full paths.</returns>
		public static List<string> GetAllFilesNested(string folderPath)
		{
			var files = new List<string>();
			var folders = new List<string> { folderPath }; //A list of folders (full path) that we need to look in.

			while (folders.Count > 0)
			{
				//pop off the next folder
				var currentFolder = folders[0];
				folders.RemoveAt(0);

				//add the files in that folder to our collection
				files.AddRange(GetFilesInFolderAbsolute(currentFolder));

				//add the folders in the current folder to the list of folders we need to look in
				folders.AddRange(Directory.GetDirectories(currentFolder));
			}

			return files;
		}

		/// <summary>
		/// Returns a list of the files in a folder, with a full path (C:\folderA\folderB\fileA)
		/// </summary>
		/// <returns></returns>
		public static List<string> GetFilesInFolderAbsolute(string folderPath)
		{
			var folders = new List<string>();
			folders.AddRange(Directory.GetFiles(folderPath));

			folders.ForEach(f => f = Path.Combine(folderPath, f)); //TODO: Does this do anything?

			return folders;
		}

		public static ManifestEntry MakeManifestEntry(string filePath, string dataDirectory)
		{
			var fileInfo = new FileInfo(filePath);
			return new ManifestEntry {
				FileSizeBytes = fileInfo.Length,
				//TODO: Is it better to chop off the first x characters instead?
				File = filePath.Replace(dataDirectory, ""),
				Id = Guid.NewGuid().ToString().Replace("-", "")
			};
		}

		/// <summary>
		/// Split a file into chunks
		/// </summary>
		/// <param name="filePath">The file to split</param>
		/// <param name="destinationPath">The folder that the chunks should be put in</param>
		/// <param name="chunkSize">The size of each chunk (last chunk is likely smaller)</param>
		/// https://msdn.microsoft.com/en-us/library/system.io.filestream(v=vs.110).aspx
		public static void SplitFile(string filePath, string destinationPath, int chunkSize)
		{
			var chunkNumber = 1;
			using (FileStream fs = File.OpenRead(filePath))
			{
				byte[] chunk = new byte[chunkSize];
				while (fs.Read(chunk, 0, chunk.Length) > 0)
				{
					var chunkFilePath = MakeChunkFilePath(filePath, destinationPath, chunkNumber);
					WriteChunkToFile(chunkFilePath, chunk);
					chunkNumber++;
				}
			}
		}

		/// <summary>
		/// Business logic for naming chunks.
		/// {destinationPath}\{chunkNumber}.chnk
		/// </summary>
		/// <param name="filePath">The path to the file that's being split into chunks</param>
		/// <param name="destinationPath">The path to the folder that the chunks are going to</param>
		/// <param name="chunkNumber">This chunks number</param>
		/// <returns></returns>
		public static string MakeChunkFilePath(string filePath, string destinationPath, int chunkNumber)
		{
			var fileName = $"{chunkNumber}.chnk";
			return Path.Combine(destinationPath, fileName);
		}

		public static void WriteChunkToFile(string chunkFilePath, byte[] chunk)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(chunkFilePath));


			using (FileStream fs = File.Create(chunkFilePath))
			{
				fs.Write(chunk, 0, chunk.Length);
			}
		}

	}
}
