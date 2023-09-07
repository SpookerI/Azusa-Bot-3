using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Azusa.bot_3.Core.Managers
{
    public class PrefixManager
    {
        static string prefixList = "Config/customPrefix.txt";

        public static string getPrefix(ulong guildID)
        {
            string customPrefix = null;
            if(!File.Exists(prefixList))
                File.Create(prefixList);
            while(true)
            {
                string[] lines;
                try
                {
                    lines = File.ReadAllLines(prefixList);
                }
                catch(IOException)
                {
                    return ConfigManager.Config.Prefix;
                }
                try
                {
                    for(int i = 0; ; i++)
                    {
                        if (lines[i] == guildID.ToString())
                        {
                            customPrefix = lines[i+1];
                            return customPrefix;
                        }
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    return ConfigManager.Config.Prefix;
                }
            }
        }
        public static Task setPrefix(ulong guildID, string prefix)
        {
            bool customPrefixFound = false;
            while(!customPrefixFound)
            {
                string[] lines = File.ReadAllLines(prefixList);
                try
                {
                    for (int i = 0; ; i++)
                    {
                        if (lines[i] == guildID.ToString())
                        {
                            lines[i+1] = prefix;
                            File.WriteAllLines(prefixList, lines);
                            customPrefixFound = true;
                            break;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    List<string> newLines = new List<string>(lines);
                    newLines.Add(guildID.ToString());
                    newLines.Add(prefix);
                    newLines.ToArray();
                    File.WriteAllLines(prefixList, newLines);
                    customPrefixFound = true;
                }
            }
            return Task.CompletedTask;
        }
        public static Task resetPrefix(ulong guildID)
        {
            bool customPrefixFound = false;
            while(!customPrefixFound)
            {
                string[] lines = File.ReadAllLines(prefixList);
                try
                {
                    for (int i = 0; ; i++)
                    {
                        if (lines[i] == guildID.ToString())
                        {
                            List<string> newLines = new List<string>(lines);
                            newLines.RemoveAt(i);
                            newLines.RemoveAt(i);
                            newLines.ToArray();
                            File.WriteAllLines(prefixList, newLines);
                            customPrefixFound = true;
                            break;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}