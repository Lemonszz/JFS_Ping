using Discore;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    internal class CommandSetRank : Command
    {
        public CommandSetRank(string name, Rank requiredRank) : base(name, requiredRank)
        {
        }

        public override string GetHelpText()
        {
            return "!setrank [user] [rank]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 3)
            {
                SendHelpMessage(channel);
                return;
            }

            DiscordUser dUser = DiscordUtil.DiscordUserFromName(shard, commandParams[1]);
            if (dUser == null)
            {
                DiscordUtil.SendMessage(channel, "Unable to find user: " + commandParams[1]);
                return;
            }

            Rank rank = Rank.ANYONE;
            try
            {
                String rankString = commandParams[2];
                int rankInt = Convert.ToInt32(rankString);

                rank = (Rank)rankInt;
            }
            catch (Exception)
            {
                DiscordUtil.SendMessage(channel, "Invalid rank!");
                return;
            }

            User user = JfsPing.UserManager.UserFromDiscordUser(dUser);
            JfsPing.UserManager.SetUserRank(user, rank);
            DiscordUtil.SendMessage(channel, "Set rank for " + commandParams[1] + " to " + rank);
        }
    }
}