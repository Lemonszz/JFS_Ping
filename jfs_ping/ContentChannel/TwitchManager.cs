using Discore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace jfs_ping
{
    public class TwitchManager : IChannelManager
    {
        private List<TwitchChannel> twitchChannels;

        public TwitchManager()
        {
            twitchChannels = new List<TwitchChannel>();
            LoadChannels();
        }

        public void AddChannel(String name)
        {
            AddChannel(new TwitchChannel(name));
        }

        public void AddChannel(TwitchChannel channel)
        {
            twitchChannels.Add(channel);

            SaveChannels();
        }

        public TwitchChannel GetChannelFromName(string v)
        {
            foreach (TwitchChannel t in twitchChannels)
            {
                if (t.ChannelName.ToLower().Equals(v.ToLower()))
                {
                    return t;
                }
            }
            return null;
        }

        public void CheckChannels(ITextChannel channel)
        {
            foreach (TwitchChannel t in twitchChannels)
            {
                t.Check(channel);
            }
        }

        public void LoadChannels()
        {
            twitchChannels.Clear();
            String[] channelsString = File.ReadAllLines("channels_twitch.json");
            String channelStringConcat = "";
            foreach (String s in channelsString)
            {
                channelStringConcat += s;
            }

            JArray channelArray = JArray.Parse(channelStringConcat);
            for (int i = 0; i < channelArray.Count; i++)
            {
                JObject userJO = (JObject)channelArray[i];
                AddChannel((String)userJO["name"]);
            }
        }

        public void SaveChannels()
        {
            JArray channelArray = new JArray();
            foreach (TwitchChannel c in twitchChannels)
            {
                channelArray.Add(c.Save());
            }
            File.WriteAllText("channels_twitch.json", channelArray.ToString());
        }

        public bool IsEnabled()
        {
            return JfsPing.Settings.Twitch_Enabled;
        }
    }
}