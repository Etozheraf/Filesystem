using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class DeclarationModeParser<T> : ArgumentParserBase<T>
    where T : IBuilder
{
    private readonly string _shortName;
    private readonly IArgumentParser<T> _modeParser;

    public DeclarationModeParser(string shortName, IArgumentParser<T> modeParser)
    {
        _shortName = shortName;
        _modeParser = modeParser;
    }

    public override ParseResult Parse(T builder, IEnumerator<string> argument)
    {
        if (argument.Current != "-" + _shortName)
        {
            return Next is null
                ? new ParseResult.Fault("Unspecified mode")
                : Next.Parse(builder, argument);
        }

        argument.MoveNext();
        return _modeParser.Parse(builder, argument);
    }
}