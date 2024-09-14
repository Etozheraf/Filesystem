namespace Filesystem.Entities.Commands.Connect;

public class LocalConnectFactory : IConnectFactory
{
    public ICommand Create(string path)
    {
        return new LocalConnectCommand(path);
    }
}