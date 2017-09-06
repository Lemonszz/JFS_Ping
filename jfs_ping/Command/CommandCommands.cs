using Discore;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    internal class CommandCommands : Command
    {
        private Commands commands;

        public CommandCommands(string name, Rank requiredRank, Commands commands) : base(name, requiredRank)
        {
            this.commands = commands;
        }

        public override string GetHelpText()
        {
            return "!commands";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            String outstring = "\n";
            foreach (Command c in commands.commandList)
            {
                outstring += "`" + c.Name + " | " + c.GetHelpText() + " | " + c.RequiredRank + "` \n";
            }
            DiscordUtil.SendMessage(channel, outstring);
        }
    }
}