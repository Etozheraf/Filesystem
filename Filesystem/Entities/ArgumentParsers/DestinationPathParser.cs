using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class DestinationPathParser<TBuilder> : ArgumentParserBase<TBuilder>
    where TBuilder : IWithDestinationPath<TBuilder>, IBuilder
{
    public override ParseResult Parse(TBuilder builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Success(builder);

        builder.WithDestinationPath(argument.Current);

        argument.MoveNext();

        return Next is null
            ? new ParseResult.Success(builder)
            : Next.Parse(builder, argument);
    }
}