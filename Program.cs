using Newtonsoft.Json;
using System.Text;
using WFMarketTool;

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Controls;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using DustInTheWind.ConsoleTools.CommandLine;
using DustInTheWind.ConsoleTools.Controls.Menus;
using System.Security.AccessControl;

class Program
{
    static async Task Main(string[] args)
    {
        string apiBaseUrl = "https://api.warframe.market/v1";

        CustomConsole.WriteLine(HorizontalAlignment.Center, "Welcome to WFMarketTool");
        HorizontalLine titleLine = new HorizontalLine
        {
            Character = '*',
            ForegroundColor = ConsoleColor.Cyan,
            Margin = "0 0",
        };
        titleLine.Display();

        Console.Clear();

        //WebDriver webDriver = new WebDriver();
        //webDriver.Login();


        //Credentials.CheckCredentials();

        //await WFMarketTask.TokenSignIn();
        //await WFMarketTask.GetItemId();

        Log("Reached end");
        Console.ReadLine();
    }

    public static void Log(string message,
                    [CallerFilePath] string? file = null,
                    [CallerLineNumber] int line = 0)
    {
        if (GlobalState.Verbose == true)
        {
            Console.WriteLine("{0} ({1}): {2}", Path.GetFileName(file), line, message);
        }
    }
}