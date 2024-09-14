namespace Filesystem.Entities.Commands.FileShow;

public class FileShowCommandBuilder : IBuilder, IWithPath<FileShowCommandBuilder>
{
    private string? _path;
    private IFileShowCommandFactory _factory = new ConsoleFileShowCommandFactory();

    public FileShowCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public FileShowCommandBuilder WithPrintMode(IFileShowCommandFactory factory)
    {
        _factory = factory;
        return this;
    }

    public ICommand Build()
    {
        return _factory.Create(
            _path ?? throw new ArgumentNullException(nameof(_path)));
    }
}