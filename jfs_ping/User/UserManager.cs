using Discore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace jfs_ping
{
    public class UserManager
    {
        private List<User> users;

        public UserManager()
        {
            users = new List<User>();
            ReloadUsers();
        }

        public void ReloadUsers()
        {
            users.Clear();
            String[] userString = File.ReadAllLines("users.json");
            String userStringConcat = "";
            foreach (String s in userString)
            {
                userStringConcat += s;
            }

            JArray userArray = JArray.Parse(userStringConcat);
            for (int i = 0; i < userArray.Count; i++)
            {
                JObject userJO = (JObject)userArray[i];
                users.Add(User.FromJson(userJO));
            }
        }

        public void SaveUsers()
        {
            JArray userArray = new JArray();
            foreach (User u in users)
            {
                userArray.Add(u.Save());
            }
            File.WriteAllText("users.json", userArray.ToString());
        }

        public User UserFromDiscordUser(DiscordUser user)
        {
            ulong searchID = user.Id.Id;
            User foundUser = UserFromID(searchID);
            if (foundUser == null)
            {
                return CreateUserFromDiscordUser(user);
            }
            else
            {
                return foundUser;
            }
        }

        public User UserFromID(ulong id)
        {
            foreach (User u in users)
            {
                if (u.ID == id)
                {
                    return u;
                }
            }
            return null;
        }

        public User CreateUserFromDiscordUser(DiscordUser user)
        {
            User newUser = User.FromDiscordUser(user);
            users.Add(newUser);
            SaveUsers();
            return newUser;
        }

        public void SetUserRank(User user, Rank rank)
        {
            user.Rank = (int)rank;
            SaveUsers();
            ReloadUsers();
        }
    }
}