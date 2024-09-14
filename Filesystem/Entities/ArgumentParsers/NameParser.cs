using Filesystem.Entities.Commands.FileRename;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class NameParser : ArgumentParserBase<FileRenameCommandBuilder>
{
    public override ParseResult Parse(FileRenameCommandBuilder builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Success(builder);

        builder.WithName(argument.Current);

        return Next is null
            ? new ParseResult.Success(builder)
            : Next.Parse(builder, argument);
    }
}