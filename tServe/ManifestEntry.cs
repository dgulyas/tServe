namespace tServe
{
	public class ManifestEntry
	{
		//Where the file is on the harddrive. This shouldn't be shown in the web service.
		public string FilePath;
		public bool ShouldSerializeFilePath() { return false; }

		public double FileSizeBytes;

		//The file path of the file relative to the file store directory. This is the unique key for this file.
		public string File;

	}
}
