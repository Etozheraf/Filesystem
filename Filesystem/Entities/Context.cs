using Filesystem.Entities.CommandParsers;
using Filesystem.Entities.Commands;
using Filesystem.Models;

namespace Filesystem.Entities;

public class Context
{
    private readonly ICommandParser _commandParser;

    private IFileSystem? _fileSystem;

    public Context(ICommandParser commandParser)
    {
        _commandParser = commandParser;
        _fileSystem = null;
    }

    public ExecutionResult Execute(string commandText)
    {
        ParseResult parseResult = _commandParser.Parse(commandText);
        if (parseResult is ParseResult.Fault parseFault)
        {
            return new ExecutionResult.Fault(parseFault.Message);
        }

        if (parseResult is not ParseResult.Success success)
        {
            return new ExecutionResult.Success();
        }

        IBuilder builder = success.Builder;

        ICommand command;
        try
        {
            command = builder.Build();
        }
        catch (ArgumentNullException)
        {
            return new ExecutionResult.Fault("Doesn't enough arguments");
        }

        ExecutionResult executionResult = command.Execute(_fileSystem);

        if (executionResult is ExecutionResult.Fault executionFault)
        {
            return new ExecutionResult.Fault(executionFault.Message);
        }

        if (executionResult is not ExecutionResult.NewFileSystem newFileSystem)
        {
            return new ExecutionResult.Success();
        }

        _fileSystem = newFileSystem.FileSystem;
        return new ExecutionResult.Success();
    }

    public void SetFileSystem(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
}