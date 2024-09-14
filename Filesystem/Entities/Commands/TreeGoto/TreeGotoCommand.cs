using Filesystem.Models;

namespace Filesystem.Entities.Commands.TreeGoto;

public class TreeGotoCommand : ICommand
{
    private string _path;

    public TreeGotoCommand(string path)
    {
        _path = path;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        return fileSystem is null
            ? new ExecutionResult.Fault("Connect before")
            : fileSystem.TreeGoto(_path);
    }
}