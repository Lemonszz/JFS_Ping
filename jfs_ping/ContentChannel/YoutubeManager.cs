using Discore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace jfs_ping
{
    public class YoutubeManager : IChannelManager
    {
        private List<YoutubeChannel> channels;

        public YoutubeManager()
        {
            channels = new List<YoutubeChannel>();
            LoadChannels();
        }

        public void CheckChannels(ITextChannel channel)
        {
            foreach (YoutubeChannel c in channels)
            {
                c.GetLatestVideo();
            }

            foreach (YoutubeChannel c in channels)
            {
                if (c.HasChanged)
                {
                    c.TellVideo(channel);
                }
            }
        }

        public void LoadChannels()
        {
            channels.Clear();
            String[] channelsString = File.ReadAllLines("channels_yt.json");
            String channelStringConcat = "";
            foreach (String s in channelsString)
            {
                channelStringConcat += s;
            }

            JArray channelArray = JArray.Parse(channelStringConcat);
            for (int i = 0; i < channelArray.Count; i++)
            {
                JObject userJO = (JObject)channelArray[i];
                channels.Add(YoutubeChannel.FromJson(userJO));
            }
        }

        public void SaveChannels()
        {
            JArray channelArray = new JArray();
            foreach (YoutubeChannel c in channels)
            {
                channelArray.Add(c.Save());
            }
            File.WriteAllText("channels_yt.json", channelArray.ToString());
        }

        public void AddChannel(String youtubeID)
        {
            YoutubeChannel channel = new YoutubeChannel(youtubeID);
            AddChannel(channel);
        }

        public void AddChannel(YoutubeChannel channel)
        {
            channels.Add(channel);
            SaveChannels();
        }

        public YoutubeChannel GetChannelByChannelID(String channelID)
        {
            foreach (YoutubeChannel c in channels)
            {
                if (c.YoutubeID.Equals(channelID))
                {
                    return c;
                }
            }

            return null;
        }

        public bool IsEnabled()
        {
            return JfsPing.Settings.Youtube_Enabled;
        }

    }
}