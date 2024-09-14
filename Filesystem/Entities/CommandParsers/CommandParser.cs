using Filesystem.Entities.ArgumentParsers;
using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.CommandParsers;

public class CommandParser<TBuilder> : CommandParserBase
    where TBuilder : IBuilder, new()
{
    private readonly IArgumentParser<TBuilder>? _argumentCommandParser;
    private readonly string _commandStart;

    public CommandParser(string commandStart)
    {
        _commandStart = commandStart;
    }

    public CommandParser(string commandStart, IArgumentParser<TBuilder> argumentCommandParser)
    {
        _argumentCommandParser = argumentCommandParser;
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

        if (_argumentCommandParser is null)
        {
            return new ParseResult.Success(new TBuilder());
        }

        IEnumerator<string> argument = command.Split().ToList().GetEnumerator();
        argument.MoveNext();

        return _argumentCommandParser.Parse(
            new TBuilder(),
            argument);
    }
}