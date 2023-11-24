using Newtonsoft.Json.Linq;

namespace WFMarketTool
{

    /// <summary>
    /// Manages initializing HttpClient to have correct authentication headers and any async tasks of retrieving data from the WFMarket api.
    /// </summary>
    internal class WFMarketTasks
    {
        HttpClient httpClient;

        /// <summary>
        /// Initializes HttpClient to have correct authorization required by WFMarket api.
        /// </summary>
        public WFMarketTasks()
        {
            httpClient = new HttpClient();
            string token = Credentials.GetApiAuthToken();

            Credentials.LoadCredentialsFromJson();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"JWT {token}");
            httpClient.Timeout = TimeSpan.FromSeconds(15);
        }
    }
}
