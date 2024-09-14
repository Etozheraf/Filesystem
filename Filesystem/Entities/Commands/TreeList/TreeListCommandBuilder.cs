namespace Filesystem.Entities.Commands.TreeList;

public class TreeListCommandBuilder : IBuilder
{
    private int _depth = 1;

    public TreeListCommandBuilder WithDepth(int depth)
    {
        _depth = depth;
        return this;
    }

    public ICommand Build()
    {
        return new TreeListCommand(_depth);
    }
}