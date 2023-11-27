using Newtonsoft.Json;
using static WFMarketTool.CommandController;

namespace WFMarketTool
{
    /// <summary>
    /// Represents the environment where a REPL will be implemented to process user commands.
    /// </summary>
    public class Shell
    {
        public static List<string>? _feedbackMessages = new List<string> { };
        
        private bool _firstDisplay = true;
        
        private string _shellPrompt = "> ";
        
        public string[]? consoleArgs;
       
        public static void ResetFeedbackMessages()
        {
            _feedbackMessages.Clear();
        }

        public void Display()
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

        public void Read()
        {
            string? input = Console.ReadLine();

            // We check against database of valid command in Evaluate(),
            // this is just if they enter nothing...

            CommandController.ReadCommandsFromJson();
            List<string> commands = CommandController.GetCommandsList();
            foreach (string command in commands)
            {
                _feedbackMessages.Add(command);
            }

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
        public bool isValidCommand()
        {
            //TODO check commands/args against database of known ones.
            foreach (string arg in consoleArgs)
            {
                Console.WriteLine(arg);
            }

            //TODO remove this later plzz
            if (consoleArgs.Contains("batch")) { return true; } else { return false; }
        }

        public bool areAllArgsValid()
        {
            return false;
        }
    }
}
