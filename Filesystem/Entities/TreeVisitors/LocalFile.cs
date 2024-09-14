namespace Filesystem.Entities.TreeVisitors;

public class LocalFile : IFile
{
    private readonly FileInfo _fileInfo;

    public LocalFile(string path)
    {
        _fileInfo = new FileInfo(path);
    }

    public string Name => _fileInfo.Name;

    public void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<IFile> visitorDirectory)
        {
            visitorDirectory.Visit(this);
        }
    }
}