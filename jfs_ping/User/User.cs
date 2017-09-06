using Discore;
using Newtonsoft.Json.Linq;
using System;

namespace jfs_ping
{
    public class User
    {
        public ulong ID
        {
            get;
            set;
        }

        public int Rank
        {
            get;
            set;
        }

        private User(ulong id, int rank)
        {
            ID = id;
            Rank = rank;
        }

        public static User FromJson(JObject jo)
        {
            ulong id = (ulong)jo.GetValue("id");
            int rank = (int)jo.GetValue("rank");

            return new User(id, rank);
        }

        public static User FromJSonString(String jsonString)
        {
            return FromJson(JObject.Parse(jsonString));
        }

        public static User FromDiscordUser(DiscordUser user)
        {
            return new User(user.Id.Id, 0);
        }

        public JObject Save()
        {
            JObject jo = new JObject();
            jo["id"] = ID;
            jo["rank"] = Rank;

            return jo;
        }
    }
}