using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace Alik;

public class Config
{
    private StreamReader _file;

    public Config(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path);
            Console.WriteLine($"Config file {path} not found, please fill it.");
        }

        _file = File.OpenText(path);
    }
    
    public string Text => _file.ReadToEnd();
}