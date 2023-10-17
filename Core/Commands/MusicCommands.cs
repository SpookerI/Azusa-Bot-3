using Discord.Interactions;
using System.Threading.Tasks;
using Lavalink4NET.Players.Queued;
using Lavalink4NET.Players;
using Lavalink4NET;
using Lavalink4NET.DiscordNet;
using Lavalink4NET.Rest.Entities.Tracks;

namespace Azusa.bot_3.Core.Commands
{
    [RequireContext(ContextType.Guild)]
    public class MusicCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IAudioService lavalink;

        public MusicCommands(IAudioService lavalink)
        {
            this.lavalink = lavalink;
            lavalink.StartAsync();
        }
        [SlashCommand("play", description: "Plays music", runMode: RunMode.Async)]
        public async Task Play(string query)
        {
            await DeferAsync().ConfigureAwait(false);
            var player = await GetPlayerAsync(connectToVoiceChannel: true).ConfigureAwait(false);
            
            if (player is null)
            {
                return;
            }

            var track = await lavalink.Tracks
                    .LoadTrackAsync(query, TrackSearchMode.YouTube)
                    .ConfigureAwait(false);
            if (track is null)
            {
                await FollowupAsync("No results.").ConfigureAwait(false);
                return;
            }

            await player.PlayAsync(track).ConfigureAwait(false);
            await FollowupAsync($"Playing: {track.Title}").ConfigureAwait(false);
        }
        [SlashCommand("stop", description: "Stops the current track", runMode: RunMode.Async)]
        public async Task Stop()
        {
            var player = await GetPlayerAsync(connectToVoiceChannel: false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                await RespondAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }
            await player.StopAsync().ConfigureAwait(false);
            await RespondAsync("Stopped playing.").ConfigureAwait(false);
        }
        [SlashCommand("pause", description: "Pauses the player.", runMode: RunMode.Async)]
        public async Task PauseAsync()
        {
            var player = await GetPlayerAsync(connectToVoiceChannel: false);

            if (player is null)
            {
                return;
            }

            if (player.State is  PlayerState.Paused)
            {
                await RespondAsync("Player is already paused.").ConfigureAwait(false);
                return;
            }

            await player.PauseAsync().ConfigureAwait(false);
            await RespondAsync("Paused.").ConfigureAwait(false);
        }
        [SlashCommand("resume", description: "Resumes the player.", runMode: RunMode.Async)]
        public async Task ResumeAsync()
        {
            var player = await GetPlayerAsync(connectToVoiceChannel: false);

            if (player is null)
            {
                return;
            }

            if (player.State is not PlayerState.Paused)
            {
                await RespondAsync("Player is not paused.").ConfigureAwait(false);
                return;
            }

            await player.ResumeAsync().ConfigureAwait(false);
            await RespondAsync("Resumed.").ConfigureAwait(false);
        }
        [SlashCommand("skip", description: "Skips the current track", runMode: RunMode.Async)]
        public async Task Skip()
        {
            var player = await GetPlayerAsync(connectToVoiceChannel: false);

            if (player is null)
            {
                return;
            }

            if (player.CurrentTrack is null)
            {
                await RespondAsync("Nothing playing!").ConfigureAwait(false);
                return;
            }

            await player.SkipAsync().ConfigureAwait(false);

            var track = player.CurrentTrack;

            if (track is not null)
            {
                await RespondAsync($"Skipped. Now playing: {track.Uri}").ConfigureAwait(false);
            }
            else
            {
                await RespondAsync("Skipped. Stopped playing because the queue is now empty.").ConfigureAwait(false);
            }
        }
        private async ValueTask<QueuedLavalinkPlayer?> GetPlayerAsync(bool connectToVoiceChannel = true)
        {
            var channelBehavior = connectToVoiceChannel 
                ? PlayerChannelBehavior.Join
                : PlayerChannelBehavior.None;

            var retrieveOptions = new PlayerRetrieveOptions(ChannelBehavior: channelBehavior);

            var result = await lavalink.Players
                .RetrieveAsync(Context, playerFactory: PlayerFactory.Queued, retrieveOptions)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                var errorMessage = result.Status switch
                {
                    PlayerRetrieveStatus.UserNotInVoiceChannel => "You are not connected to a voice channel.",
                    PlayerRetrieveStatus.BotNotConnected => "The bot is currently not connected.",
                    _ => "Unknown error.",
                };

                await FollowupAsync(errorMessage).ConfigureAwait(false);
                return null;
            }

            return result.Player;
        }
    }
}