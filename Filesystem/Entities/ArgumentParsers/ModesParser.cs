using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities.ArgumentParsers;

public class ModesParser<T> : ArgumentParserBase<T>
    where T : IBuilder
{
    private readonly DeclarationModeParser<T> _nextDeclarationModeParser;

    public ModesParser(DeclarationModeParser<T> nextDeclarationModeParser)
    {
        _nextDeclarationModeParser = nextDeclarationModeParser;
    }

    public override ParseResult Parse(T builder, IEnumerator<string> argument)
    {
        if (string.IsNullOrEmpty(argument.Current))
            return new ParseResult.Success(builder);

        ParseResult parseResult = _nextDeclarationModeParser.Parse(builder, argument);
        if (parseResult is ParseResult.Fault)
        {
            return parseResult;
        }

        while (argument.MoveNext())
        {
            parseResult = _nextDeclarationModeParser.Parse(builder, argument);
            if (parseResult is ParseResult.Fault)
            {
                return parseResult;
            }
        }

        return new ParseResult.Success(builder);
    }
}