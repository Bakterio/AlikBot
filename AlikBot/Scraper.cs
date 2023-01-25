using System.Collections;
using System.Text;
using HtmlAgilityPack;

namespace AlikBot;

public class Scraper
{
    public static string AlikJoke()
    {
        const string URL = "https://www.alik.cz/v/vsechny/";
        var random = new Random();
        var web = new HtmlWeb();
        var doc = web.Load(URL + random.Next(1, 50));
        var jokes = doc.DocumentNode.SelectNodes("//div[@class='vtip']");
        var joke = jokes[random.Next(jokes.Count)];
        return joke.ChildNodes[3].InnerText;
    }

    public static string TvojeMamaJoke()
    {
        const string URL = "https://www.vtipy.club/tvoje_mama/";
        var random = new Random();
        var web = new HtmlWeb();
        web.OverrideEncoding = Encoding.UTF8;
        var doc = web.Load(URL + random.Next(1, 32));
        var content = doc.DocumentNode.SelectSingleNode("//*[@id=\"content\"]");
        var jokes = new List<string>();
        foreach (var node in content.ChildNodes)
        {
            if (!node.HasClass("vtip")) continue;
            var text = node.InnerText.Trim().Split("\n")[0].Remove(0, 10);
            jokes.Add(text);
        }

        return jokes[random.Next(jokes.Count)];
    }
}