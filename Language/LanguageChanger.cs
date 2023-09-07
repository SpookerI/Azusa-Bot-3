using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Azusa.bot_3.Language
{
    public class LanguageChanger
    {
        static string serverList = "Config/servers.txt";
        public static string currentLanguage = "russian";
        public static Task ChangeLanguage(string language, ulong guildID)
        {
            bool serverIDFound = false;
            if (language == "english" || language == "English" || language == "en")
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                currentLanguage = "english";
                while(!serverIDFound)
                {
                    string[] lines = File.ReadAllLines(serverList);
                    try
                    {
                        for (int i = 0; ; i++)
                        {
                            if (lines[i] == guildID.ToString())
                            {
                                lines[i + 1] = "english";
                                File.WriteAllLines(serverList, lines);
                                serverIDFound = true;
                                break;
                            }
                        }
                    }
                    catch(IndexOutOfRangeException)
                    {
                        List<string> newLines = new List<string>(lines);
                        newLines.Add(guildID.ToString());
                        newLines.Add("english");
                        newLines.ToArray();
                        File.WriteAllLines(serverList, newLines);
                        serverIDFound = true;
                    }
                }
            }
            else if(language == "russian" || language == "Russian" || language == "ru")
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                currentLanguage = "russian";
                while (!serverIDFound)
                {
                    string[] lines = File.ReadAllLines(serverList);
                    try
                    {
                        for (int i = 0; ; i++)
                        {
                            if (lines[i] == guildID.ToString())
                            {
                                lines[i + 1] = "russian";
                                File.WriteAllLines(serverList, lines);
                                serverIDFound = true;
                                break;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        List<string> newLines = new List<string>(lines);
                        newLines.Add(guildID.ToString());
                        newLines.Add("russian");
                        newLines.ToArray();
                        File.WriteAllLines(serverList, newLines);
                        serverIDFound = true;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}