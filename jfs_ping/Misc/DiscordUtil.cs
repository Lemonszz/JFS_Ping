using Discore;
using Discore.Http;
using Discore.WebSocket;
using System;

namespace jfs_ping
{
    public class DiscordUtil
    {
        public static async void SendMessage(ITextChannel channel, String message)
        {
            try
            {
                await channel.CreateMessage("🍋 " + message);
            }
            catch (DiscordHttpApiException) { }
        }

        public static DiscordUser DiscordUserFromName(Shard shard, String name)
        {
            foreach (var k in shard.Cache.Users.Keys)
            {
                DiscordUser user = shard.Cache.Users[k];
                if (user.Username.ToLower().Equals(name.ToLower()))
                {
                    return user;
                }
            }

            return null;
        }
    }
}