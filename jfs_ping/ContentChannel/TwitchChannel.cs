using Discore;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace jfs_ping
{
    public class TwitchChannel
    {
        public String ChannelName
        {
            get;
            set;
        }

        public bool IsLive
        {
            get;
            set;
        }

        public bool HasChanged
        {
            get;
            set;
        }

        public bool HasCheckedBefore
        {
            get;
            set;
        }

        public TwitchChannel(String channelName)
        {
            ChannelName = channelName;
            IsLive = false;
            HasChanged = false;
            HasCheckedBefore = false;
        }

        private StreamInfo GetTwitchInfo()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Client-ID", JfsPing.Settings.Twitch_Token);
                Task<String> get = client.GetStringAsync("https://api.twitch.tv/kraken/streams/" + ChannelName);
                if (get == null)
                {
                    return null;
                }
                String fetched = get.Result;

                if (fetched.Length < 200)
                {
                    return null;
                }
                JObject streamJson = JObject.Parse(fetched);
                if (streamJson["stream"] == null)
                {
                    return null;
                }

                JObject streamInfoJson = (JObject)streamJson["stream"];

                String name = ChannelName;
                String game = (String)streamJson["game"];

                JObject channelInfo = (JObject)streamInfoJson["channel"];
                String title = (String)channelInfo["status"];

                StreamInfo info = new StreamInfo(name, game, title);
                return info;
            }
            catch (Exception) //TODO: better error
            {
                return null;
            }
        }

        public JObject Save()
        {
            JObject jo = new JObject();
            jo["name"] = ChannelName;

            return jo;
        }

        public void Check(ITextChannel channel)
        {
            bool wasLive = IsLive;
            bool liveNow = false;
            StreamInfo info = GetTwitchInfo();
            if (info == null)
            {
                liveNow = false;
            }
            else
            {
                liveNow = true;
            }

            if (IsLive != liveNow)
            {
                HasChanged = true;
                IsLive = liveNow;
            }

            if (HasCheckedBefore && IsLive && !wasLive)
            {
                DiscordUtil.SendMessage(channel, info.Name + " has just gone live on Twitch! \n" + info.Game + ": " + info.Title + "\n http://twitch.tv/" + ChannelName);
            }
            HasCheckedBefore = true;
        }
    }
}