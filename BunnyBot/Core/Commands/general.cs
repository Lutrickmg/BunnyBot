using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BunnyBot.Core.Commands
{
    public class general : ModuleBase<SocketCommandContext>
    {
        [Command("embed"), Summary("embed test command")]
        public async Task embed([Remainder]string input = "none")
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Test embed", Context.User.GetAvatarUrl());
            embed.WithColor(40, 200, 150);
            embed.WithFooter("Footer for test embed", Context.Guild.Owner.GetAvatarUrl());
            embed.WithDescription("Description for test embed that has a link. \n" +
                "[Here's some website text link](https://google.com)");
            embed.AddInlineField("User input:", input);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("ping"), Summary("ping command to test connection")]
        public async Task ping()
        {
            await Context.Channel.SendMessageAsync("pong");
        }
    }
}
