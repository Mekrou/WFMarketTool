using System;
using CommandLine;

class Program
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('addlisting', Required = false, HelpText = "Add a listing to your account.")]
    }

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(o =>
               {
                   if (o.Verbose)
                   {
                       Console.WriteLine("Quick Start Example! App is in Verbose mode!");
                   }
                   else
                   {
                       Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                       Console.WriteLine("Quick Start Example!");
                   }
               });
    }
}