using Discore;
using System;
using System.Collections.Generic;

namespace jfs_ping
{
    public class Commands
    {
        public List<Command> commandList;

        public Commands(YoutubeManager yt, TwitchManager tw, RedditManager rd)
        {
            commandList = new List<Command>();
            JfsPing.CommandMessageHandler += OnCommandMessage;

            commandList.Add(new CommandResponse("test", Rank.ADMIN, "AHHHHHH"));
            commandList.Add(new CommandReloadUsers("reloadusers", Rank.ANYONE));
            commandList.Add(new CommandSetRank("SetRank", Rank.ADMIN));
            commandList.Add(new CommandAddYoutubeChannel("AddYoutube", Rank.ADMIN, yt));
            commandList.Add(new CommandDebugForceChannel("ForceYoutube", Rank.ADMIN, yt));
            commandList.Add(new CommandHelp("Help", Rank.ANYONE, this));
            commandList.Add(new CommandCommands("Commands", Rank.ADMIN, this));
            commandList.Add(new CommandAddTwitch("AddTwitch", Rank.ADMIN, tw));
            commandList.Add(new CommandIsLive("IsLive", Rank.ANYONE, tw));
            commandList.Add(new CommandAddReddit("AddReddit", Rank.ADMIN, rd));
        }

        private void OnCommandMessage(object sender, CommandEventArgs e)
        {
            DiscordMessage message = e.Message;
            String[] splitMessage = message.Content.Split(' ');

            String commandName = splitMessage[0].Substring(1, splitMessage[0].Length - 1).ToLower();
            foreach (Command command in commandList)
            {
                if (command.Name.ToLower().Equals(commandName))
                {
                    if (HasRequiredRank(command, message.Author))
                    {
                        command.RunCommand(e.Shard, e.TextChannel, splitMessage);
                    }
                    else
                    {
                        DiscordUtil.SendMessage(e.TextChannel, "You do not have permission to run that command!");
                    }
                }
            }
        }

        public Boolean HasRequiredRank(Command command, DiscordUser duser)
        {
            User user = JfsPing.UserManager.UserFromDiscordUser(duser);
            return user.Rank >= (int)command.RequiredRank;
        }
    }
}