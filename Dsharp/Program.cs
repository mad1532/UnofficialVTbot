using Dsharp.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Dsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(File.Exists("VTTOKEN.txt") && File.Exists("DTOKEN.txt"))
            {
                info.tokens.VT_token = System.IO.File.ReadAllText(@"VTTOKEN.txt");
                info.tokens.D_token = System.IO.File.ReadAllText("DTOKEN.txt");
                MainAsync().GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("Missing token files.");
                Console.WriteLine("please enter discord token, then press enter.");
                System.IO.File.WriteAllText("DTOKEN.txt",Console.ReadLine());
                Console.WriteLine("please enter VT token, then press enter.");
                System.IO.File.WriteAllText("VTTOKEN.txt", Console.ReadLine());
            }
            
        }

     internal static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = info.tokens.D_token,
                TokenType = TokenType.Bot
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<MyFirstModule>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
