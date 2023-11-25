using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using static Program;

namespace WFMarketTool
{

    /// <summary>
    /// Manages initializing HttpClient to have correct authentication headers and any async tasks of retrieving data from the WFMarket api.
    /// </summary>
    internal class WFMarketTask
    {
        static HttpClient httpClient;
        private readonly static string apiBaseUrl = "https://api.warframe.market/v1";

        /// <summary>
        /// Initializes HttpClient to have correct authorization required by WFMarket api.
        /// </summary>
        static WFMarketTask()
        {
            httpClient = new HttpClient();
            string token = Credentials.GetApiAuthToken();

            Credentials.LoadCredentialsFromJson();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
            httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public static async Task TokenSignIn()
        {
            string authPayload = JsonConvert.SerializeObject(new
            {
                auth_type = "cookie",
                email = Credentials.email,
                password = Credentials.password,
                device_id = "pc"
            });

            HttpContent authContent = new StringContent(authPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://api.warframe.market/v1/auth/signin", authContent);


            if (response.IsSuccessStatusCode)
            {
                Log("Successfully logged in to WFMarket!");
                Log("Response");
                Log(response.ToString());
            }
            else
            {
                Log("Something went wrong with WFMarket API authentication");
            }
        }

        public async static Task GetAugmentModId()
        {
            try
            {
                string syndicatesJson = File.ReadAllText("Syndicates.json");

                // ReadAllText doesn't always throw an exception by default, so this redundancy is necessary.
                if (syndicatesJson != null)
                {
                    Syndicates syndicates = JsonConvert.DeserializeObject<Syndicates>(syndicatesJson);
                    var perrinArray = syndicates.ThePerrinSeqeunce.augmentMods;
                    HttpResponseMessage getItemData = await httpClient.GetAsync($"https://api.warframe.market/v1/items/{perrinArray[0].ModName}");
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

        public async static Task<string> GetItemId(string item_name)
        {
            HttpResponseMessage getItemData = await httpClient.GetAsync($"https://api.warframe.market/v1/items/{item_name}");
            string json = await getItemData.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(json);
            string itemId = (string)jsonObject["payload"]["item"]["id"];
            return itemId;
        }

        public async static Task CreateOrder(string itemId)
        {
            Object payload = new
            {
                item = "59e203ce115f1d887cfd7ac6",
                order_type = "sell",
                platinum = 12,
                quantity = 5,
                visible = true,
                rank = 3,
                subtype = "flawless"
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);


            Log(jsonPayload);

            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


            string token = Credentials.GetApiAuthToken();
            

            HttpResponseMessage response = await httpClient.PostAsync(apiBaseUrl + "/profile/orders", content);

            Console.WriteLine("Request:");
            Console.WriteLine(await content.ReadAsStringAsync());

            

            Console.WriteLine("Response:");
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Order successfully created.");
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
