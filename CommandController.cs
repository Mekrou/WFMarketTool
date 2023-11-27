using Newtonsoft.Json;

namespace WFMarketTool
{
    internal class CommandController
    {
        public class CommandList
        {
            public List<CommandInfo> commands { get; set; }
        }
        public class CommandInfo
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("helptext")]
            public string helpText { get; set; }
        }
        public static void ReadCommandsFromJson()
        {
            string json = File.ReadAllText("Commands.json");
            CommandList commands = JsonConvert.DeserializeObject<CommandList>(json);
        }

    }
}
