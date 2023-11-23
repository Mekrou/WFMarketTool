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


            Syndicate newLoka = new Syndicate();

            foreach (var item in syndicates.NewLoka)
            {
                AugmentMod currMod = new AugmentMod { ModName = item, WFMarketID = "Default"};
                newLoka.augmentMods.Add(currMod);
            }

            Syndicate perrinSeq = new Syndicate();
            foreach (var item in syndicates.ThePerrinSeqeunce)
            {
                AugmentMod currMod = new AugmentMod { ModName = item, WFMarketID = "Default" };
                perrinSeq.augmentMods.Add(currMod);
            }

            string newPerrin = JsonConvert.SerializeObject(perrinSeq);
            string newNewLoka = JsonConvert.SerializeObject(newLoka);
            File.WriteAllText("NewPerrin.json", newPerrin);
            File.WriteAllText("NewNewLoka.json", newNewLoka);
        }
    }
}
 