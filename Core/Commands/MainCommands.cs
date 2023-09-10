using Discord.Commands;
using Azusa.bot_3.Language;
using Discord.WebSocket;
using Azusa.bot_3.Core.Managers;
using System.Threading.Tasks;
using System;
using Discord;
using System.Linq;

namespace Azusa.bot_3.Core.Commands
{
    public class MainCommands : ModuleBase<SocketCommandContext>
    {
        StringManager StringManager = new StringManager();
        private static CommandService _commandService = ServiceManager.GetService<CommandService>();
        private static DiscordSocketClient _client = ServiceManager.GetService<DiscordSocketClient>();

        [Command("random")]
        public async Task Random(int randomMin = 0, int randomMax = 101)
        {
            var eb = new EmbedBuilder();
            Random rnd = new Random();
            eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "RandomTitle")}");
            eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "Random")} {rnd.Next(randomMin, randomMax)}");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("help")]
        public async Task HelpCommand()
        {
            var eb = new EmbedBuilder();
            eb.WithTitle(StringManager.getString(Context.Guild.Id, "HelpTitle"));
            eb.AddField(x =>
            {
                x.Name = StringManager.getString(Context.Guild.Id, "HelpMainCommands");
                x.Value = StringManager.getString(Context.Guild.Id, "HelpMainCommandsList");
                x.IsInline = false;
            });
            eb.AddField(x =>
            {
                x.Name = StringManager.getString(Context.Guild.Id, "HelpFunCommands");
                x.Value = StringManager.getString(Context.Guild.Id, "HelpFunCommandsList");
                x.IsInline = false;
            });
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("kick")]
        public async Task Kick(IGuildUser userAccount = null, string reason = null)
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser; // Needed for rights check.
            var role = (user as IGuildUser).GuildPermissions.Administrator;

            if (userAccount == null)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "KickTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "KickChooseUser");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
                return;
            }
            if (userAccount == Context.User)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "KickTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = "...Baka";
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
                return;
            }
            if (user.GuildPermissions.KickMembers)
            {
                if (!userAccount.GuildPermissions.Administrator)
                {
                    await userAccount.SendMessageAsync($"{StringManager.getString(Context.Guild.Id, "KickKicked")} {Context.Guild.Name}! {StringManager.getString(Context.Guild.Id, "KickReason")} " + (reason != null ? reason : $"{StringManager.getString(Context.Guild.Id, "KickNoReason")}"));
                    await Context.Guild.GetUser(userAccount.Id).KickAsync();
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "KickTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameSuccess");
                        x.Value = StringManager.getString(Context.Guild.Id, "KickKicked1") + " " + userAccount.Username + " " + StringManager.getString(Context.Guild.Id, "KickKicked2");
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
                else
                {
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "KickTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                        x.Value = StringManager.getString(Context.Guild.Id, "KickAdministrator");
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "KickTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "NotEnoughRights");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
            }
        }
        [Command("ban")]
        public async Task Ban(IGuildUser userAccount = null, string reason = null)
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;

            if (userAccount == null)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "BanTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "KickChooseUser");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
                return;
            }
            if (userAccount == Context.User)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "BanTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = "...Baka";
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
                return;
            }
            if (user.GuildPermissions.BanMembers)
            {
                if (!userAccount.GuildPermissions.Administrator)
                {
                    await userAccount.SendMessageAsync($"{StringManager.getString(Context.Guild.Id, "BanBanned")} {Context.Guild.Name}! {StringManager.getString(Context.Guild.Id, "BanReason")} " + (reason != null ? reason : $"{StringManager.getString(Context.Guild.Id, "BanNoReason")}"));
                    await Context.Guild.AddBanAsync(userAccount, 0, reason);
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "BanTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameSuccess");
                        x.Value = StringManager.getString(Context.Guild.Id, "BanBanned1") + " " + userAccount.Username + " " + StringManager.getString(Context.Guild.Id, "BanBanned2");
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
                else
                {
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "BanTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                        x.Value = StringManager.getString(Context.Guild.Id, "BanAdministrator");
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "BanTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "NotEnoughRights");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
            }
        }
        [Command("language")]
        public async Task Language(string lang = null)
        {
            var eb = new EmbedBuilder();
            await LanguageChanger.ChangeLanguage(lang, Context.Guild.Id);
            if (lang == "english" || lang == "English" || lang == "en")
            {
                eb.WithTitle("Language");
                eb.AddField(x =>
                {
                    x.Name = ":white_check_mark: Success.";
                    x.Value = "OK! Now i will talk on english <3";
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else if (lang == "russian" || lang == "Russian" || lang == "ru")
            {
                eb.WithTitle("Язык");
                eb.AddField(x =>
                {
                    x.Name = ":white_check_mark: Успешно.";
                    x.Value = "ОК! Теперь я буду разговаривать на русском <3";
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle("Language");
                eb.AddField(x =>
                {
                    x.Name = ":x: Error.";
                    x.Value = "Language not found. Available languages: english(en), russian(ru)";
                    x.IsInline = false;
                });
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("purge")]
        public async Task PurgeChat(int amount)
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;
            var manageChannelsRole = (user as IGuildUser).GuildPermissions.ManageChannels;
            var manageMessagesRole = (user as IGuildUser).GuildPermissions.ManageMessages;

            if (!manageChannelsRole && manageMessagesRole)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "PurgeTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "NotEnoughRights");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
            }
            if (amount <= 0)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "PurgeTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "PurgeInvalidAmount");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
            }
            await ReplyAsync(StringManager.getString(Context.Guild.Id, "PurgeInProgress"));
            var messages = await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount).FlattenAsync(); // Downloads messages
            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14); // Filters messages if they are older than 14 days.
            var count = filteredMessages.Count(); // Messages count.

            if (count == 0)
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "PurgeTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "PurgeNoMessages");
                    x.IsInline = false;
                });
                await ReplyAsync("", false, eb.Build());
                return;
            }
            await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages); // Deletes the messages.
            eb.WithTitle(StringManager.getString(Context.Guild.Id, "PurgeTitle"));
            eb.AddField(x =>
            {
                x.Name = StringManager.getString(Context.Guild.Id, "NameSuccess");
                x.Value = StringManager.getString(Context.Guild.Id, "PurgeDone") + " " + count + " " + StringManager.getString(Context.Guild.Id, "PurgeDone2");
                x.IsInline = false;
            });
            await ReplyAsync("", false, eb.Build());
        }
        [Command("flipcoin")]
        public async Task FlipCoin()
        {
            var eb = new EmbedBuilder();
            Random rnd = new Random();
            var result = rnd.Next(0, 2) == 0 ? $"{StringManager.getString(Context.Guild.Id, "FlipCoinHeads")}" : $"{StringManager.getString(Context.Guild.Id, "FlipCoinTails")}";

            eb.WithTitle(StringManager.getString(Context.Guild.Id, "FlipCoinTitle"));
            eb.AddField(x =>
            {
                x.Name = StringManager.getString(Context.Guild.Id, "FlipCoinTitle");
                x.Value = result;
                x.IsInline = false;
            });
            await ReplyAsync("", false, eb.Build());
        }
        [Command("setprefix")]
        public async Task SetPrefix(string prefix = null)
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;

            if (user.GuildPermissions.Administrator)
            {
                if (prefix == null)
                {
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "CustomPrefixTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                        x.Value = StringManager.getString(Context.Guild.Id, "SetPrefixNotEnoughArguments");
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
                else
                {
                    await PrefixManager.setPrefix(Context.Guild.Id, prefix);
                    eb.WithTitle(StringManager.getString(Context.Guild.Id, "CustomPrefixTitle"));
                    eb.AddField(x =>
                    {
                        x.Name = StringManager.getString(Context.Guild.Id, "NameSuccess");
                        x.Value = StringManager.getString(Context.Guild.Id, "SetPrefixDone") + " " + prefix;
                        x.IsInline = false;
                    });
                    await ReplyAsync("", false, eb.Build());
                }
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "CustomPrefixTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "NotEnoughRights");
                    x.IsInline = false;
                }); 
                await ReplyAsync("", false, eb.Build());
            }
        }
        [Command("resetprefix")]
        public async Task Resetprefix()
        {
            var eb = new EmbedBuilder();
            var user = Context.User as SocketGuildUser;

            if (user.GuildPermissions.Administrator)
            {
                await PrefixManager.resetPrefix(Context.Guild.Id);
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "ResetPrefixTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameSuccess");
                    x.Value = StringManager.getString(Context.Guild.Id, "ResetPrefixDone");
                    x.IsInline = false;
                }); 
                await ReplyAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "ResetPrefixTitle"));
                eb.AddField(x =>
                {
                    x.Name = StringManager.getString(Context.Guild.Id, "NameError");
                    x.Value = StringManager.getString(Context.Guild.Id, "NotEnoughRights");
                    x.IsInline = false;
                }); 
                await ReplyAsync("", false, eb.Build());
            }
        }
        [Command("whatsnew")]
        public async Task WhatsNew()
        {
            await Context.Channel.SendMessageAsync(StringManager.getString(Context.Guild.Id, "WhatsNew"));
        }
    }
}