using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using System.Text;

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

			folders.ForEach(f => f = Path.Combine(folderPath, f));

			return folders;
		}

		public static ManifestEntry MakeManifestEntry(string filePath)
		{
			var fileInfo = new FileInfo(filePath);
			return new ManifestEntry { FilePath = filePath, FileSize = fileInfo.Length };
		}

		/// <summary>
		/// Split a file into chunks
		/// </summary>
		/// <param name="filePath">The file to split</param>
		/// <param name="destinationPath">The folder that the chuncks should be put in</param>
		/// <param name="chunckSize">The size of each chunck (last chunck is likely smaller)</param>
		/// https://msdn.microsoft.com/en-us/library/system.io.filestream(v=vs.110).aspx
		public static void SplitFile(string filePath, string destinationPath, int chunckSize)
		{
			var chunckNumber = 1;
			using (FileStream fs = File.OpenRead(filePath))
			{
				byte[] chunck = new byte[chunckSize];
				UTF8Encoding temp = new UTF8Encoding(true);
				while (fs.Read(chunck, 0, chunck.Length) > 0)
				{
					var chunckFilePath = MakeChunckFilePath(filePath, destinationPath, chunckNumber);
					WriteChunckToFile(chunckFilePath, chunck);
					Console.WriteLine(temp.GetString(chunck));
					chunckNumber++;
				}
			}
		}

		/// <summary>
		/// Buisiness logic for naming chuncks.
		/// {destinationPath}\{chunckNumber}_{fileName}.chnk
		/// </summary>
		/// <param name="filePath">The path to the file that's being split into chuncks</param>
		/// <param name="destinationPath">The path to the folder that the chuncks are going to</param>
		/// <param name="chunckNumber">This chuncks number</param>
		/// <returns></returns>
		public static string MakeChunckFilePath(string filePath, string destinationPath, int chunckNumber)
		{
			var fileName = Path.GetFileName(filePath);
			fileName = $"{chunckNumber}_{fileName}.chnk";

			return Path.Combine(destinationPath, fileName);
		}

		public static void WriteChunckToFile(string chunckFilePath, byte[] chunck)
		{
			using (FileStream fs = File.Create(chunckFilePath))
			{
				fs.Write(chunck, 0, chunck.Length);
			}
		}

	}
}
