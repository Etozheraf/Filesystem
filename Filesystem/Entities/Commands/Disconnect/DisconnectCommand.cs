using Filesystem.Models;

namespace Filesystem.Entities.Commands.Disconnect;

public class DisconnectCommand : ICommand
{
    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        if (fileSystem is null)
        {
            return new ExecutionResult.Fault("FileSystem has already disconnected. Connect before");
        }

        return new ExecutionResult.NewFileSystem(null);
    }
}