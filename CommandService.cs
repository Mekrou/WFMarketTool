using Newtonsoft.Json;
using System.Collections;

namespace WFMarketTool
{
    /// <summary>
    /// Controls deserialization of command list from json and stores command master list.
    /// </summary>
    internal static class CommandService
    {
        private static CommandRootObject commandRootObject { get; set; }
        public static List<CommandObject> commands { get; set; }

        /// <summary>
        /// Represents the encapsulating "commands" list inside of Commands.json
        /// </summary>
        public class CommandRootObject
        {
            [JsonProperty("commands")]
            public List<CommandObject> commands { get; set; }
        }

        /// <summary>
        /// Represents the individual command objects inside of Commands.json
        /// </summary>
        public class CommandObject
        {
            /// <summary>
            /// The name of the command.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// The help information that will be displayed on the 'help' command.
            /// </summary>
            [JsonProperty("helptext")]
            public string HelpText { get; set; }
            
            /// <summary>
            /// The possible/valid arguments of this command.
            /// </summary>
            [JsonProperty("args")]
            public List<List<string>> Args { get; set; }
        }
        public class ArgumentModel
        {

        }

        public static void ReadCommandsFromJson()
        {
            string json = File.ReadAllText("Commands.json");
            commandRootObject = JsonConvert.DeserializeObject<CommandRootObject>(json);
        }

        static CommandService()
        {
            ReadCommandsFromJson();
            commands = commandRootObject.commands;
        }

        /// <summary>
        /// Loops through command list to return clean list of all known command names.
        /// </summary>
        /// <returns>A list of known command names.</returns>
        public static List<string> GetCommandsList() {
            List<string> commandNames = new List<string>();
            foreach (CommandObject commandInfo in commands)
            {
                commandNames.Add(commandInfo.Name);
            }
            return commandNames;
        }
    }
}
