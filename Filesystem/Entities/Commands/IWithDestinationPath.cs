namespace Filesystem.Entities.Commands;

public interface IWithDestinationPath<T>
{
    public T WithDestinationPath(string path);
}