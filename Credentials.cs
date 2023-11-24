using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools;
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

        public static void CheckCredentials()
        {
            if (File.Exists("Credentials.json"))
            {
                Program.Log("Found stored credentials. No need to enter them.");
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

        // TODO: Add functionality if token.json is missing!!!
        public static string GetApiAuthToken()
        {
            string tokenJson = File.ReadAllText("token.json");
            JObject tokenObject = JObject.Parse(tokenJson);
            string token = (string)tokenObject["token"];
            return token;
        }
    }
}
