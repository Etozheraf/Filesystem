using Filesystem.Entities.Commands.FileShow;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class ConsoleShowModeParser : ArgumentParserBase<FileShowCommandBuilder>
{
    public override ParseResult Parse(FileShowCommandBuilder builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Fault("Missing mod value");

        if (argument.Current != "console")
        {
            return Next is not null
                ? Next.Parse(builder, argument)
                : new ParseResult.Fault("Not implemented mode");
        }

        argument.MoveNext();
        return new ParseResult.Success(builder.WithPrintMode(new ConsoleFileShowCommandFactory()));
    }
}