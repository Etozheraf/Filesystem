using Filesystem.Models;

namespace Filesystem.Entities.Commands;

public interface ICommand
{
    ExecutionResult Execute(IFileSystem? fileSystem);
}