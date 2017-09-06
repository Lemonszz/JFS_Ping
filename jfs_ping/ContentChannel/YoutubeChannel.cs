using Discore;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace jfs_ping
{
    public class YoutubeChannel
    {
        public String YoutubeID
        {
            get;
            set;
        }

        public YoutubeVideo LastVideo
        {
            get;
            set;
        }

        public bool HasChanged
        {
            get;
            set;
        }

        public YoutubeChannel(String youtubeID)
        {
            YoutubeID = youtubeID;
            GetLatestVideo();
            HasChanged = false;
        }

        public void GetLatestVideo()
        {
            try
            {
                if (LastVideo != null)
                {
                    Console.WriteLine("- Last video on record for " + YoutubeID + " is " + LastVideo.Title);
                }

                HttpClient client = new HttpClient();
                Task<String> get = client.GetStringAsync("https://www.youtube.com/feeds/videos.xml?channel_id=" + YoutubeID);
                if (get == null)
                {
                    return;
                }
                String fetched = get.Result;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(fetched);

                String author = xDoc.DocumentElement["author"]["name"].InnerText;
                String title = xDoc.DocumentElement["entry"]["title"].InnerText;
                String url = "https://www.youtube.com/watch?v=" + xDoc.DocumentElement["entry"]["yt:videoId"].InnerText;

                YoutubeVideo newVideo = new YoutubeVideo(url, title, author);
                if (LastVideo != null)
                {
                    Console.WriteLine("- Checking the video " + newVideo.Title + " against " + LastVideo.Title);
                }
                if (LastVideo == null)
                {
                    LastVideo = newVideo;
                }
                else if (newVideo.CompareTo(LastVideo) > 0)
                {
                    Console.WriteLine("- this was a new video");
                    LastVideo = newVideo;
                    HasChanged = true;
                }
                else
                {
                    Console.WriteLine("- No change in video");
                    HasChanged = false;
                    LastVideo = newVideo;
                }
            }
            catch (Exception)
            {
            }
        }

        public void TellVideo(ITextChannel channel)
        {
            DiscordUtil.SendMessage(channel, LastVideo.Author + " uploaded a video! " + LastVideo.Title + " " + LastVideo.URL);
        }

        public static YoutubeChannel FromJson(JObject json)
        {
            String channelID = (string)json["id"];
            return new YoutubeChannel(channelID);
        }

        public JObject Save()
        {
            JObject jo = new JObject();
            jo["id"] = YoutubeID;

            return jo;
        }
    }
}