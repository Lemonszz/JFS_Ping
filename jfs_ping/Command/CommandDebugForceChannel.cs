using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandDebugForceChannel : Command
    {
        private YoutubeManager manager;

        public CommandDebugForceChannel(string name, Rank requiredRank, YoutubeManager manager) : base(name, requiredRank)
        {
            this.manager = manager;
        }

        public override string GetHelpText()
        {
            return "!ForceYoutube [channel]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
            }
            YoutubeChannel yt = manager.GetChannelByChannelID(commandParams[1]);
            if (yt != null)
            {
                yt.GetLatestVideo();
                yt.TellVideo(channel);
            }
            else
            {
                DiscordUtil.SendMessage(channel, "No channel found with ID " + commandParams[1]);
            }
        }
    }
}