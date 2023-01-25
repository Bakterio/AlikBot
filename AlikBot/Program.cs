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
        //CreateSlashCommands();
        _client.SlashCommandExecuted += VtipCommandHandler;
        _client.SlashCommandExecuted += MamaCommandHandler;
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
            var vtip = new SlashCommandBuilder();
            vtip.Name = "vtip";
            vtip.Description = "Hahaha, velice vtipné";

            await _client.CreateGlobalApplicationCommandAsync(vtip.Build());
            
            var mama = new SlashCommandBuilder();
            mama.Name = "tvoje_mama";
            mama.Description = "Tvoje máma, je tak...";

            await _client.CreateGlobalApplicationCommandAsync(mama.Build());
            Console.WriteLine("Commands uploaded");
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
        
        emb.AddField(":rofl: :rofl: :rofl:", Scraper.AlikJoke());

        await cmd.RespondAsync(embed: emb.Build());
    }
    
    private static async Task MamaCommandHandler(SocketSlashCommand cmd)
    {
        if (cmd.Data.Name != "tvoje_mama") { return; }

        var emb = new EmbedBuilder()
        {
            Title = "Jedovatý vtípek o TVOJÍ MÁMĚ :index_pointing_at_the_viewer:",
            Url = "https://www.vtipy.club/tvoje_mama",
            Color = Color.Blue,
            Footer = new EmbedFooterBuilder().WithText("Made by Bakterio")
        };
        
        emb.AddField(":rofl: :rofl: :rofl:", Scraper.TvojeMamaJoke());

        await cmd.RespondAsync(embed: emb.Build());
    }
}