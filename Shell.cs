using OpenQA.Selenium.DevTools.V117.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    /// <summary>
    /// Represents the environment where a REPL will be implemented to process user commands.
    /// </summary>
    public class Shell
    {
        public enum State
        {
            Read,
            Evaluate,
            Print,
            Loop
        }

        private string? _feedbackMessage;
        private bool _firstDisplay = true;
        private string _shellPrompt = "> ";
        public State state;
        public string[]? consoleArgs;

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
            ConsoleOutput.WriteBottomText(_feedbackMessage, ConsoleColor.Red);
            _feedbackMessage = null;
            Console.Write(_shellPrompt);
        }

        public void Read()
        {
            string? input = Console.ReadLine();

            // We check against database of valid command in Evaluate(),
            // this is just if they enter nothing...
            if (input == "")
            {
                _feedbackMessage = "Unrecognized command.";
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

    public class ShellStateMachine
    {
        public Shell shell;

        public ShellStateMachine()
        {
            shell = new Shell();
        }

        public void Activate()
        {
            do
            {
                shell.Display();
                shell.Read();

                Program.Log($"Result of shell.isValidCommand(): {shell.isValidCommand()}");
            } while (!(shell.isValidCommand()));
        }
    }


    public class CommandArgument
    {
        public string? Name {  get; set; }
        public bool? Required { get; set; }
    }
    public class Command
    {
        public string Alias { get; set; }
        // How can I define required ARGS for a command? listing <add/remove> <item_name> <buy/sell> <price>
        //                                               will only support prime parts atm
    }
    internal class Batch : Command
    {
        public Batch()
        {
            this.Alias = "batch";


            List<CommandArgument> Args = new List<CommandArgument>();
        }
        internal class Toggle : CommandArgument
        {
            internal Toggle()
            {
                Name = "toggle";
                Required = false;
            }

            public void Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}
