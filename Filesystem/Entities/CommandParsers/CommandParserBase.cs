using Filesystem.Models;

namespace Filesystem.Entities.CommandParsers;

public abstract class CommandParserBase : ICommandParser
{
    protected ICommandParser? NextCommand { get; private set; }

    public ICommandParser AddNextParser(ICommandParser commandParser)
    {
        if (NextCommand is null)
        {
            NextCommand = commandParser;
        }
        else
        {
            NextCommand.AddNextParser(commandParser);
        }

        return this;
    }

    public abstract ParseResult Parse(string command);
}