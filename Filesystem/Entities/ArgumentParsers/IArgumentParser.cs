using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public interface IArgumentParser<TBuilder>
    where TBuilder : IBuilder
{
    IArgumentParser<TBuilder> AddNext(IArgumentParser<TBuilder> nextParser);
    ParseResult Parse(TBuilder builder, IEnumerator<string> argument);
}