using Filesystem.Models;

namespace Filesystem.Entities.CommandParsers;

public interface ICommandParser
{
    ICommandParser AddNextParser(ICommandParser commandParser);

    ParseResult Parse(string command);
}