using Filesystem.Models;

namespace Filesystem.Entities.Commands.FileShow;

public class ConsoleFileShowCommand : ICommand
{
    private readonly string _path;

    public ConsoleFileShowCommand(string path)
    {
        _path = path;
    }

    public ExecutionResult Execute(IFileSystem? fileSystem)
    {
        if (fileSystem is null)
            return new ExecutionResult.Fault("Connect before");

        ExecutionResult result = fileSystem.Show(_path);

        if (result is not ExecutionResult.SuccessShow successShow) return result;

        Console.WriteLine(successShow.Message);
        return new ExecutionResult.Success();
    }
}