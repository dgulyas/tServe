using Nancy;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Nancy.Responses;

namespace tServe
{
	public class TServe : NancyModule
	{

		public TServe(IManifest manifest)
		{
			Get["/"] = parameters => JsonConvert.SerializeObject(manifest.GetEntries());

			/*
			 *
			 * https://stackoverflow.com/questions/20121730/file-downloads-in-a-self-host-nancy-application
			 * */

			Get["/{id}/{chunkNum}"] = parameters =>
			{
				var manifestEntry = manifest.GetEntries().Single(e => e.Id == parameters.id);
				var folderPath = Manifest.MakeChunkFolderPath(manifestEntry); //TODO: This tightly couples this class to the Manifest class instead of the interface
				var filePath = $"{folderPath}\\{parameters.chunkNum}.chnk";

				if (File.Exists(filePath))
				{
					var file = new FileStream(filePath, FileMode.Open);
					string fileName = manifestEntry.Id + "-" + parameters.chunkNum;

					var response = new StreamResponse(() => file, MimeTypes.GetMimeType(fileName));
					return response.AsAttachment(fileName);
				}
				else
				{
					return HttpStatusCode.NoContent;
				}
			};
		}

	}
}
