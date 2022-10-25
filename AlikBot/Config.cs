using Newtonsoft.Json;

namespace AlikBot;

public class Config
{
    private StreamReader _file;
    public dynamic Data;

    public Config(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path);
            Console.WriteLine($"Config file {path} not found, please fill it.");
        }

        _file = File.OpenText(path);
        Data = JsonConvert.DeserializeObject(_file.ReadToEnd())!;
    }
    
    public string Text => _file.ReadToEnd();
}