using Filesystem.Models;

namespace Filesystem.Entities.Commands.FileMove;

public class FileMoveCommand : ICommand
{
    private string _sourcePath;
    private string _destinationPath;

    public FileMoveCommand(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        return fileSystem is null
            ? new ExecutionResult.Fault("Connect before")
            : fileSystem.Move(_sourcePath, _destinationPath);
    }
}