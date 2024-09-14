namespace Filesystem.Entities.TreeVisitors;

public interface IDirectory : IComponent
{
    string Name { get; }
    IEnumerable<IFile> NestedFiles { get; }
    IEnumerable<IDirectory> NestedDirectories { get; }
}