using System;

namespace jfs_ping
{
    public class StreamInfo
    {
        public String Name
        {
            get;
            set;
        }

        public String Game
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public StreamInfo(String name, String game, String title)
        {
            Name = name;
            Game = game;
            Title = title;
        }
    }
}