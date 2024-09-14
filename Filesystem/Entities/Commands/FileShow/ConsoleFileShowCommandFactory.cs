namespace Filesystem.Entities.Commands.FileShow;

public class ConsoleFileShowCommandFactory : IFileShowCommandFactory
{
    public ICommand Create(string path)
    {
        return new ConsoleFileShowCommand(path);
    }
}