using Discord.Commands;
using System.Threading.Tasks;
using System.Threading;
using System;
using Azusa.bot_3.Core.Managers;
using Azusa.bot_3.Language;
using Discord;
using Discord.Net.Rest;

namespace Azusa.bot_3.Core.Commands
{
    public class NSFWCommands : ModuleBase<SocketCommandContext>
    {
        StringManager StringManager = new StringManager();
        [Command("ass")]
        public async Task Ass()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hass");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("anal")]
        public async Task Anal()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("anal");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("sex")]
        public async Task Sex()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                Random rnd = new Random();
                string url = rnd.Next(0, 2) == 0 ? APIManager.GetAPIURLNSFW("classic") : APIManager.GetAPIURLNSFW("gif");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("masturbation")]
        public async Task Masturbation()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("masturbation");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("panties")]
        public async Task Panties()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("pantsu");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("pussy")]
        public async Task Pussy()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("pussy");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("blowjob")]
        public async Task Blowjob()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("blowjob");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("footjob")]
        public async Task Footjob()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("footjob");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("boobs")]
        public async Task Boobs()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hboobs");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("thighs")]
        public async Task Thighs()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hthigh");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("neko")]
        public async Task Neko()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("lewdneko");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("hentai")]
        public async Task Hentai()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hentai");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("incest")]
        public async Task Incest()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("incest");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("yuri")]
        public async Task Yuri()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hyuri");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("handjob")]
        public async Task Handjob()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIURLNSFW("handjob");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("femboy")]
        public async Task Femboy()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIRule34("femboy");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("paizuri")]
        public async Task Paizuri()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("paizuri");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("kitsune")]
        public async Task Kitsune()
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIV2("hkitsune");
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(":wink:");
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "NSFWCommandTitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("gelbooru")]
        public async Task Gelbooru([Remainder]string tags)
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIGelbooru(tags + " -rating:general -rating:sensitive");
                if(url == "Not found")
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GelbooruAPITitle")}");
                    eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "NSFWAPINotFound")} " + tags);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else if (url == "ERR")
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GelbooruAPITitle")}");
                    eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "NSFWAPIError")}");
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GelbooruAPITitle")}");
                    eb.WithDescription(":wink:");
                    eb.WithImageUrl(url);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "GelbooruAPITitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("rule34")]
        public async Task Rule34([Remainder]string tags)
        {
            var eb = new EmbedBuilder();
            ITextChannel channel = (ITextChannel)Context.Channel; // Get current channel to check for NSFW attribute.
            if (channel.IsNsfw)
            {
                string url = APIManager.GetAPIRule34(tags);
                if (url == "Not Found")
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "Rule34APITitle")}");
                    eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "NSFWAPINotFound")} " + tags);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else if (url == "ERR")
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "Rule34APITitle")}");
                    eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "NSFWAPIError")}");
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
                else
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "Rule34APITitle")}");
                    eb.WithDescription(":wink:");
                    eb.WithImageUrl(url);
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                }
            }
            else
            {
                eb.WithTitle(StringManager.getString(Context.Guild.Id, "Rule34APITitle"));
                eb.WithDescription(StringManager.getString(Context.Guild.Id, "NSFWCommandChannelError"));
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
    }
}