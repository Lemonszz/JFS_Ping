using Discore;
using Discore.WebSocket;

namespace jfs_ping
{
    public class CommandEventArgs
    {
        public DiscordMessage Message
        {
            get;
            set;
        }

        public ITextChannel TextChannel
        {
            get;
            set;
        }

        public Shard Shard
        {
            get;
            set;
        }

        public CommandEventArgs(DiscordMessage message, ITextChannel textChannel, Shard shard)
        {
            Message = message;
            TextChannel = textChannel;
            Shard = shard;
        }
    }
}