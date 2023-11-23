using Newtonsoft.Json;

namespace WFMarketTool
{
    internal class Syndicates
    {
        [JsonProperty("New Loka")]
        public Syndicate NewLoka {  get; set; }

        [JsonProperty("The Perrin Sequence")]
        public Syndicate ThePerrinSeqeunce { get; set; }

    }

    internal class OldSyndicates
    {
        [JsonProperty("New Loka")]
        public List<string> NewLoka { get; set; }

        [JsonProperty("The Perrin Sequence")]
        public List<string> ThePerrinSeqeunce { get; set; }
    }

    public class Syndicate
    {
        public Syndicate()
        {
            isTracked = false;
            augmentMods = new List<AugmentMod> { };
        }

        [JsonProperty("isTracked")]
        public bool isTracked { get; set; }

        [JsonProperty("AugmentMods")]
        public List<AugmentMod> augmentMods { get; set; }
    }

    public class AugmentMod
    {
        public AugmentMod()
        {
            ModName = string.Empty;
            WFMarketID = string.Empty;
        }

        [JsonProperty("modName")]
        public string ModName { get; set; }
        [JsonProperty("WFMarketID")]
        public string WFMarketID { get; set; }
    }
}
