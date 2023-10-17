using System;
using System.Threading.Tasks;
using Azusa.bot_3.Core.Modules;
using Azusa.bot_3.Language;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Lavalink4NET;
using Lavalink4NET.Players.Queued;
using Microsoft.Extensions.DependencyInjection;

namespace Azusa.bot_3.Core.Managers
{
    public class EventManager
    {
        private static DiscordSocketClient _client = ServiceManager.GetService<DiscordSocketClient>();
        private static CommandService _commandService = ServiceManager.GetService<CommandService>();
        private static StringManager StringManager = new StringManager();
        
        public static async Task LoadCommands()
        {
            _client.Log += message =>
            {
                Console.WriteLine($"[{DateTime.Now}]\t({message.Source})\t{message.Message}"); // Logs
                return Task.CompletedTask;
            };
            _commandService.Log += message =>
            {
                Console.WriteLine($"[{DateTime.Now}]\t({message.Source})\t{message.Message}"); // Logs
                return Task.CompletedTask;
            };
            var sCommands = ServiceManager.Provider.GetRequiredService<InteractionService>();
            await ServiceManager.Provider.GetRequiredService<InteractionManager>().InitializeAsync();
            _client.Ready += async () =>
            {
                await sCommands.RegisterCommandsGloballyAsync();
            };
            _client.Ready += OnReady; // Executed at startup.
            _client.MessageReceived += OnMessageReceived;
        }
        private static async Task OnReady()
        {
            Console.WriteLine($"[{DateTime.Now}]\t(READY)\tI'm ready to rock!");
            await _client.SetStatusAsync(UserStatus.Online); // Sets online status.
        }
        private static async Task OnMessageReceived(SocketMessage arg) // SocketMessage user for sending messages.
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            ISocketMessageChannel channel = arg.Channel;
            var guildChannel = channel as SocketGuildChannel;
            ulong guildID = guildChannel.Guild.Id;

            if(message.Author.IsBot || message.Channel is SocketDMChannel) return; // Check for a Bot and DM.
            var argPos = 0;

            if (message.Content == "a!forgotprefix")
            {
                await context.Channel.SendMessageAsync($"{StringManager.getString(guildID, "ForgotPrefix")} \"{PrefixManager.getPrefix(guildID)}\"");
                return;
            }

            if (!(message.HasStringPrefix(PrefixManager.getPrefix(guildID), ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;

            var result = await _commandService.ExecuteAsync(context, argPos, ServiceManager.Provider);
            
            if(!result.IsSuccess)
            {
                if(result.Error == CommandError.UnknownCommand) return; // Check for non existing command.
            }
        }
    }
}