using Nancy;
using Newtonsoft.Json;

namespace tServe
{
	public class TServe : NancyModule
	{

		public TServe(IManifest manafest)
		{
			Get["/"] = parameters => JsonConvert.SerializeObject(manafest.GetEntries());
		}

	}
}
