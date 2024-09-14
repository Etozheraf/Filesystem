namespace Filesystem.Entities.TreeVisitors;

public class LocalDirectory : IDirectory
{
    private readonly DirectoryInfo _directoryInfo;
    private List<IFile>? _nestedFiles;
    private List<IDirectory>? _nestedDirectories;

    public LocalDirectory(string path)
    {
        _directoryInfo = new DirectoryInfo(path);
    }

    public string Name => _directoryInfo.Name;

    public IEnumerable<IFile> NestedFiles
    {
        get
        {
            if (_nestedFiles is not null)
            {
                return _nestedFiles;
            }

            _nestedFiles = new List<IFile>();

            foreach (FileInfo file in _directoryInfo.GetFiles())
            {
                _nestedFiles.Add(new LocalFile(file.FullName));
            }

            return _nestedFiles;
        }
    }

    public IEnumerable<IDirectory> NestedDirectories
    {
        get
        {
            if (_nestedDirectories is not null)
            {
                return _nestedDirectories;
            }

            _nestedDirectories = new List<IDirectory>();

            foreach (DirectoryInfo directory in _directoryInfo.GetDirectories())
            {
                _nestedDirectories.Add(new LocalDirectory(directory.FullName));
            }

            return _nestedDirectories;
        }
    }

    public void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<IDirectory> visitorDirectory)
        {
            visitorDirectory.Visit(this);
        }
    }
}