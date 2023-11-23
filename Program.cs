using CommandLine;
using Newtonsoft.Json;
using System.Text;
using WFMarketTool;

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Controls;
using System.Runtime.CompilerServices;
using System.IO;
class Program
{
    static void Log(string message,
                    [CallerFilePath] string? file = null,
                    [CallerLineNumber] int line = 0)
    {
        Console.WriteLine("{0} ({1}): {2}", Path.GetFileName(file), line, message);
    }

    public class Config
    {

    }

    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('a', "addlisting", Required = false, HelpText = "Add a listing to your account.")]
        public String? Product { get; set; }
    }

    public class TokenModel
    {
        public string Token { get; set; }
    }

    static void Main(string[] args)
    {
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
        TokenModel token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

        Credentials.LoadCredentialsFromJson();

        //WebDriver webDriver = new WebDriver();
        //webDriver.Login();

        client.DefaultRequestHeaders.Add("Authorization", $"JWT {token.Token}");
        client.Timeout = TimeSpan.FromSeconds(15);

        CheckCredentials();
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
                Log("Should be logged in");

                HttpResponseMessage getCurrentOrdersResponse = await client.GetAsync("https://api.warframe.market/v1/profile/orders");
                string responseBody = await getCurrentOrdersResponse.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Log("Something went wrong with WFMarket API authentication");
            }
        }

        void GetItemId()
        {
            try
            {
                string augmentModsJson = File.ReadAllText("AugmentsName.json");

                // ReadAllText doesn't always throw an exception by default, so this redundancy is necessary.
                if (augmentModsJson != null)
                {

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



            //string authPayload = JsonConvert.SerializeObject(new
            //{
            //    auth_type = "cookie",
            //    email = Credentials.email,
            //    password = Credentials.password,
            //    device_id = "pc"
            //});
            //HttpContent authContent = new StringContent(authPayload, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await client.PostAsync($"https://api.warframe.market/v1/items/endless_lullaby", authContent);


            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine("Should be logged in");

            //    HttpResponseMessage getCurrentOrdersResponse = await client.GetAsync("https://api.warframe.market/v1/profile/orders");
            //    string responseBody = await getCurrentOrdersResponse.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseBody);
            //}
            //else
            //{
            //    Console.WriteLine("Something went wrong with WFMarket API authentication");
            //}
        }

        //Parser.Default.ParseArguments<Options>(args)
        //       .WithParsed<Options>(async o =>
        //       {
        //           if (o.Verbose)
        //           {
        //               Console.WriteLine("App is in Verbose mode!");
        //           }
        //           else
        //           {
        //               Console.WriteLine($"Current Arguments: -v {o.Verbose}");
        //           }
        //       });
        JsonTesting.ModifyJson();

        Log("Reached end");
        Console.ReadLine();
    }

    public static void CheckCredentials()
    {
        if (File.Exists("Credentials.json"))
        {
            Console.WriteLine("Credentials.json found!");
        }
        else
        {
            YesNoQuestion yesNoQuestion = new YesNoQuestion("WFMarket credentials not detected. Would you like to enter them?");
            YesNoAnswer answer = yesNoQuestion.ReadAnswer();
            if (answer == YesNoAnswer.No)
            {
                Environment.Exit(0);
            }

            Console.Clear();
            CustomConsole.WriteEmphasized("Enter your password and then press Enter\n");
            string pass = Console.ReadLine();

            CustomConsole.WriteEmphasized("Enter your email and then press Enter\n");
            string email = Console.ReadLine();


            string credentialsString = JsonConvert.SerializeObject(new
            {
                Password = pass,
                Email = email
            });

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Credentials.json"), credentialsString);
        }
    }
}