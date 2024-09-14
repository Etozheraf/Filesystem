using Filesystem.Entities.Commands.Connect;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class LocalFileSystemParser : ArgumentParserBase<ConnectCommandBuilder>
{
    public override ParseResult Parse(ConnectCommandBuilder builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Fault("Missing mod value");

        if (argument.Current != "local")
        {
            return Next is not null
                ? Next.Parse(builder, argument)
                : new ParseResult.Fault("Not implemented mode");
        }

        argument.MoveNext();
        return new ParseResult.Success(builder.WithFileSystemMode(new LocalConnectFactory()));
    }
}