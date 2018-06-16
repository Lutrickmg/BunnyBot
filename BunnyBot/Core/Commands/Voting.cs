using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

using BunnyBot.Core.Data;

namespace BunnyBot.Core.Commands
{
    public class Voting : ModuleBase<SocketCommandContext>
    {
        [Group("vote"), Summary("Group of commands for voting")]
        public class VotesGroup : ModuleBase<SocketCommandContext>
        {
            [Command(""), Alias("me"), Summary("Shows the users current number of votes")]
            public async Task Me()
            {
                try
                {
                    int VoteCount = Data.Data.GetVotes(Context.User.Id);

                    await Context.Channel.SendMessageAsync($"{Context.User.Username} currently has {VoteCount} votes.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            [Command("upvote"), Alias("+1"), Summary("Give another user a vote")]
            public async Task Upvote(IUser User = null)
            {
                if (User == null)
                {
                    await Context.Channel.SendMessageAsync(":x: You didn't mention a user to upvote!  Pleas use this syntax: !vote upvote **<@user>**");
                    return;
                }

                await Data.Data.GiveVote(User.Id);

                int VoteCount = Data.Data.GetVotes(User.Id);

                await Context.Channel.SendMessageAsync($":tada: {User.Mention} you have been upvoted by {Context.User.Username}! Current votes at {VoteCount}");
            }
        }
    }
}
