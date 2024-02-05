namespace MacrixPracticalTask_Client.API
{
    public class Utils
    {
        public static readonly string ConfigPath = "config.json";

        public static Config.Config GetConfig()
        {
            var text = File.ReadAllText(ConfigPath);
            var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Config.Config>(text);
            return dictionary!;
        }
    }
}
