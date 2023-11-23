using Newtonsoft.Json;
using System.IO;

namespace WFMarketTool
{
    internal static class JsonTesting
    {
        public static void ModifyJson()
        {
            string json = File.ReadAllText("AugmentsName.json");

            AugmentMods augmentMods = JsonConvert.DeserializeObject<AugmentMods>(json);

            var convert = augmentMods.ThePerrinSeqeunce.ConvertAll(s => new AugmentMod(s));

            foreach (var mod in convert) { Console.Write(mod); }
        }
    }
}
 