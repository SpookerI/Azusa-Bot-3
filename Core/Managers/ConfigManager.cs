using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Azusa.bot_3.Core.Managers
{
    public static class ConfigManager
    {
        private static string ConfigFolder = "Config";
        private static string ConfigFile = "config.json";
        private static string ConfigPath = ConfigFolder + "/" + ConfigFile;
        public static BotConfig Config { get; private set; }

        static ConfigManager()
        {
            if(!Directory.Exists(ConfigFolder))
                Directory.CreateDirectory(ConfigFolder);
            if(!File.Exists(ConfigPath))
            {
                Config = new BotConfig();
                var json = JsonConvert.SerializeObject(Config, Formatting.Indented); // Text formating.
                File.WriteAllText(ConfigPath, json);
            }
            else
            {
                var json = File.ReadAllText(ConfigPath);
                Config = JsonConvert.DeserializeObject<BotConfig>(json); // Text deformating.
            }
        }
        public static Task Deserialize()
        {
            var json = File.ReadAllText(ConfigPath);
            Config = JsonConvert.DeserializeObject<BotConfig>(json); // Loads updated config file.
            return Task.CompletedTask;
        }

        public struct BotConfig
        {
            [JsonProperty("token")]
            public string Token { get; private set; }
            [JsonProperty("prefix")]
            public string Prefix { get; private set; }
            [JsonProperty("chatGPTKey")]
            public string chatGPTKey { get; private set; }
            [JsonProperty("gelbooruAPIKEYURL")]
            public string gelbooruAPIKey { get; private set; }
        }
    }
}