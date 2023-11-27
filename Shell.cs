using Newtonsoft.Json;
using static WFMarketTool.CommandService;

namespace WFMarketTool
{
    /// <summary>
    /// Represents the environment where a REPL will be implemented to process user commands.
    /// </summary>
    public static class Shell
    {
        public static List<string> _feedbackMessages;

        private static bool _firstDisplay = true;

        private static string _shellPrompt = "> ";

        public static string[]? consoleArgs;

        static Shell()
        {
            _feedbackMessages = new List<string>(); 
            CommandService.ReadCommandsFromJson();
        }

        public static void ResetFeedbackMessages()
        {
            _feedbackMessages.Clear();
        }

        public static void PromptInput()
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
            else if (input == null)
            {
                _feedbackMessages.Add("Input recognized as null");
                input = "";
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
            bool validName = false;


            // Check One
            // Is first argument a valid name?
            List<string> commands = CommandService.GetCommandsList();
            foreach (string command in commands)
            {
                if (command == consoleArgs[0])
                {
                    validName = true;
                    _feedbackMessages.Add($"{consoleArgs[0]} passed first check.");
                    break;
                }
            }

            // Check Two
            //TODO check commands/args against database of known ones.
            ValidateArgs();

            if (validName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ValidateArgs()
        {
            throw new NotImplementedException();
        }
    }
}
