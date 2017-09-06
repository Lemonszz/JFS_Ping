using Discore;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    public class CommandIsLive : Command
    {
        private TwitchManager manager;

        public CommandIsLive(string name, Rank requiredRank, TwitchManager manager) : base(name, requiredRank)
        {
            this.manager = manager;
        }

        public override string GetHelpText()
        {
            return "!IsLive [Twitch Channel]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
            }

            TwitchChannel tw = manager.GetChannelFromName(commandParams[1]);
            if (tw == null)
            {
                DiscordUtil.SendMessage(channel, "I don't know of that channel.");
            }
            else
            {
                String message = tw.IsLive ? tw.ChannelName + " is live now! http://www.twitch.tv/" + tw.ChannelName : tw.ChannelName + " is not live right now.";
                DiscordUtil.SendMessage(channel, message);
            }
        }
    }
}