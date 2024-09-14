using Filesystem.Models;

namespace Filesystem.Entities.Commands.Connect;

public class LocalConnectCommand : ICommand
{
    private readonly string _connectPath;

    public LocalConnectCommand(string path)
    {
        _connectPath = path;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        if (fileSystem is not null)
        {
            return new ExecutionResult.Fault("FileSystem has already connected. Disconnect before");
        }

        string pathRoot = Path.GetFullPath(_connectPath);
        if (!Path.Exists(pathRoot))
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        return new ExecutionResult.NewFileSystem(new LocalFileSystem(pathRoot));
    }
}