namespace Filesystem.Entities.Commands.Connect;

public class ConnectCommandBuilder : IBuilder, IWithPath<ConnectCommandBuilder>
{
    private string? _path;
    private IConnectFactory? _fileSystemMode;

    public ConnectCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public ConnectCommandBuilder WithFileSystemMode(IConnectFactory fileSystemMode)
    {
        _fileSystemMode = fileSystemMode;
        return this;
    }

    public ICommand Build()
    {
        if (_fileSystemMode is null)
        {
            throw new ArgumentNullException(nameof(_path));
        }

        return _fileSystemMode.Create(
            _path ?? throw new ArgumentNullException(nameof(_path)));
    }
}