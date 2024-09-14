using System.Text;
using Filesystem.Entities;
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

namespace Filesystem;

public static class Program
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
            "delete",
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

    private static readonly Context Context = new Context(
        FileCommands
            .AddNextParser(TreeCommands)
            .AddNextParser(Connect)
            .AddNextParser(Disconnect));

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            string? command = Console.ReadLine();
            if (command is null || command == "\\q!")
            {
                return;
            }

            ExecutionResult result = Context.Execute(command);
            if (result is ExecutionResult.Fault fault)
            {
                Console.WriteLine(fault.Message);
            }
        }
    }
}
