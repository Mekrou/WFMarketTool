using Newtonsoft.Json;
using OpenQA.Selenium;
using System.Collections;

namespace WFMarketTool
{
    /// <summary>
    /// Registers, stores, and controls access to commands available in Shell.
    /// </summary>
    internal static class CommandService
    {
        private static Dictionary<string, Command> commands = new Dictionary<string, Command>();

        static CommandService()
        { 
            commands.Add("login", new Login());         
        }


        /// <summary>
        /// Looks up valid commands, returning their names as a list of strings.
        /// </summary>
        /// <returns>The list of valid commands.</returns>
        public static List<string> GetCommandNames()
        {
            var commandNames = CommandService.commands.Keys;
            List<string> commands = CommandService.commands.Keys.ToList<string>();
            return commands;
        }

        /// <summary>
        /// Searches command master list for the Command object that matches its name.
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <returns>The Command object of the given name. If it is not found, null.</returns>
        static Command? GetCommandByName(string name)
        {
            foreach (var kvp in commands)
            {                 
                if (kvp.Key == name)
                {
                    return kvp.Value;
                }
            }
            return null;
        }
    }
}
