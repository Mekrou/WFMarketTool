using Newtonsoft.Json;
using static WFMarketTool.CommandService;

namespace WFMarketTool
{
    /// <summary>
    /// Represents the environment where a REPL will be implemented to process user commands.
    /// </summary>
    public static class Shell
    {
        /// <summary>
        /// A list of messages that will be shown to the user after they Enter input.
        /// </summary>
        public static List<string> _feedbackMessages;

        private static bool _firstDisplay = true;

        /// <summary>
        /// The prompt the user sees when entering commands.
        /// </summary>
        private static string _shellPrompt = "> ";

        /// <summary>
        /// An array of the commands that were last entered by the user.
        /// </summary>
        public static string[]? input;

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
            string? dirtyInput = Console.ReadLine();

            // We check against database of valid command in Evaluate(),
            // this is just if they enter nothing...

            if (dirtyInput == "")
            {
                _feedbackMessages.Add("Unrecognized command.");
            }
            else if (dirtyInput == null)
            {
                _feedbackMessages.Add("Input recognized as null");
                dirtyInput = "";
            }

            // separates user input based on spaces
            Shell.input = dirtyInput.Split(' ');
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
                if (command == input[0])
                {
                    validName = true;
                    _feedbackMessages.Add($"{input[0]} passed first check.");
                    break;
                }
            }

            // Check Two
            //TODO check commands/args against database of known ones.

            if (validName)
            {
                ValidateArgs();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void ValidateArgs()
        {
            Dictionary<int, bool> argValid = new Dictionary<int, bool>();

            // Get list of arguments
            CommandObject command = CommandService.FindCommandFromName(input[0]);
            List<List<string>> Args = command.Args;

            for(int i = 0; i < Args.Count; i++)
            {
                foreach (string arg in Args[i])
                {
                    try
                    {
                        if (arg == input[i + 1])
                        {
                            argValid.Add(i, true);
                        }
                        else
                        {
                            argValid.Add(i, false);
                        }
                    }
                    catch (IndexOutOfRangeException ex) 
                    {
                        Shell._feedbackMessages.Add(ex.Message);
                    }
                }
            }
        }
    }
}
