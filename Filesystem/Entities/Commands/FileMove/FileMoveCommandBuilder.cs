namespace Filesystem.Entities.Commands.FileMove;

public class FileMoveCommandBuilder :
    IBuilder,
    IWithPath<FileMoveCommandBuilder>,
    IWithDestinationPath<FileMoveCommandBuilder>
{
    private string? _sourcePath;
    private string? _destinationPath;

    public FileMoveCommandBuilder WithPath(string path)
    {
        _sourcePath = path;
        return this;
    }

    public FileMoveCommandBuilder WithDestinationPath(string path)
    {
        _destinationPath = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileMoveCommand(
            _sourcePath ?? throw new ArgumentNullException(nameof(_sourcePath)),
            _destinationPath ?? throw new ArgumentNullException(nameof(_destinationPath)));
    }
}