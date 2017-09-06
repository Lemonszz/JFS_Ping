using System;
using System.Collections.Generic;
using System.Text;
using Discore;
using System.IO;
using Newtonsoft.Json.Linq;
using RedditNet;
using RedditNet.Requests;
using RedditNet.Things;
using System.Linq;

namespace jfs_ping
{
    public class RedditManager : IChannelManager
    {
        private List<Subreddit> reddits;
        RedditApi api;

        public RedditManager()
        {
            reddits = new List<Subreddit>();
            api = new RedditApi();
            LoadChannels();
        }

        public void AddChannel(Subreddit reddit)
        {
            reddits.Add(reddit);
            SaveChannels();
        }

        public void AddChannel(String reddit)
        {
            Subreddit sr = new Subreddit(reddit);
            AddChannel(sr);
        }

        public void CheckChannels(ITextChannel channel)
        {
            try
            {
                foreach (Subreddit sr in reddits)
                {
                    var sub = api.GetSubredditAsync(sr.Name).Result;
                    var links = sub.GetNewLinksAsync(new ListingRequest { Limit = 1 }).Result;
                    foreach (Link link in links.OfType<Link>())
                    {
                        Console.WriteLine("__" + link.Title + " " + link.Url + " " + link.Permalink);

                        if (sr.LastLink != null)
                        {
                            Console.WriteLine("__" + sr.LastLink);

                        }
                        if (sr.LastLink == null)
                        {
                            sr.LastLink = link.Url;
                        }
                        else if (!link.Url.ToLower().Equals(sr.LastLink.ToLower()))
                        {

                            DiscordUtil.SendMessage(channel, "New post in /r/" + sr.Name + "! https://www.reddit.com" + link.Permalink);
                            sr.LastLink = link.Url;
                        }
                    }

                }
            }
            catch(Exception)
            {

            }

        }

        public void LoadChannels()
        {
            reddits.Clear();
            String[] channelsString = File.ReadAllLines("channels_reddit.json");
            String channelStringConcat = "";
            foreach (String s in channelsString)
            {
                channelStringConcat += s;
            }

            JArray channelArray = JArray.Parse(channelStringConcat);
            for (int i = 0; i < channelArray.Count; i++)
            {
                JObject userJO = (JObject)channelArray[i];
                String redditName = (String)userJO["subreddit"];
                AddChannel(redditName);
            }
        }

        public void SaveChannels()
        {
            JArray channelArray = new JArray();
            foreach (Subreddit c in reddits)
            {
                channelArray.Add(c.Save());
            }
            File.WriteAllText("channels_reddit.json", channelArray.ToString());
        }

        public bool IsEnabled()
        {
            return JfsPing.Settings.Reddit_Enabled;
        }
    }
}
