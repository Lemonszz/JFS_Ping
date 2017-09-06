using Discore;
using System;
using System.Collections.Generic;
using System.Text;

namespace jfs_ping.ContentChannel
{
    public class ChannelManager
    {
        private List<IChannelManager> managers = new List<IChannelManager>();

        public ChannelManager()
        {

        }

        public void AddManager(IChannelManager manager)
        {
            managers.Add(manager);
        }

        public void CheckChannels(ITextChannel channel)
        {
            foreach(IChannelManager manager in managers)
            {
                if (manager.IsEnabled())
                    manager.CheckChannels(channel);
            }
        }
    }
}
