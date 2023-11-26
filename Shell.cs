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
    internal class Shell
    {
        enum State
        {
            Read,
            Evaluate,
            Print,
            Loop
        }

        State state;
        public static string[] consoleArgs;

        public static void Display()
        {
            Console.Clear();
            Console.Write("> ");
        }

        public static void Read()
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Command not recognized");
            }

            // separates user input based on spaces
            consoleArgs = input.Split(' ');
        }

        public static void Evaluate()
        {
            foreach (string arg in consoleArgs)
            {
                Console.WriteLine(arg);
            }
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
