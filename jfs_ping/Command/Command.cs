using Discore;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    public abstract class Command
    {
        public String Name
        {
            get;
            set;
        }

        public Rank RequiredRank
        {
            get;
            set;
        }

        public Command(String name, Rank requiredRank)
        {
            Name = name;
            RequiredRank = requiredRank;
        }

        public void SendHelpMessage(ITextChannel channel)
        {
            DiscordUtil.SendMessage(channel, "`" + GetHelpText() + "`");
        }

        public abstract String GetHelpText();

        public abstract void RunCommand(Shard shard, ITextChannel channel, string[] commandParams);  //0 is command name
    }
}