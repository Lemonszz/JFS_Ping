using Discore;
using Discore.WebSocket;
using jfs_ping.ContentChannel;
using System;
using System.Threading.Tasks;

namespace jfs_ping
{
    public class JfsPing
    {
        public static event EventHandler<CommandEventArgs> CommandMessageHandler;
        public static event EventHandler<StartupEventArgs> StartupEvent;

        public ChannelManager channelManager = new ChannelManager();
        public static Settings Settings = new Settings();
        public static UserManager UserManager = new UserManager();

        public static TimeSpan startTime = TimeSpan.Zero;
        public static TimeSpan checkPeriod = TimeSpan.FromSeconds(Settings.Check_Time);

        public Commands commands;
        public YoutubeManager youtubeManager;
        public TwitchManager twitchManager;
        public RedditManager redditManager;

        public static void Main(string[] args)
        {
            JfsPing program = new JfsPing();
            program.Run().Wait();
        }

        public async Task Setup(Shard shard)
        {
            await Task.Delay(2000);
            Console.WriteLine("Bot Started!");
            while (true)
            {
                ITextChannel cha = (ITextChannel)shard.Cache.Channels.Get(new Snowflake(Settings.Channel_ID));
                Console.WriteLine("Checking channels...");

                channelManager.CheckChannels(cha);

                Console.WriteLine("...Done Checking");
                await Task.Delay(checkPeriod);
            }
        }

        public JfsPing()
        {
            StartupEvent += OnStartup;
            StartupEvent(this, new StartupEventArgs(channelManager));

            commands = new Commands(youtubeManager, twitchManager, redditManager);
        }

        public void OnStartup(Object o, StartupEventArgs e)
        {
            youtubeManager = new YoutubeManager();
            redditManager = new RedditManager();
            twitchManager = new TwitchManager();

            e.ChannelManager.AddManager(youtubeManager);
            e.ChannelManager.AddManager(redditManager);
            e.ChannelManager.AddManager(twitchManager);
        }


        public async Task Run()
        {
            // Create authenticator using a bot user token.
            DiscordBotUserToken token = new DiscordBotUserToken(Settings.Bot_Token);

            // Create a WebSocket application.
            DiscordWebSocketApplication app = new DiscordWebSocketApplication(token);

            // Create and start a single shard.
            Shard shard = app.ShardManager.CreateSingleShard();
            await shard.StartAsync();

            // Subscribe to the message creation event.
            shard.Gateway.OnMessageCreated += Gateway_OnMessageCreated;

            await Setup(shard);

            // Wait for the shard to end before closing the program.
            while (shard.IsRunning)
                await Task.Delay(1000);
        }

        public static void Gateway_OnMessageCreated(object sender, MessageEventArgs e)
        {
            Shard shard = e.Shard;
            DiscordMessage message = e.Message;
            ITextChannel textChannel = (ITextChannel)shard.Cache.Channels.Get(message.ChannelId);
            if (message.Author == shard.User)
                return;

            DiscordChannel texstChannel = shard.Cache.Channels[new Snowflake(Settings.Channel_ID)];

            if (message.ChannelId.Id == Settings.Channel_ID)
            {
                if (message.Content.StartsWith("!"))
                {
                    OnCommandMessage(new CommandEventArgs(message, textChannel, shard));
                }
            }
        }

        public static void OnCommandMessage(CommandEventArgs e)
        {
            CommandMessageHandler?.Invoke(null, e);
        }
    }
}