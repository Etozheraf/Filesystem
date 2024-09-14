using Filesystem.Models;

namespace Filesystem.Entities.Commands.FileRename;

public class FileRenameCommand : ICommand
{
    private readonly string _path;
    private readonly string _name;

    public FileRenameCommand(string path, string name)
    {
        _path = path;
        _name = name;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        return fileSystem is null
            ? new ExecutionResult.Fault("Connect before")
            : fileSystem.Rename(_path, _name);
    }
}