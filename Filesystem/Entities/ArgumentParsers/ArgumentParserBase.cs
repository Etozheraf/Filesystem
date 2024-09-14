using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public abstract class ArgumentParserBase<TBuilder> : IArgumentParser<TBuilder>
    where TBuilder : IBuilder
{
    protected IArgumentParser<TBuilder>? Next { get; private set; }

    public IArgumentParser<TBuilder> AddNext(IArgumentParser<TBuilder> nextParser)
    {
        if (Next is null)
        {
            Next = nextParser;
        }
        else
        {
            Next.AddNext(nextParser);
        }

        return this;
    }

    public abstract ParseResult Parse(TBuilder builder, IEnumerator<string> argument);
}