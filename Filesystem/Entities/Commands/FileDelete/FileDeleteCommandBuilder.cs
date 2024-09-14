namespace Filesystem.Entities.Commands.FileDelete;

public class FileDeleteCommandBuilder : IBuilder, IWithPath<FileDeleteCommandBuilder>
{
    private string? _path;

    public FileDeleteCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileDeleteCommand(
            _path ?? throw new ArgumentNullException(nameof(_path)));
    }
}