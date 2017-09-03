using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
