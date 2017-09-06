using jfs_ping.ContentChannel;
using System;
using System.Collections.Generic;
using System.Text;

namespace jfs_ping
{
    public class StartupEventArgs : EventArgs
    {
        public ChannelManager ChannelManager
        {
            get;
            set;
        }

        public StartupEventArgs(ChannelManager manager)
        {
            ChannelManager = manager;
        }
    }
}
