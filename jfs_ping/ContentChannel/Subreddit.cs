using Newtonsoft.Json.Linq;
using RedditNet.Things;
using System;
using System.Collections.Generic;
using System.Text;

namespace jfs_ping
{
    public class Subreddit
    {
        public String LastLink
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public Subreddit(String name)
        {
            Name = name;   
        }

        public JObject Save()
        {
            JObject n = new JObject();
            n["subreddit"] = Name;

            return n;
        }

    }
}
