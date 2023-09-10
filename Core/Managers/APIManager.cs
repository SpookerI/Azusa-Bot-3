using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Azusa.bot_3.Core.Managers
{
    public class APIManager
    {
        public static WebClient api = new WebClient();
        public static string replyPath = "API/";
        public static string sfwURL = "https://hmtai.hatsunia.cfd/sfw/";
        public static string nsfwURL = "https://hmtai.hatsunia.cfd/nsfw/";
        public static string apiV2 = "https://nekobot.xyz/api/image?type=";
        public static string gelbooruAPI = "https://gelbooru.com/index.php?page=dapi&s=post&q=index&tags=";

        public static string GetAPIURLSFW(string command)
        {
            string url = null;
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 1000);
            api.DownloadFile(sfwURL + command, replyPath + "reply" + randomNumber + ".json");
            var json = File.ReadAllText(replyPath + "reply" + randomNumber + ".json");
            var reply = JsonConvert.DeserializeObject<dynamic>(json);
            url = reply.url;
            File.Delete(replyPath + "reply" + randomNumber + ".json");
            return url;
        }
        public static string GetAPIURLNSFW(string command)
        {
            string url = null;
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 1000);
            api.DownloadFile(nsfwURL + command, replyPath + "reply" + randomNumber + ".json");
            var json = File.ReadAllText(replyPath + "reply" + randomNumber + ".json");
            var reply = JsonConvert.DeserializeObject<dynamic>(json);
            url = reply.url;
            File.Delete(replyPath + "reply" + randomNumber + ".json");
            return url;
        }
        public static string GetAPIV2(string command)
        {
            string url = null;
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 1000);
            api.DownloadFile(apiV2 + command, replyPath + "replyV2_" + randomNumber + ".json");
            var json = File.ReadAllText(replyPath + "replyV2_" + randomNumber + ".json");
            var reply = JsonConvert.DeserializeObject<dynamic>(json);
            var replyAPI = reply["message"];
            url = replyAPI;
            File.Delete(replyPath + "replyV2_" + randomNumber + ".json");
            return url;
        }
        public static string GetAPIGelbooru(string tags)
        { 
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            string url = null;
            string apiKey = ConfigManager.Config.gelbooruAPIKey;
            Random rnd = new Random();
            int randomNumber = rnd.Next(0, 1000);
            try
            {
                api.DownloadFile(gelbooruAPI + tags + $"&json=1&limit=1&pid=0{apiKey}", replyPath + "gelbooruAPI_" + randomNumber + ".json");
                var jsonCount = File.ReadAllText(replyPath + "gelbooruAPI_" + randomNumber + ".json");
                var checkCount = JsonConvert.DeserializeObject<dynamic>(jsonCount);
                int postCount = checkCount["@attributes"]["count"]; // Checks overall amount of posts by this tags.
                if(postCount == 0)
                {
                    if(main.debugmode)
                        Console.WriteLine("GelbooruAPI: Nothing found for this search: " + tags);
                    return "Not Found";
                }
                string pageID = rnd.Next(0, postCount).ToString(); // Random pageID
                api.DownloadFile(gelbooruAPI + tags + $"&json=1&limit=1&pid={pageID}{apiKey}", replyPath + "gelbooruAPI_" + randomNumber + ".json");
                var json = File.ReadAllText(replyPath + "gelbooruAPI_" + randomNumber + ".json");
                dynamic reply = JsonConvert.DeserializeObject(json);
                dynamic posts = reply["post"];
                foreach(var post in posts)
                {
                    url = post["file_url"].ToString(); // Gets the file URI
                }
                File.Delete(replyPath + "gelbooruAPI_" + randomNumber + ".json");
                return url;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Gelbooru API: An exception has occured: ", ex.ToString());
                Console.WriteLine(ex.StackTrace);
                return "ERR";
            }
        }
    }
}