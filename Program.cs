using WFMarketTool;
using System.Runtime.CompilerServices;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        WFMarketTask task = new WFMarketTask();

        ConsoleOutput.DisplayAsciiBanner();
        ConsoleOutput.DisplayHelpSplash();

        Credentials.CheckCredentials();

        ShellController shellState = new ShellController();
        shellState.Activate();

        //WebDriver webdriver = new WebDriver();
        //webdriver.Login();


        //await WFMarketTask.TokenSignIn();
        //string corpusScene = await WFMarketTask.GetItemId("corpus_ice_planet_wreckage_scene");
        //Log($"Corpus ID: {corpusScene}");

        //Log("Enter something to send your order");
        //Console.ReadLine();
        //await WFMarketTask.CreateOrder("corpus_ice_planet_wreckage_scene");


        //Log("Reached end");
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