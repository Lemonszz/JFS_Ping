using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandHelp : Command
    {
        private Commands commands;

        public CommandHelp(string name, Rank requiredRank, Commands commands) : base(name, requiredRank)
        {
            this.commands = commands;
        }

        public override string GetHelpText()
        {
            return "!help [command]";
        }

        public override void RunCommand(Shard shard, ITextChannel channel, string[] commandParams)
        {
            if (commandParams.Length < 2)
            {
                SendHelpMessage(channel);
                return;
            }

            foreach (Command c in commands.commandList)
            {
                if (c != this && commandParams[1].ToLower().Equals(c.Name.ToLower()))
                {
                    c.SendHelpMessage(channel);
                }
            }
        }
    }
}