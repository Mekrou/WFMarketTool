using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    internal static class ConsoleOutput
    {
        // Controls the color of normal text output.
        static ConsoleColor textColor = ConsoleColor.Yellow;
        // Controls the color of the logo ASCII art.
        static ConsoleColor logoColor = ConsoleColor.Cyan;
        // Controls the color of any help text.
        static ConsoleColor helpColor = ConsoleColor.DarkCyan;
        // Controls the color of any user feedback text.
        static ConsoleColor feedbackColor = ConsoleColor.Red;

        public static void DisplayAsciiBanner()
        {
            Console.ForegroundColor = logoColor;
            Console.Write(@" __    __            __                                            _        _   " + Environment.NewLine);
            Console.Write(@"/ / /\ \ \__ _ _ __ / _|_ __ __ _ _ __ ___   ___  /\/\   __ _ _ __| | _____| |_ " + Environment.NewLine);
            Console.Write(@"\ \/  \/ / _` | '__| |_| '__/ _` | '_ ` _ \ / _ \/    \ / _` | '__| |/ / _ \ __|" + Environment.NewLine);
            Console.Write(@" \  /\  / (_| | |  |  _| | | (_| | | | | | |  __/ /\/\ \ (_| | |  |   <  __/ |_ " + Environment.NewLine);
            Console.Write(@"  \/  \/ \__,_|_|  |_| |_|  \__,_|_| |_| |_|\___\/    \/\__,_|_|  |_|\_\___|\__|" + Environment.NewLine);
            Console.Write(@"                                                                                " + Environment.NewLine);
            ResetTextColor();
        }

        public static void DisplayHelpSplash()
        {
            Console.ForegroundColor = helpColor;
            Console.WriteLine("Use 'help' to see list of commands!");
            ResetTextColor();    
        }

        public static void WriteBottomText(string text, ConsoleColor color)
        {
            ushort windowHeight = (ushort) Console.WindowHeight;
            (int, int) previousCursorPosition = Console.GetCursorPosition();
            Console.SetCursorPosition(0, windowHeight - 2);
            Console.ForegroundColor = feedbackColor;
            Console.WriteLine(text);
            Console.SetCursorPosition(previousCursorPosition.Item1, previousCursorPosition.Item2);
            ResetTextColor();
        }

        public static void ResetTextColor()
        {
            Console.ForegroundColor = textColor;
        }
    }
}
