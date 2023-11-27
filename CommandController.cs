using Newtonsoft.Json;
using System.Collections;

namespace WFMarketTool
{
    /// <summary>
    /// Controls deserialization of command list from json and stores command master list.
    /// </summary>
    internal static class CommandController
    {
        private static CommandRootObject _commands { get; set; }
        public static List<CommandInfo> commands { get; set; }

        // For Json
        public class CommandRootObject
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
            _commands = JsonConvert.DeserializeObject<CommandRootObject>(json);
        }

        static CommandController()
        {
            ReadCommandsFromJson();
            commands = _commands.commands;
        }

        /// <summary>
        /// Loops through command list to return clean list of all known command names.
        /// </summary>
        /// <returns>A list of known command names.</returns>
        public static List<string> GetCommandsList() {
            List<string> commandNames = new List<string>();
            foreach (CommandInfo commandInfo in commands)
            {
                commandNames.Add(commandInfo.Name);
            }
            return commandNames;
        }
    }
}
