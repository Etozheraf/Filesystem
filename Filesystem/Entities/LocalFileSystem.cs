using Filesystem.Entities.GetEmoji;
using Filesystem.Entities.TreeVisitors;
using Filesystem.Models;

namespace Filesystem.Entities;

public class LocalFileSystem : IFileSystem
{
    private readonly string _connectedPath;

    private string _actualPath;

    public LocalFileSystem(string rootPath)
    {
        _connectedPath = rootPath;
        _actualPath = rootPath;
    }

    public ExecutionResult Copy(string source, string destination)
    {
        return BinaryFileCommand(source, destination, File.Copy);
    }

    public ExecutionResult Delete(string source)
    {
        return UnaryFileCommand(source, File.Delete);
    }

    public ExecutionResult Move(string source, string destination)
    {
        return BinaryFileCommand(source, destination, File.Move);
    }

    public ExecutionResult Rename(string source, string name)
    {
        string? sourcePath = CreateNewPath(source);

        if (sourcePath is null)
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        string fileName = Path.GetFileName(sourcePath);
        string destinationPath = sourcePath.Remove(sourcePath.Length - fileName.Length) + name;

        try
        {
            File.Move(sourcePath, destinationPath);
        }
#pragma warning disable CA1031
        catch
#pragma warning restore CA1031
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        return new ExecutionResult.Success();
    }

    public ExecutionResult Show(string source)
    {
        string? sourcePath = CreateNewPath(source);

        if (sourcePath is null)
        {
            return new ExecutionResult.Fault("Wrong path");
        }

        string text;
        try
        {
            text = File.ReadAllText(sourcePath);
        }
#pragma warning disable CA1031
        catch (Exception)
#pragma warning restore CA1031
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        return new ExecutionResult.SuccessShow(text);
    }

    public ExecutionResult TreeGoto(string destination)
    {
        string oldPath = _actualPath;

        ExecutionResult result = UnaryFileCommand(
            destination,
            destinationPath => _actualPath = destinationPath);

        if (Path.Exists(_actualPath)) return result;

        _actualPath = oldPath;
        return new ExecutionResult.Fault("Path doesn't exist");
    }

    public ExecutionResult TreeList()
    {
        return new ExecutionResult.SuccessTreeList(
            new LocalDirectory(_actualPath),
            new GetFromConfigEmoji().GetEmoji());
    }

    private static string? TryCombine(string? oldPath, string? newPath)
    {
        if (oldPath is null || newPath is null)
        {
            return null;
        }

        try
        {
            return Path.Combine(oldPath, newPath);
        }
#pragma warning disable CA1031
        catch
#pragma warning restore CA1031
        {
            return null;
        }
    }

    private string? CreateNewPath(string source)
    {
        string updatePath;

        if (source.StartsWith('`') && source.EndsWith('`'))
        {
            source = source.Trim('`');
            updatePath = _connectedPath;
        }
        else
        {
            updatePath = _actualPath;
        }

        string? resultPath = TryCombine(updatePath, source);

        if (resultPath is null)
        {
            return null;
        }

        resultPath = Path.GetFullPath(resultPath);

        if (resultPath.Length < _connectedPath.Length
            || resultPath.Substring(0, _connectedPath.Length) != _connectedPath)
        {
            return null;
        }

        return resultPath;
    }

    private ExecutionResult UnaryFileCommand(string source, Action<string> fileCommand)
    {
        string? sourcePath = CreateNewPath(source);

        if (sourcePath is null)
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        if (!Path.Exists(sourcePath))
        {
            return new ExecutionResult.Fault("File doesn't exist");
        }

        try
        {
            fileCommand(sourcePath);
        }
#pragma warning disable CA1031
        catch
#pragma warning restore CA1031
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        return new ExecutionResult.Success();
    }

    private ExecutionResult BinaryFileCommand(
        string source,
        string destination,
        Action<string, string> fileCommand)
    {
        string? sourcePath = CreateNewPath(source);
        string? destinationPath = CreateNewPath(destination);

        if (sourcePath is null || destinationPath is null)
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        string fileName = Path.GetFileName(sourcePath);
        string destinationPathWithName = Path.Combine(destinationPath, fileName);
        destinationPath = destinationPathWithName;
        int count = 1;
        while (Path.Exists(destinationPath))
        {
            destinationPath = destinationPathWithName + $"({count})";
            ++count;
        }

        try
        {
            fileCommand(sourcePath, destinationPath);
        }
#pragma warning disable CA1031
        catch
#pragma warning restore CA1031
        {
            return new ExecutionResult.Fault("Invalid path");
        }

        return new ExecutionResult.Success();
    }
}