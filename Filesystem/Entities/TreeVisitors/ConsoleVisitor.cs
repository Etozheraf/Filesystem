namespace Filesystem.Entities.TreeVisitors;

public class ConsoleVisitor : IVisitor<IFile>, IVisitor<IDirectory>
{
    private readonly int _maxDepth;
    private readonly Dictionary<string, string> _dictionaryEmoji;

    private string _offset;

    public ConsoleVisitor(int depth = 1)
    {
        _maxDepth = depth * 4;
        _offset = string.Empty;
        _dictionaryEmoji = new Dictionary<string, string>()
        {
            { "file", "📄" },
            { "directory", "📁" },
            { "offset1", "    " },
            { "offset2", "│   " },
            { "offset3", "├───" },
            { "offset4", "└───" },
        };
    }

    public ConsoleVisitor(Dictionary<string, string> dictionaryEmoji, int depth = 1)
    {
        _maxDepth = depth * 4;
        _offset = string.Empty;
        _dictionaryEmoji = dictionaryEmoji;
    }

    public void Visit(IFile component)
    {
        Console.WriteLine(_offset + _dictionaryEmoji["file"] + component.Name);
    }

    public void Visit(IDirectory component)
    {
        Console.WriteLine(_offset + _dictionaryEmoji["directory"] + component.Name);

        ChangeOffset(_dictionaryEmoji["offset3"], _dictionaryEmoji["offset2"]);

        ChangeOffset(_dictionaryEmoji["offset4"], _dictionaryEmoji["offset1"]);

        if (_offset.Length + 4 > _maxDepth) return;

        if (!component.NestedDirectories.Any())
        {
            _offset += _dictionaryEmoji["offset1"];
        }
        else
        {
            _offset += _dictionaryEmoji["offset2"];
        }

        foreach (IFile nestedComponents in component.NestedFiles)
        {
            nestedComponents.Accept(this);
        }

        if (component.NestedFiles.Any())
        {
            Console.WriteLine(_offset);
        }

        IDirectory[] directories = component.NestedDirectories.ToArray();
        for (int i = 0; i < directories.Length - 1; ++i)
        {
            _offset = _offset[..^4] + _dictionaryEmoji["offset3"];
            directories[i].Accept(this);
        }

        _offset = _offset[..^4] + _dictionaryEmoji["offset4"];

        if (directories.Length > 0)
        {
            directories[^1].Accept(this);
        }

        _offset = _offset[..^4];
    }

    private void ChangeOffset(string oldOffset, string newOffset)
    {
        if (_offset.Length >= 4 && _offset[^4..] == oldOffset)
        {
            _offset = _offset[..^4] + newOffset;
        }
    }
}