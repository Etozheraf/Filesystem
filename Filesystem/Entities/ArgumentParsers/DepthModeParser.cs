using Filesystem.Entities.Commands.TreeList;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class DepthModeParser : ArgumentParserBase<TreeListCommandBuilder>
{
    public override ParseResult Parse(TreeListCommandBuilder builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Fault("Missing mod value");

        bool result = int.TryParse(argument.Current, out int depth);

        if (!result) return new ParseResult.Fault("Wrong number");

        argument.MoveNext();
        return new ParseResult.Success(builder.WithDepth(depth));
    }
}