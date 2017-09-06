using Discore;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    public class CommandResponse : Command
    {
        public String Response
        {
            get;
            set;
        }

        public CommandResponse(string name, Rank requiredRank, String response) : base(name, requiredRank)
        {
            Response = response;
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            DiscordUtil.SendMessage(channel, Response);
        }

        public override string GetHelpText()
        {
            return "!test";
        }
    }
}