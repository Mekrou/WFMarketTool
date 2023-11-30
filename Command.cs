using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using static WFMarketTool.CommandService;

namespace WFMarketTool
{
    public class CommandArgumentLike
    {
        private string? name;
        public string Name
        {
            get { return name; }
        }

        protected void SetName(string value)
        {
            name = value;
        }
    }


    public abstract class Command : CommandArgumentLike
    {
        private Dictionary<string, List<Argument>> arguments = new Dictionary<string, List<Argument>>();

        protected void AddArgument(string argPos, Argument argument)
        {
            List<Argument>? argumentsAtArgPos;
            if (arguments.TryGetValue(argPos, out argumentsAtArgPos))
            {
                argumentsAtArgPos.Add(argument);
                arguments.Add(argPos, argumentsAtArgPos);
            }
        }
        protected void AddArguments(string argPos, List<Argument> arguments) 
        {
            this.arguments.Add(argPos, arguments);
        }
    }

    public abstract class Argument : CommandArgumentLike
    {
        private bool required;

        public bool Required
        {
            get { return required; }
        }

        protected void SetRequired(bool value)
        {
            required = value;
        }
        protected abstract void Execute();
    }

    /// <summary>
    /// Stores all functions related to the "login" command.
    /// </summary>
    public class Login : Command
    {
        public Login()
        {
            SetName("login");
            AddArgument("arg1", new Email());
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class Email : Argument
    {
        public Email()
        {
            SetName("email");
            SetRequired(true);
        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }

}
