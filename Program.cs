using CommandLine;
using Newtonsoft.Json;
using System.Text;
using WFMarketTool;


using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Controls;
using System.ComponentModel.DataAnnotations;

class Program
{
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

        //WebDriver webDriver = new WebDriver();
        //webDriver.Login();

        client.DefaultRequestHeaders.Add("Authorization", $"JWT {token.Token}");
        client.Timeout = TimeSpan.FromSeconds(15);

        if (File.Exists("Credentials.json"))
        {
            Console.WriteLine("Credentials.json found!");
        } else
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


            async Task TokenSignIn() {
            string authPayload = JsonConvert.SerializeObject(new
            {
                auth_type = "cookie",
                email = "email",
                password = "password",
                device_id = "pc"
            });
            HttpContent authContent = new StringContent(authPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://api.warframe.market/v1/auth/signin", authContent);
            

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Should be logged in");

                HttpResponseMessage getCurrentOrdersResponse = await client.GetAsync("https://api.warframe.market/v1/profile/orders");
                string responseBody = await getCurrentOrdersResponse.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Something went wrong with WFMarket API authentication");
            }
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

        Console.WriteLine("Reached end");
        Console.ReadLine();
    }
}