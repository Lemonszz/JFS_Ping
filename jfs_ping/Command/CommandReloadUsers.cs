using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandReloadUsers : Command
    {
        public CommandReloadUsers(string name, Rank requiredRank) : base(name, requiredRank)
        {
        }

        public override string GetHelpText()
        {
            return "!reloadusers";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            JfsPing.UserManager.ReloadUsers();
            DiscordUtil.SendMessage(channel, "Reloaded Users!");
        }
    }
}