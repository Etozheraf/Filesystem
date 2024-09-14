using Filesystem.Models;

namespace Filesystem.Entities.Commands.FileDelete;

public class FileDeleteCommand : ICommand
{
    private string _path;

    public FileDeleteCommand(string path)
    {
        _path = path;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        return fileSystem is null
            ? new ExecutionResult.Fault("Connect before")
            : fileSystem.Delete(_path);
    }
}