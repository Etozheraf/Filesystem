using Filesystem.Models;

namespace Filesystem.Entities.Commands.FileCopy;

public class FileCopyCommand : ICommand
{
    private string _sourcePath;
    private string _destinationPath;

    public FileCopyCommand(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        return fileSystem is null
            ? new ExecutionResult.Fault("Connect before")
            : fileSystem.Copy(_sourcePath, _destinationPath);
    }
}