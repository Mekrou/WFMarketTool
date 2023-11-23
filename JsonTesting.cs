using Newtonsoft.Json;
using System.IO;

namespace WFMarketTool
{
    internal static class JsonTesting
    {
        public static void ModifyJson()
        {
            string json = File.ReadAllText("AugmentsName.json");

            OldSyndicates syndicates = JsonConvert.DeserializeObject<OldSyndicates>(json);


        }
    }
}
 