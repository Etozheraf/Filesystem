namespace Filesystem.Entities.Commands.TreeGoto;

public class TreeGotoCommandBuilder : IWithPath<TreeGotoCommandBuilder>, IBuilder
{
    private string? _path;

    public TreeGotoCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new TreeGotoCommand(
            _path ?? throw new ArgumentNullException(nameof(_path)));
    }
}