namespace Filesystem.Entities.TreeVisitors;

#pragma warning disable CA1040
public interface IVisitor { }
#pragma warning restore CA1040

public interface IVisitor<T> : IVisitor
    where T : IComponent
{
    void Visit(T component);
}