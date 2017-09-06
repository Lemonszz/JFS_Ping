using System;

namespace jfs_ping
{
    public class YoutubeVideo : IComparable<YoutubeVideo>
    {
        public String URL
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }

        public YoutubeVideo(String url, String title, String author)
        {
            URL = url;
            Title = title;
            Author = author;
        }

        public int CompareTo(YoutubeVideo other)
        {
            return other.URL == URL ? 0 : 1;
        }
    }
}