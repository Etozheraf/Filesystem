namespace Filesystem.Entities.TreeVisitors;

public interface IComponent
{
    void Accept(IVisitor visitor);
}