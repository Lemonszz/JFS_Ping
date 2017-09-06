using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandAddTwitch : Command
    {
        private TwitchManager manager;

        public CommandAddTwitch(string name, Rank requiredRank, TwitchManager manager) : base(name, requiredRank)
        {
            this.manager = manager;
        }

        public override string GetHelpText()
        {
            return "!AddTwitch [Channel Name]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
            }

            manager.AddChannel(commandParams[1]);
            DiscordUtil.SendMessage(channel, "Added Twitch channel: " + commandParams[1]);
        }
    }
}