namespace Filesystem.Entities.TreeVisitors;

public interface IFile : IComponent
{
    string Name { get; }
}