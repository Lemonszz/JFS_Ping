using Discore;

namespace jfs_ping
{
    public interface IChannelManager
    {
        void CheckChannels(ITextChannel channel);

        void LoadChannels();

        void SaveChannels();

        bool IsEnabled();
    }
}