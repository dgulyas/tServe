using System;
using System.Collections.Generic;
using System.Linq;

namespace tServe
{
    public class Manifest : IManifest
    {
        public static List<string> Entries;

        public List<string> GetEntries()
        {
            return Entries;
        }


    }
}
