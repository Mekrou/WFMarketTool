using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    internal static class ConsoleOutput
    {
        public static void DisplayAsciiBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(@" __    __            __                                            _        _   " + Environment.NewLine);
            Console.Write(@"/ / /\ \ \__ _ _ __ / _|_ __ __ _ _ __ ___   ___  /\/\   __ _ _ __| | _____| |_ " + Environment.NewLine);
            Console.Write(@"\ \/  \/ / _` | '__| |_| '__/ _` | '_ ` _ \ / _ \/    \ / _` | '__| |/ / _ \ __|" + Environment.NewLine);
            Console.Write(@" \  /\  / (_| | |  |  _| | | (_| | | | | | |  __/ /\/\ \ (_| | |  |   <  __/ |_ " + Environment.NewLine);
            Console.Write(@"  \/  \/ \__,_|_|  |_| |_|  \__,_|_| |_| |_|\___\/    \/\__,_|_|  |_|\_\___|\__|" + Environment.NewLine);
            Console.Write(@"                                                                                " + Environment.NewLine);
            Console.ResetColor();
        }

        public static void DisplayHelpSplash()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Use 'help' to see list of commands!");
        }
    }
}
