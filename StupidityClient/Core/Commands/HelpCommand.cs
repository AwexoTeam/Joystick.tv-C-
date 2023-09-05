using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HelpCommand : CommandBase
{
    public override string Name => "help";

    public override void Execute(ChatMessage msg, string[] args)
    {
        if (args == null || args.Length <= 0)
        {
            ShowHelp(msg, args);
            return;
        }

        string cmd = args[0].ToLower();
        if(cmd == "commands")
        {
            ShowAllCommands(msg, args);
            return;
        }

        var command = CommandManager.commands.Find(x => x.Name == cmd);
        if(command == null)
        {
            BotManager.SendMessage($"{cmd} is not a valid command and hence cannot get help! Use '/help commands' to see all available commands!");

            return;
        }

        BotManager.SendMessage(command.GetHelp(msg, args));
    }
    
    public void ShowHelp(ChatMessage msg, string[] args)
    {
        string help = GetHelp(msg, args);
        BotManager.SendMessage(help);
    }


    public override string GetHelp(ChatMessage msg, string[] args)
    {
        string help = "";
        help += "To get started you use '/balance' to see your balance.";
        help += "You can use '/redeem' to view redeem list for the current stream. ";
        help += "If you wish to redeem a spank you would do '/redeem spank'.";
        help += "If theres a command you're unsure of how to use you can use '/help name' and replace name with the name of the command so for example '/help redeem' would show a help for redeem command.";

        help += "You gain more points the more active in chat you are you can see your current stats with '/stat' .";

        return help;
    }

    public void ShowAllCommands(ChatMessage msg, string[] args)
    {
        List<CommandBase> availableCommands = new List<CommandBase>();
        availableCommands = CommandManager.commands.FindAll(x => !x.isAdmin).ToList();
        if (Utils.isMessageFromAdmin(msg))
            availableCommands.AddRange(CommandManager.commands.FindAll(x => x.isAdmin));

        string allCommands = "";
        availableCommands.ForEach(x => allCommands += $"{x.Name}, ");

        BotManager.SendMessage(allCommands);
    }
}