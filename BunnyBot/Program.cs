using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BunnyBot
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            client.MessageReceived += Client_MessageReceived;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            client.Ready += Client_Ready;

            client.Log += Client_Log;

            commands.Log += Commands_Log;

            string token = "";
            using (var stream = new FileStream((Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(@"bin\Debug\netcoreapp2.0", @"Data\token.txt"), FileMode.Open, FileAccess.Read))
            using (var readToken = new StreamReader(stream))
            {
                token = readToken.ReadToEnd();
            }

            await client.LoginAsync(TokenType.Bot, token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Commands_Log(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now} at {message.Source} {message.Message}");
        }

        private async Task Client_Log(LogMessage message)
        {
            Console.WriteLine($"{DateTime.Now} at {message.Source} {message.Message}");
        }

        private async Task Client_Ready()
        {
            await client.SetGameAsync("BunnyBoting", "https://google.com", StreamType.NotStreaming);
        }

        private async Task Client_MessageReceived(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            var context = new SocketCommandContext(client, message);

            if (context.Message == null || context.Message.Content == "") return;
            if (context.User.IsBot) return;

            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var result = await commands.ExecuteAsync(context, argPos);

            if (!result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at commands: Something went wrong executing command. Text: {context.Message.Content} | Error: {result.ErrorReason}");
                context.Channel.SendMessageAsync($":x: {DateTime.Now} at commands: Something went wrong executing command. Text: {context.Message.Content} | Error: {result.ErrorReason}");
            }
                
            

        }
    }
}
