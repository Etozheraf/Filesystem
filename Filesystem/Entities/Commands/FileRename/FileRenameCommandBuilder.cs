namespace Filesystem.Entities.Commands.FileRename;

public class FileRenameCommandBuilder : IBuilder, IWithPath<FileRenameCommandBuilder>
{
    private string? _path;
    private string? _name;

    public FileRenameCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public FileRenameCommandBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ICommand Build()
    {
        return new FileRenameCommand(
            _path ?? throw new ArgumentNullException(nameof(_path)),
            _name ?? throw new ArgumentNullException(nameof(_name)));
    }
}