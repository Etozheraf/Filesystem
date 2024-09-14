using System.Text;

namespace Filesystem.Entities.GetEmoji;

public class GetFromConfigEmoji : IGetEmoji
{
    public Dictionary<string, string> GetEmoji()
    {
        var result = new Dictionary<string, string>();

        if (!Path.Exists("config"))
        {
            return result;
        }

        var config = new StreamReader("config", Encoding.UTF8);

        string? name = config.ReadLine();
        while (name is not null && name.StartsWith('#'))
        {
            name = config.ReadLine();
        }

        string? value = config.ReadLine();
        while (value is not null && value.StartsWith('#'))
        {
            value = config.ReadLine();
        }

        while (name is not null && value is not null)
        {
            result.Add(name, value);
            name = config.ReadLine();
            while (name is not null && name.StartsWith('#'))
            {
                name = config.ReadLine();
            }

            value = config.ReadLine();
            while (value is not null && value.StartsWith('#'))
            {
                value = config.ReadLine();
            }
        }

        config.Close();
        return result;
    }
}