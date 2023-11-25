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
            }
            else
            {
                Log("Something went wrong with WFMarket API authentication");
            }
        }

        public async static Task GetItemId()
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
    }
}
