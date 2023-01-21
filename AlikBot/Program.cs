using Discord;
using Discord.WebSocket;

namespace AlikBot;

class Program
{
    public static Config Config;
    private static DiscordSocketClient _client;

    public static async Task Main(string[] args)
    {
        Config = new Config("config.json");
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.SlashCommandExecuted += VtipCommandHandler;
        await _client.LoginAsync(TokenType.Bot, Config.Data.token.ToString());
        await _client.StartAsync();
        await Task.Delay(-1); // block method
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.Message);
        return Task.CompletedTask;
    }

    private static void CreateSlashCommands()
    {
        _client.Ready += async () =>
        {
            var cmd = new SlashCommandBuilder();
            cmd.Name = "vtip";
            cmd.Description = "Hahaha, velice vtipné";

            //await _client.Guilds.First().CreateApplicationCommandAsync(cmd.Build());
            await _client.CreateGlobalApplicationCommandAsync(cmd.Build());
            Console.WriteLine("Command uploaded");
        };
    }

    private static void DestroyGuildSlashCommand()
    {
        _client.Ready += async () =>
        {
            await _client.Guilds.First().DeleteApplicationCommandsAsync();
        };
    }

    private static async Task VtipCommandHandler(SocketSlashCommand cmd)
    {
        if (cmd.Data.Name != "vtip") { return; }

        var emb = new EmbedBuilder()
        {
            Title = "Vtípek z alík.cz",
            Url = "https://www.alik.cz/v",
            Color = Color.Blue,
            Footer = new EmbedFooterBuilder().WithText("Made by Bakterio")
        };
        
        emb.AddField(":rofl: :rofl: :rofl:", AlikScraper.RandomJoke());

        await cmd.RespondAsync(embed: emb.Build());
    }
}