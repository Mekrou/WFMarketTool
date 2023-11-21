using CommandLine;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using WFMarketTool;


class Program
{

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
        HttpClient client = new HttpClient();
        string tokenJson = File.ReadAllText("token.json");
        TokenModel token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

        WebDriver webDriver = new WebDriver();
        webDriver.Login();

        Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(async o =>
               {
                   if (o.Verbose)
                   {
                       Console.WriteLine("App is in Verbose mode!");
                   }
                   else
                   {
                       Console.WriteLine($"Current Arguments: -v {o.Verbose}");
                   }


                   //if (o.Product != null)
                   if (true)
                   {
                       Console.WriteLine("Do something to WF market here!");

                       string authPayload = JsonConvert.SerializeObject(new
                       {
                           auth_type = "cookie",
                           email = "email",
                           password = "password",
                           device_id = "pc"
                       });

                       client.DefaultRequestHeaders.Add("Authorization", $"JWT {token.Token}");
                       client.Timeout = TimeSpan.FromSeconds(15);


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
                           Console.WriteLine(response.StatusCode);
                       }
                   }
               });

        Console.ReadLine();
    }
}