using Newtonsoft.Json;
using OpenQA.Selenium;
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
            /// The possible/valid arguments of this command. The position in the outer list represents the valid arguments for that position.
            /// Args[0] contains a lists of valid arguments for the first argument, after the command name. Example:
            /// <br> > login Args[0] Args[1]</br> 
            /// <br>Args[0]
            /// contains ["email"], since that is the only valid argument in the first spot.
            /// Args[1] contains ["password"], similarly. </br>
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

        /// <summary>
        /// Loops through command master list and finds a Command by name.
        /// </summary>
        /// <param name="name">The name of the command to find.</param>
        /// <returns>The CommandObject with the Name, Helptext, and Args info.</returns>
        public static CommandObject? FindCommandFromName(string name)
        {
            foreach (CommandObject command in commands)
            {
                if (command.Name == name)
                {
                    return command;
                }     
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>A list of valid argument names for the given command.</returns>
        public static List<string> GetPossibleArgs(string command)
        {
            throw new NotImplementedException();
            CommandObject commandObject = FindCommandFromName(command);

            foreach (List<string> possibleArgs in commandObject.Args)
            {

            }
        }
    }
}
