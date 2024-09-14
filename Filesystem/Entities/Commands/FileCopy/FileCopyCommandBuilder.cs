namespace Filesystem.Entities.Commands.FileCopy;

public class FileCopyCommandBuilder :
    IBuilder,
    IWithPath<FileCopyCommandBuilder>,
    IWithDestinationPath<FileCopyCommandBuilder>
{
    private string? _sourcePath;
    private string? _destinationPath;

    public FileCopyCommandBuilder WithPath(string path)
    {
        _sourcePath = path;
        return this;
    }

    public FileCopyCommandBuilder WithDestinationPath(string path)
    {
        _destinationPath = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileCopyCommand(
            _sourcePath ?? throw new ArgumentNullException(nameof(_sourcePath)),
            _destinationPath ?? throw new ArgumentNullException(nameof(_destinationPath)));
    }
}