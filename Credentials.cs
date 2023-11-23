using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WFMarketTool
{
    public class CredentialModel
    {
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    public class Credentials
    {
        public static string? password { get; set; }
        public static string? email { get; set; }

        public static void LoadCredentialsFromJson()
        {
            string jsonString = File.ReadAllText("Credentials.json");
            CredentialModel credentials = JsonConvert.DeserializeObject<CredentialModel>(jsonString);
            password = credentials.Password;
            email = credentials.Email;
        }
    }
}
