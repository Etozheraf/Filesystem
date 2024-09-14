namespace Filesystem.Entities.Commands.FileShow;

public interface IFileShowCommandFactory
{
    ICommand Create(string path);
}