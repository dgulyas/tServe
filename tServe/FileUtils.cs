using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;

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
        
	}
}
