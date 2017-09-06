using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace jfs_ping
{
    public class Settings
    {
        public String Bot_Token
        {
            get;
            set;
        }

        public int Check_Time
        {
            get;
            set;
        }

        public ulong Channel_ID
        {
            get;
            set;
        }

        public String Twitch_Token
        {
            get;
            set;
        }

        public bool Twitch_Enabled
        {
            get;
            set;
        }

        public bool Youtube_Enabled
        {
            get;
            set;
        }

        public bool Reddit_Enabled
        {
            get;
            set;
        }

        public Settings()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            if(!File.Exists("settings.json"))
            {
                JObject newSettings = new JObject();
                newSettings.Add("bot_token", "");
                newSettings.Add("check_period_seconds", "");
                newSettings.Add("text_channel", "");
                newSettings.Add("twitch_client", "");
                newSettings.Add("enable_twitch", true);
                newSettings.Add("enable_youtube", true);
                newSettings.Add("enable_reddit", true);

                String output = JsonConvert.SerializeObject(newSettings);
                File.WriteAllLines("settings.json", new String[] { output });

                JArray emptyChannel = new JArray();
                String emptyChannelString = JsonConvert.SerializeObject(emptyChannel);

                if(!File.Exists("channels_reddit.json"))
                    File.WriteAllLines("channels_reddit.json", new String[] { emptyChannelString });

                if (!File.Exists("channels_twitch.json"))
                    File.WriteAllLines("channels_twitch.json", new String[] { emptyChannelString });

                if (!File.Exists("channels_yt.json"))
                    File.WriteAllLines("channels_yt.json", new String[] { emptyChannelString });

                Console.WriteLine("Generated settings files. Please enter your settings and restart the program.");
                Console.WriteLine("The program will now exit.");
                Environment.Exit(1);

                return;
            }

            String[] lines = File.ReadAllLines("settings.json");
            String lines_concat = "";
            foreach (string s in lines)
            {
                lines_concat += s;
            }

            JObject jo = JObject.Parse(lines_concat);

            Bot_Token = (String)jo["bot_token"];
            Check_Time = (int)jo["check_period_seconds"];
            Channel_ID = (ulong)jo["text_channel"];
            Twitch_Token = (String)jo["twitch_client"];
            Reddit_Enabled = (bool)jo["enable_reddit"];
            Twitch_Enabled = (bool)jo["enable_twitch"];
            Youtube_Enabled = (bool)jo["enable_youtube"];

        }
    }
}