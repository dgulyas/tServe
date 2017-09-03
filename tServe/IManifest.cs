using System.Collections.Generic;

namespace tServe
{
    public interface IManifest
    {
        List<string> GetEntries();
    }
}
