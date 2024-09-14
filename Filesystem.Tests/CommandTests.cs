using Filesystem.Entities.ArgumentParsers;
using Filesystem.Entities.CommandParsers;
using Filesystem.Entities.Commands.Connect;
using Filesystem.Entities.Commands.Disconnect;
using Filesystem.Entities.Commands.FileCopy;
using Filesystem.Entities.Commands.FileDelete;
using Filesystem.Entities.Commands.FileMove;
using Filesystem.Entities.Commands.FileRename;
using Filesystem.Entities.Commands.FileShow;
using Filesystem.Entities.Commands.TreeGoto;
using Filesystem.Entities.Commands.TreeList;
using Filesystem.Models;
using Xunit;

namespace Filesystem.tests;

public class CommandTests
{
    private static readonly CommandParser<ConnectCommandBuilder> Connect =
        new(
            "connect",
            new PathParser<ConnectCommandBuilder>().AddNext(
                new ModesParser<ConnectCommandBuilder>(
                    new DeclarationModeParser<ConnectCommandBuilder>(
                        "m",
                        new LocalFileSystemParser()))));

    private static readonly CommandParser<DisconnectCommandBuilder> Disconnect =
        new("disconnect");

    private static readonly CommandParser<TreeGotoCommandBuilder> TreeGoto =
        new(
            "goto",
            new PathParser<TreeGotoCommandBuilder>());

    private static readonly CommandParser<TreeListCommandBuilder> TreeList =
        new(
            "list",
            new ModesParser<TreeListCommandBuilder>(
                new DeclarationModeParser<TreeListCommandBuilder>(
                    "d",
                    new DepthModeParser())));

    private static readonly CommandParser<FileShowCommandBuilder> FileShow =
        new(
            "show",
            new PathParser<FileShowCommandBuilder>()
                .AddNext(new ModesParser<FileShowCommandBuilder>(
                    new DeclarationModeParser<FileShowCommandBuilder>(
                        "m",
                        new ConsoleShowModeParser()))));

    private static readonly CommandParser<FileMoveCommandBuilder> FileMove =
        new(
            "move",
            new PathParser<FileMoveCommandBuilder>().AddNext(
                new DestinationPathParser<FileMoveCommandBuilder>()));

    private static readonly CommandParser<FileCopyCommandBuilder> FileCopy =
        new(
            "copy",
            new PathParser<FileCopyCommandBuilder>().AddNext(
                new DestinationPathParser<FileCopyCommandBuilder>()));

    private static readonly CommandParser<FileDeleteCommandBuilder> FileDelete =
        new(
            "copy",
            new PathParser<FileDeleteCommandBuilder>());

    private static readonly CommandParser<FileRenameCommandBuilder> FileRename =
        new(
            "rename",
            new PathParser<FileRenameCommandBuilder>()
                .AddNext(new NameParser()));

    private static readonly TwoWordsCommandParser FileCommands =
        new(
            "file",
            FileRename
                .AddNextParser(FileCopy
                    .AddNextParser(FileMove
                        .AddNextParser(FileDelete
                            .AddNextParser(FileShow)))));

    private static readonly TwoWordsCommandParser TreeCommands =
        new(
            "tree",
            TreeGoto.AddNextParser(TreeList));

    private static readonly ICommandParser Parser =
        FileCommands
            .AddNextParser(TreeCommands)
            .AddNextParser(Connect)
            .AddNextParser(Disconnect);

    [Theory]
    [InlineData("connect D:/rafae -m local", true)]
    [InlineData("conect D:/rafae -m local", false)]
    public void ConnectVoidTest(string command, bool success)
    {
        ParseResult result;

        result = Parser.Parse(command);

        if (success)
        {
            Assert.IsType<ParseResult.Success>(result);
        }
        else
        {
            Assert.IsType<ParseResult.Fault>(result);
        }
    }

    [Theory]
    [InlineData("disconect", "There is no such command.")]
    public void DisconnectVoidTest(string command, string result)
    {
        ParseResult parseResult;

        parseResult = Parser.Parse(command);

        if (parseResult is ParseResult.Fault fault)
        {
            Assert.Equal(fault.Message, result);
        }

        Assert.IsType<ParseResult.Fault>(parseResult);
    }
}