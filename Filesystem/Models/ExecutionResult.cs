using Filesystem.Entities;
using Filesystem.Entities.TreeVisitors;

namespace Filesystem.Models;

public record ExecutionResult
{
    private ExecutionResult() { }

    public sealed record Success : ExecutionResult;
    public sealed record SuccessShow(string Message) : ExecutionResult;
    public sealed record SuccessTreeList(IDirectory Directory, Dictionary<string, string> Dictionary) : ExecutionResult;
    public sealed record NewFileSystem(IFileSystem? FileSystem) : ExecutionResult;
    public sealed record Fault(string Message) : ExecutionResult;
}