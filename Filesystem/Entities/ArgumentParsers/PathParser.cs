using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class PathParser<T> : ArgumentParserBase<T>
    where T : IWithPath<T>, IBuilder
{
    public override ParseResult Parse(T builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Success(builder);

        builder.WithPath(argument.Current);

        argument.MoveNext();

        return Next is null
            ? new ParseResult.Success(builder)
            : Next.Parse(builder, argument);
    }
}