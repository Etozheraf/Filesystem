namespace Filesystem.Entities.Commands.Disconnect;

public class DisconnectCommandBuilder : IBuilder
{
    public ICommand Build()
    {
        return new DisconnectCommand();
    }
}