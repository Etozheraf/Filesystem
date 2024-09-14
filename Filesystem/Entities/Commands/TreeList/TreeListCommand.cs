using Filesystem.Entities.TreeVisitors;
using Filesystem.Models;

namespace Filesystem.Entities.Commands.TreeList;

public class TreeListCommand : ICommand
{
    private readonly int _depth;
    public TreeListCommand(int depth)
    {
        _depth = depth;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        if (fileSystem is null)
            return new ExecutionResult.Fault("Connect before");

        ExecutionResult result = fileSystem.TreeList();
        if (result is not ExecutionResult.SuccessTreeList successTreeList)
            return result;

        if (!successTreeList.Dictionary.Any())
        {
            successTreeList.Directory.Accept(new ConsoleVisitor(_depth));
        }
        else
        {
            successTreeList.Directory.Accept(new ConsoleVisitor(successTreeList.Dictionary, _depth));
        }

        return new ExecutionResult.Success();
    }
}