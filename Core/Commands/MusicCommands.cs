using System.Threading.Tasks;
using Lavalink4NET.Players.Queued;
using Lavalink4NET.Players;
using Lavalink4NET;
using Lavalink4NET.DiscordNet;
using Discord.Commands;
using Microsoft.Extensions.Options;
using Discord.WebSocket;
using Lavalink4NET.Rest.Entities.Tracks;
using System.Text;
using System.Linq;
using System;

namespace Azusa.bot_3.Core.Commands
{
    public class MusicCommands : ModuleBase<SocketCommandContext>
    {
        private readonly IAudioService lavalink;

        public MusicCommands(IAudioService lavalink)
        {
            this.lavalink = lavalink;
            lavalink.StartAsync();
        }
        [Command("play")]
        public async Task PlayAsync(string query)
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            var track = await lavalink.Tracks
                    .LoadTrackAsync(query, TrackSearchMode.YouTube)
                    .ConfigureAwait(false);
            if (track is null)
            {
                await Context.Channel.SendMessageAsync("No results.").ConfigureAwait(false);
                return;
            }
            await player.PlayAsync(track).ConfigureAwait(false);
            await Context.Channel.SendMessageAsync($"Playing: {track.Title}").ConfigureAwait(false);
        }
        [Command("stop")]
        public async Task StopAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                await Context.Channel.SendMessageAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }
            await player.StopAsync().ConfigureAwait(false);
            await Context.Channel.SendMessageAsync("Stopped playing.").ConfigureAwait(false);
        }
        [Command("pause")]
        public async Task PauseAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.State is  PlayerState.Paused)
            {
                await Context.Channel.SendMessageAsync("Player is already paused.").ConfigureAwait(false);
                return;
            }
            await player.PauseAsync().ConfigureAwait(false);
            await Context.Channel.SendMessageAsync("Paused.").ConfigureAwait(false);
        }
        [Command("resume")]
        public async Task ResumeAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.State is not PlayerState.Paused)
            {
                await Context.Channel.SendMessageAsync("Player is not paused.").ConfigureAwait(false);
                return;
            }
            await player.ResumeAsync().ConfigureAwait(false);
            await Context.Channel.SendMessageAsync("Resumed.").ConfigureAwait(false);
        }
        [Command("skip")]
        public async Task SkipAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                await Context.Channel.SendMessageAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }
            await player.SkipAsync().ConfigureAwait(false);
            var track = player.CurrentTrack;
            if (track is not null)
            {
                await Context.Channel.SendMessageAsync($"Skipped. Now playing: {track.Uri}").ConfigureAwait(false);
            }
            else
            {
                await Context.Channel.SendMessageAsync("Skipped. Stopped playing because the queue is now empty.").ConfigureAwait(false);
            }
        }
        [Command("position")]
        public async Task PositionAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                await Context.Channel.SendMessageAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }
            await Context.Channel.SendMessageAsync($"Position: {player.Position?.Position} / {player.CurrentTrack.Duration}.").ConfigureAwait(false);
        }
        [Command("list")]
        public async Task ListAsync()
        {
            var descriptionBuilder = new StringBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                await Context.Channel.SendMessageAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }
            if(player.Queue.Count < 1 && player.CurrentTrack != null)
            {
                await Context.Channel.SendMessageAsync("Queue is empty.").ConfigureAwait(false);
                return;
            }
            else
            {
                var trackNum = 1;
                foreach(var track in player.Queue)
                {
                    descriptionBuilder.Append($"{trackNum}: [{track.Track.Title}]");
                    trackNum++;
                }
                await Context.Channel.SendMessageAsync(descriptionBuilder.ToString());
            }
        }
        [Command("leave")]
        public async Task LeaveAsync()
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            await player.StopAsync();
            await player.DisconnectAsync();
            await Context.Channel.SendMessageAsync("Disconnected.");
        }
        [Command("remove")]
        public async Task RemoveSongAsync(int trackNum)
        {
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (trackNum == 0)
            {
                await Context.Channel.SendMessageAsync("Track number cannot be zero.");
                return;
            }
            if (player.Queue.Count == 0)
            {
                await Context.Channel.SendMessageAsync("Queue is empty.");
                return;
            }
            if (player.Queue.Count < trackNum - 1)
            {
                await Context.Channel.SendMessageAsync("Song doesn't exist.");
                return;
            }
            var toRemove = player.Queue.ElementAt(trackNum - 1);
            if (toRemove == null)
            {
                await Context.Channel.SendMessageAsync("Song doesn't exist.");
                return;
            }
            await player.Queue.RemoveAsync(player.Queue.ElementAt(trackNum - 1));
            await Context.Channel.SendMessageAsync("Removed from queue.");
        }
        private async ValueTask<QueuedLavalinkPlayer?> GetPlayerAsync(bool connectToVoiceChannel = true, SocketGuildUser user = null)
        {
            var channelBehavior = connectToVoiceChannel 
                ? PlayerChannelBehavior.Join
                : PlayerChannelBehavior.None;

            var options = Options.Create(new QueuedLavalinkPlayerOptions());
            var retrieveOptions = new PlayerRetrieveOptions(ChannelBehavior: channelBehavior);

            var result = await lavalink.Players
                .RetrieveAsync(Context.Guild.Id, memberVoiceChannel: user?.VoiceChannel?.Id, playerFactory: PlayerFactory.Queued, options: options, retrieveOptions, cancellationToken: default)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                var errorMessage = result.Status switch
                {
                    PlayerRetrieveStatus.UserNotInVoiceChannel => "You are not connected to a voice channel.",
                    PlayerRetrieveStatus.BotNotConnected => "The bot is currently not connected.",
                    _ => "Unknown error.",
                };

                await Context.Channel.SendMessageAsync(errorMessage).ConfigureAwait(false);
                return null;
            }

            return result.Player;
        }
    }
}