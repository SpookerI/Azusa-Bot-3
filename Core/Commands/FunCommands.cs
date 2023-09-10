using Discord.Commands;
using System.Threading.Tasks;
using Azusa.bot_3.Language;
using Discord;
using Discord.WebSocket;
using Azusa.bot_3.Core.Managers;
using ChatGPT.Net;
using System;

namespace Azusa.bot_3.Core.Commands
{
    public class FunCommands : ModuleBase<SocketCommandContext>
    {
        StringManager StringManager = new StringManager();

        [Command("hello")]
        public async Task Hello()
        {
            var eb = new EmbedBuilder();
            eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "HelloTitle")}");
            eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "HelloMsg")}" + " " + $"{Context.Message.Author.Mention}!");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("goodjob")]
        public async Task GoodJob()
        {
            var eb = new EmbedBuilder();
            eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GoodJobTitle")}");
            eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "GoodJobMsg")}");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("youarecute")]
        public async Task YouAreCute()
        {
            var eb = new EmbedBuilder();
            eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "YouAreCuteTitle")}");
            eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "YouAreCuteMsg")}");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
        }
        [Command("chatviblyadkov")]
        public async Task SendGif()
        {
            await Context.Channel.SendMessageAsync("https://tenor.com/view/chatviblyadok-gif-25739796");
        }
        [Command("hug")]
        public async Task Hug(SocketUser huggedUser = null)
        {
            var eb = new EmbedBuilder();
            string huggedUserString = null;
            var user = Context.User.Mention;
            string userString = Context.User.ToString();
            if (huggedUser != null)
                huggedUserString = huggedUser.ToString();
            string url = APIManager.GetAPIURLSFW("hug");
            if (huggedUser == null)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "HugTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "HugNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else if (userString == huggedUserString)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "HugTitle")}");
                eb.WithDescription(user +$" {StringManager.getString(Context.Guild.Id, "HugNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "HugTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "HugMention")} " + huggedUser.Mention + $" {StringManager.getString(Context.Guild.Id, "HugMention2")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
        }
        [Command("kiss")]
        public async Task Kiss(SocketUser kissedUser = null)
        {
            var eb = new EmbedBuilder();
            string kissedUserString = null;
            var user = Context.User.Mention;
            string userString = Context.User.ToString();
            if (kissedUser != null)
                kissedUserString = kissedUser.ToString();
            string url = APIManager.GetAPIURLSFW("kiss");
            if (kissedUser == null)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "KissTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "KissNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            if (userString == kissedUserString)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "KissTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "KissNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "KissTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "KissMention")} " + kissedUser.Mention);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
        }
        [Command("pat")]
        public async Task Pat(SocketUser pattedUser = null)
        {
            var eb = new EmbedBuilder();
            string pattedUserString = null;
            var user = Context.User.Mention;
            string userString = Context.User.ToString();
            if (pattedUser != null)
                pattedUserString = pattedUser.ToString();
            string url = APIManager.GetAPIURLSFW("pat");
            if (pattedUser == null)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "PatTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "PatNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else if (userString == pattedUserString)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "PatTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "PatNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "PatTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "PatMention")} " + pattedUser.Mention);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
        }
        [Command("cry")]
        public async Task Cry()
        {
            var eb = new EmbedBuilder();
            var user = Context.User.Mention;
            string url = APIManager.GetAPIURLSFW("cry");
            eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "CryTitle")}");
            eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "CryMsg")}");
            await Context.Channel.SendMessageAsync("", false, eb.Build());
            await Context.Channel.SendMessageAsync(url);
        }
        [Command("slap")]
        public async Task Slap(SocketUser slappedUser = null)
        {
            var eb = new EmbedBuilder();
            string slappedUserString = null;
            var user = Context.User.Mention;
            string userString = Context.User.ToString();
            if (slappedUser != null)
                slappedUserString = slappedUser.ToString();
            string url = APIManager.GetAPIURLSFW("slap");
            if (slappedUser == null)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "SlapTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "SlapNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else if (userString == slappedUserString)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "SlapTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "SlapNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "SlapTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "SlapMention")} " + slappedUser.Mention + $" {StringManager.getString(Context.Guild.Id, "SlapMention2")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
        }
        [Command("lick")]
        public async Task Lick(SocketUser lickedUser = null)
        {
            var eb = new EmbedBuilder();
            string lickedUserString = null;
            var user = Context.User.Mention;
            string userString = Context.User.ToString();
            if (lickedUser != null)
                lickedUserString = lickedUser.ToString();
            string url = APIManager.GetAPIURLSFW("lick");
            if (lickedUser == null)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "LickTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "LickNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else if (userString == lickedUserString)
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "LickTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "LickNull")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
            else
            {
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "LickTitle")}");
                eb.WithDescription(user + $" {StringManager.getString(Context.Guild.Id, "LickMention")} " + lickedUser.Mention);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(url);
            }
        }
        [Command("chatgpt")]
        public async Task ChatGPT([Remainder] string userQuestion = null)
        {
            var eb = new EmbedBuilder();
            try
            {
                if (userQuestion == null)
                {
                    eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GPTTitle")}");
                    eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "GPTAskNull")}");
                    await Context.Channel.SendMessageAsync("", false, eb.Build());
                    return;
                }
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GPTTitle")}");
                eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "GPTAsk")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                var bot = new ChatGpt(ConfigManager.Config.chatGPTKey);
                var response = await bot.Ask(userQuestion);
                eb = new EmbedBuilder();
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GPTTitle")}");
                eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "GPTAskResponse")}");
                await Context.Channel.SendMessageAsync("", false, eb.Build());
                await Context.Channel.SendMessageAsync(response);
            }
            catch (Exception ex)
            {
                eb = new EmbedBuilder();
                eb.WithTitle($"{StringManager.getString(Context.Guild.Id, "GPTTitle")}");
                eb.WithDescription($"{StringManager.getString(Context.Guild.Id, "GPTAskError")} " + ex.Message);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
        [Command("safegelbooru")]
        public async Task Gelbooru([Remainder]string tags)
        {
            var eb = new EmbedBuilder();
            string url = APIManager.GetAPIGelbooru(tags + " -rating:questionable -rating:explicit");
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
                eb.WithImageUrl(url);
                await Context.Channel.SendMessageAsync("", false, eb.Build());
            }
        }
    }
}