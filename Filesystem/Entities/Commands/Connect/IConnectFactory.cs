namespace Filesystem.Entities.Commands.Connect;

public interface IConnectFactory
{
    ICommand Create(string path);
}