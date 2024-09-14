using Filesystem.Entities.Commands;

namespace Filesystem.Models;

public record ParseResult
{
    private ParseResult() { }

    public sealed record Success(IBuilder Builder) : ParseResult;

    public sealed record Fault(string Message) : ParseResult;
}