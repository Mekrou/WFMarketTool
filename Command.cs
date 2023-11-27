using System;
using Newtonsoft.Json;
using static WFMarketTool.CommandController;

namespace WFMarketTool
{
    public class Command
    {
        internal string Name { get; set; }

        public Command() { 
        
        }

        internal static bool isValid()
        {
            // Get list of valid commands

            // Loop through each, checking if consoleArg[0] matches any
            // return result

            return false;
        }
    }
}
