using HtmlAgilityPack;

namespace AlikBot;

public class AlikScraper
{
    public static string RandomJoke()
    {
        const string URL = "https://www.alik.cz/v/vsechny/";
        var random = new Random();
        var web = new HtmlWeb();
        var doc = web.Load(URL + random.Next(1, 50));
        var jokes = doc.DocumentNode.SelectNodes("//div[@class='vtip']");
        var joke = jokes[random.Next(jokes.Count)];
        return joke.ChildNodes[3].InnerText;
    }
}