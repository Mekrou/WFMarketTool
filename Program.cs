using CommandLine;
using Newtonsoft.Json;
using System.Text;
using WFMarketTool;

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Controls;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

class Program
{
    internal static void Log(string message,
                    [CallerFilePath] string? file = null,
                    [CallerLineNumber] int line = 0)
    {
        Console.WriteLine("{0} ({1}): {2}", Path.GetFileName(file), line, message);
    }

    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('a', "addlisting", Required = false, HelpText = "Add a listing to your account.")]
        public String? Product { get; set; }
    }


    static void Main(string[] args)
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
        
        HttpClient client = new HttpClient();
        
        string tokenJson = File.ReadAllText("token.json");
        JObject tokenObject = JObject.Parse(tokenJson);
        string token = (string) tokenObject["token"];
        Credentials.LoadCredentialsFromJson();

        //WebDriver webDriver = new WebDriver();
        //webDriver.Login();

        client.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
        client.Timeout = TimeSpan.FromSeconds(15);

        Credentials.CheckCredentials();
        Task tokenSignIn = TokenSignIn();
        GetItemId();

        async Task TokenSignIn()
        {
            string authPayload = JsonConvert.SerializeObject(new
            {
                auth_type = "cookie",
                email = Credentials.email,
                password = Credentials.password,
                device_id = "pc"
            });

            HttpContent authContent = new StringContent(authPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://api.warframe.market/v1/auth/signin", authContent);


            if (response.IsSuccessStatusCode)
            {
                Log("Successfully logged in to WFMarket!");
            }
            else
            {
                Log("Something went wrong with WFMarket API authentication");
            }
        }

        async Task GetItemId()
        {
            try
            {
                string syndicatesJson = File.ReadAllText("Syndicates.json");

                // ReadAllText doesn't always throw an exception by default, so this redundancy is necessary.
                if (syndicatesJson != null)
                {
                    Syndicates syndicates = JsonConvert.DeserializeObject<Syndicates>(syndicatesJson);
                    var perrinArray = syndicates.ThePerrinSeqeunce.augmentMods;
                    HttpResponseMessage getItemData = await client.GetAsync($"https://api.warframe.market/v1/items/{perrinArray[0].ModName}");
                    string json = await getItemData.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(json);

                    string itemId = (string)jsonObject["payload"]["item"]["id"];
                    Log(itemId);

                }
                else
                {
                    Log("Failed to read Augments Json");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        Log("Reached end");
        Console.ReadLine();
    }
}