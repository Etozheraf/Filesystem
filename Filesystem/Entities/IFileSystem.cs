using Filesystem.Models;

namespace Filesystem.Entities;

public interface IFileSystem
    : ICopy,
        IDelete,
        IMove,
        IRename,
        IShow,
        ITreeGoto,
        ITreeList
{
}

public interface ICopy
{
    ExecutionResult Copy(string source, string destination);
}

public interface IDelete
{
    ExecutionResult Delete(string source);
}

public interface IMove
{
    ExecutionResult Move(string source, string destination);
}

public interface IRename
{
    ExecutionResult Rename(string source, string name);
}

public interface IShow
{
    ExecutionResult Show(string source);
}

public interface ITreeGoto
{
    ExecutionResult TreeGoto(string destination);
}

public interface ITreeList
{
    ExecutionResult TreeList();
}