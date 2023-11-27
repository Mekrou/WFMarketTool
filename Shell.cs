using Newtonsoft.Json;
using static WFMarketTool.CommandController;

namespace WFMarketTool
{
    /// <summary>
    /// Represents the environment where a REPL will be implemented to process user commands.
    /// </summary>
    public static class Shell
    {
        public static List<string>? _feedbackMessages = new List<string> { };
        
        private static bool _firstDisplay = true;
        
        private static string _shellPrompt = "> ";
        
        public static string[]? consoleArgs;
       
        static Shell()
        {
            CommandController.ReadCommandsFromJson();
        }

        public static void ResetFeedbackMessages()
        {
            _feedbackMessages.Clear();
        }

        public static void Display()
        {
            if (_firstDisplay)
            {
                Console.Write(_shellPrompt);
                _firstDisplay = false;
                return;
            } 

            Console.Clear();
            ConsoleOutput.DisplayAsciiBanner();
            
            // Write any feedback we got from last input.
            ConsoleOutput.WriteFeedBackText(_feedbackMessages);
            Console.Write(_shellPrompt);
        }

        public static void Read()
        {
            string? input = Console.ReadLine();

            // We check against database of valid command in Evaluate(),
            // this is just if they enter nothing...

            if (input == "")
            {
                _feedbackMessages.Add("Unrecognized command.");
            }

            // separates user input based on spaces
            consoleArgs = input.Split(' ');
        }

        /// <summary>
        /// Evaluates user input against database of commands in TODO: commands
        /// </summary>
        /// <returns>Whether or not user entered a valid command.</returns>
        public static bool isValidCommand()
        {
            // Check One
            // Is first argument a valid name?
            List<string> commands = CommandController.GetCommandsList();
            foreach (string command in commands)
            {
                if (!(command == consoleArgs[0]))
                {
                    return false;
                }
                else
                {
                    _feedbackMessages.Add($"{consoleArgs[0]} passed first check.");
                    break;
                }
            }

            // Check Two



            //TODO check commands/args against database of known ones.
            foreach (string arg in consoleArgs)
            {
                Console.WriteLine(arg);
            }

            //TODO remove this later plzz
            if (consoleArgs.Contains("batch")) { return true; } else { return false; }
        }

        public static bool areAllArgsValid()
        {
            return false;
        }
    }
}
