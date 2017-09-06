using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandAddYoutubeChannel : Command
    {
        private YoutubeManager manager;

        public CommandAddYoutubeChannel(string name, Rank requiredRank, YoutubeManager manager) : base(name, requiredRank)
        {
            this.manager = manager;
        }

        public override string GetHelpText()
        {
            return "!AddYotube [channelID]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
            }

            manager.AddChannel(commandParams[1]);
            DiscordUtil.SendMessage(channel, "Added youtube channel: " + commandParams[1]);
        }
    }
}