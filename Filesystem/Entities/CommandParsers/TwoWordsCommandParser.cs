using Filesystem.Models;

namespace Filesystem.Entities.CommandParsers;

public class TwoWordsCommandParser : CommandParserBase
{
    private readonly ICommandParser _commandParser;
    private readonly string _commandStart;
    public TwoWordsCommandParser(string commandStart, ICommandParser commandParser)
    {
        _commandParser = commandParser;
        _commandStart = commandStart;
    }

    public override ParseResult Parse(string command)
    {
        command = command.Trim();
        if (!command.StartsWith(_commandStart, StringComparison.Ordinal))
        {
            return NextCommand is not null
                ? NextCommand.Parse(command)
                : new ParseResult.Fault("There is no such command.");
        }

        command = command.Remove(0, _commandStart.Length);
        command = command.Trim();

        return _commandParser.Parse(command);
    }
}