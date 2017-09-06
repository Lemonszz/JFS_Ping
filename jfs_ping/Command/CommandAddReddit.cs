using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandAddReddit : Command
    {
        private RedditManager manager;

        public CommandAddReddit(string name, Rank requiredRank, RedditManager manager) : base(name, requiredRank)
        {
            this.manager = manager;
        }

        public override string GetHelpText()
        {
            return "!AddReddit [Subreddit]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
            }

            manager.AddChannel(commandParams[1]);
            DiscordUtil.SendMessage(channel, "Added subreddit: " + commandParams[1]);
        }
    }
}