using System;
using System.Resources;
using System.Globalization;
using System.IO;

namespace Azusa.bot_3.Language
{
    public class StringManager
    {
        static string serverList = "Config/servers.txt";
        private static global::System.Resources.ResourceManager resourceMan;
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Azusa.bot_3.Language.strings", typeof(strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        public string getString(ulong guildID, string stringName)
        {
            var russianCulture = CultureInfo.GetCultureInfo("ru-RU");
            var englishCulture = CultureInfo.GetCultureInfo("en-US");
            string language = getServerLanguage(guildID);
            if (language == "english")
            {
                return ResourceManager.GetString(stringName, englishCulture);
            }
            else if (language == "russian")
            {
                return ResourceManager.GetString(stringName, russianCulture);
            }
            return null;
            
        }
        public string getServerLanguage(ulong guildID)
        {
            string serverLanguage = null;
            bool serverIDFound = false;
            try
            {
                if(!File.Exists(serverList))
                {
                    throw new IndexOutOfRangeException();
                }
                string[] lines = File.ReadAllLines(serverList);
                while(!serverIDFound) 
                {
                    for(int i = 0; ; i++)
                    {
                        if (lines[i] == guildID.ToString())
                        {
                            serverLanguage = lines[i+1];
                            serverIDFound = true;
                            return serverLanguage;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                string currentCulture = CultureInfo.CurrentCulture.ToString();
                switch (currentCulture)
                {
                    case "ru-RU":
                        return "russian";
                    case "en-US":
                        return "english";
                }
            }
            return null;
        }
    }
}