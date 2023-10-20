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
using Discord;
using Azusa.bot_3.Language;

namespace Azusa.bot_3.Core.Commands
{
    public class MusicCommands : ModuleBase<SocketCommandContext>
    {
        private readonly IAudioService lavalink;
        StringManager stringManager = new StringManager();

        public MusicCommands(IAudioService lavalink)
        {
            this.lavalink = lavalink;
            lavalink.StartAsync();
        }
        [Command("play")]
        public async Task PlayAsync([Remainder]string query)
        {
            var eb = new EmbedBuilder();
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
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicPlay");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNoResults") + $" {query}";
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            await player.PlayAsync(track).ConfigureAwait(false);
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicPlay");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicNowPlaying") + $" {track.Title}";
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("stop")]
        public async Task StopAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicStop");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNothingPlaying");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            await player.StopAsync().ConfigureAwait(false);
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicStop");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicStoppedPlaying");
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("pause")]
        public async Task PauseAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.State is PlayerState.Paused)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicPause");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicAlreadyPaused");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            await player.PauseAsync().ConfigureAwait(false);
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicPause");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicPaused");
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("resume")]
        public async Task ResumeAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.State is not PlayerState.Paused)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicResume");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicAlreadyResumed");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            await player.ResumeAsync().ConfigureAwait(false);
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicResume");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicResumed");
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("skip")]
        public async Task SkipAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: false, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicSkip");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNothingPlaying");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicSkip");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicSkipped") + $" {player.CurrentTrack.Title}";
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
            await player.SkipAsync().ConfigureAwait(false);
        }
        [Command("position")]
        public async Task PositionAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicPosition");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNothingPlaying");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicPosition");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicPositionText") + $" {player.Position?.Position} / {player.CurrentTrack.Duration}";
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("list")]
        public async Task ListAsync()
        {
            var eb = new EmbedBuilder();
            var descriptionBuilder = new StringBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (player.CurrentTrack is null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicList");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNothingPlaying");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            if(player.Queue.Count < 1 && player.CurrentTrack != null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicList");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNowPlaying") + $" {player.CurrentTrack.Title}" + "\n" + stringManager.getString(Context.Guild.Id, "MusicQueueEmpty");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            else
            {
                var trackNum = 1;
                foreach(var track in player.Queue)
                {
                    descriptionBuilder.Append($"{trackNum}: [{track.Track.Title}]\n");
                    trackNum++;
                }
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicList");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicNowPlaying") + $" {player.CurrentTrack.Title}" + "\n" + descriptionBuilder.ToString();
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
            }
        }
        [Command("leave")]
        public async Task LeaveAsync()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            await player.StopAsync();
            await player.DisconnectAsync();
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicLeave");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicLeft");
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        [Command("remove")]
        public async Task RemoveSongAsync(int trackNum)
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var player = await GetPlayerAsync(connectToVoiceChannel: true, user).ConfigureAwait(false);
            if (player is null)
            {
                return;
            }
            if (trackNum == 0)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicRemove");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicRemoveIndexZero");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            if (player.Queue.Count == 0)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicRemove");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicQueueEmpty");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            if (player.Queue.Count < trackNum - 1)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicRemove");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicSongDoesntExist");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            var toRemove = player.Queue.ElementAt(trackNum - 1);
            if (toRemove == null)
            {
                eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                eb.AddField(x =>
                {
                    x.Name = stringManager.getString(Context.Guild.Id, "MusicRemove");
                    x.Value = stringManager.getString(Context.Guild.Id, "MusicSongDoesntExist");
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return;
            }
            string songName = player.Queue.ElementAt(trackNum - 1).Track.Title;
            await player.Queue.RemoveAsync(player.Queue.ElementAt(trackNum - 1));
            eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
            eb.AddField(x =>
            {
                x.Name = stringManager.getString(Context.Guild.Id, "MusicRemove");
                x.Value = stringManager.getString(Context.Guild.Id, "MusicRemoved") + $" {songName}";
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
        }
        private async ValueTask<QueuedLavalinkPlayer?> GetPlayerAsync(bool connectToVoiceChannel = true, SocketGuildUser user = null)
        {
            var eb = new EmbedBuilder();
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
                /*var errorMessage = result.Status switch
                {
                    PlayerRetrieveStatus.UserNotInVoiceChannel => "UserNotConnected",
                    PlayerRetrieveStatus.BotNotConnected => "BotNotConnected",
                    _ => "ERR",
                };*/
                switch (result.Status)
                {
                    case PlayerRetrieveStatus.UserNotInVoiceChannel:
                        eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                        eb.AddField(x =>
                        {
                            x.Name = stringManager.getString(Context.Guild.Id, "MusicError");
                            x.Value = stringManager.getString(Context.Guild.Id, "MusicUserNull");
                            x.IsInline = false;
                        });
                        break;
                    case PlayerRetrieveStatus.BotNotConnected:
                        eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                        eb.AddField(x =>
                        {
                            x.Name = stringManager.getString(Context.Guild.Id, "MusicError");
                            x.Value = stringManager.getString(Context.Guild.Id, "MusicBotNull");
                            x.IsInline = false;
                        });
                        break;
                    default:
                        eb.WithTitle(stringManager.getString(Context.Guild.Id, "MusicTitle"));
                        eb.AddField(x =>
                        {
                            x.Name = stringManager.getString(Context.Guild.Id, "MusicError");
                            x.Value = stringManager.getString(Context.Guild.Id, "MusicUnknownError");
                            x.IsInline = false;
                        });
                        break;
                }

                await Context.Channel.SendMessageAsync("", false, eb.Build()).ConfigureAwait(false);
                return null;
            }

            return result.Player;
        }
    }
}