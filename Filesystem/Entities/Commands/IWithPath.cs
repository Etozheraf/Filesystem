namespace Filesystem.Entities.Commands;

public interface IWithPath<T>
{
    public T WithPath(string path);
}