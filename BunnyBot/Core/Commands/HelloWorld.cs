using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace BunnyBot.Core.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("helloworld", "world"), Summary("hello world command")]

        public async Task helloWorld()
        {
            await Context.Channel.SendMessageAsync("Hello World");
        }
    }
}
