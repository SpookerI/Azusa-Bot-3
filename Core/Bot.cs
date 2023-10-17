using Discord.WebSocket;
using Discord.Commands;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Discord.Interactions;
using Azusa.bot_3.Core.Managers;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Lavalink4NET.DiscordNet;
using Lavalink4NET.Extensions;
using System;

namespace Azusa.bot_3.Core
{
    public class Bot
    {
        private DiscordSocketClient _client; // Needed for initialization, should be only one in the program.
        private CommandService _commandService; // Needed for initialization, should be only one in the program.
        private DiscordClientWrapper _discordClientWrapper;

        public Bot()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Debug,
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.All
            });
            _commandService = new CommandService(new CommandServiceConfig()
            {
                LogLevel = LogSeverity.Debug,
                CaseSensitiveCommands = false,
                DefaultRunMode = Discord.Commands.RunMode.Async,
                IgnoreExtraArgs = true
            });
            _discordClientWrapper = new DiscordClientWrapper(_client);

            var collection = new ServiceCollection();

            collection.AddSingleton(_client);
            collection.AddSingleton(_commandService);
            collection.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
            collection.AddSingleton<InteractionManager>();
            collection.AddSingleton(_discordClientWrapper);
            collection.AddLavalink();
            collection.ConfigureLavalink(config =>
            {
                config.BaseAddress = new Uri("http://localhost:2333");
                config.Passphrase = "22362236";
                config.ReadyTimeout = TimeSpan.FromSeconds(10);
            });
            ServiceManager.SetProvider(collection);
        }
        public async Task MainAsync()
        {
            if(string.IsNullOrWhiteSpace(ConfigManager.Config.Token)) return; // Check for empty config file

            if(!File.Exists("Config/customPrefix.txt"))
                File.Create("Config/customPrefix.txt");
            if(!File.Exists("Config/servers.txt"))
                File.Create("Config/servers.txt");
            if(!Directory.Exists("Resources"))
                Directory.CreateDirectory("Resources");    
            if(!Directory.Exists("API"))
                Directory.CreateDirectory("API");

            await CommandManager.LoadCommandsAsync();
            await EventManager.LoadCommands();
            await _client.LoginAsync(TokenType.Bot, ConfigManager.Config.Token);
            await _client.StartAsync();

            Thread.Sleep(-1);
        }
    }
}