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
        

        CustomConsole.WriteLine(HorizontalAlignment.Center, "Welcome to WFMarketTool");
        HorizontalLine titleLine = new HorizontalLine
        {
            Character = '*',
            ForegroundColor = ConsoleColor.Cyan,
            Margin = "0 0",
        };
        titleLine.Display();


        //WebDriver webDriver = new WebDriver();
        //webDriver.Login();


        Credentials.CheckCredentials();

        await WFMarketTask.TokenSignIn();
        string corpusScene = await WFMarketTask.GetItemId("corpus_ice_planet_wreckage_scene");
        Log($"Corpus ID: {corpusScene}");

        Log("Enter something to send your order");
        Console.ReadLine();
        await WFMarketTask.CreateOrder( corpusScene );
        

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