using StupidityClient;
using StupidityClient.Messages.ChatMessage;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CommandManager : ManagerBase
{
    public static List<CommandBase> commands;

    public override void Initialize()
    {
        commands = new List<CommandBase>();

        var type = typeof(CommandBase);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

        foreach (var t in types)
        {
            CommandBase command = (CommandBase)Activator.CreateInstance(t);
            commands.Add(command);
            Debug.Log(LogLevel.Debug, "Registered Command " + command);
        }
    }

    public static void HandleCommand(ChatMessage msg)
    {
        bool isAdmin = Utils.isMessageFromAdmin(msg);

        string[] args = msg.text.Split(' ');

        string cmdName = args[0].Substring(1, args[0].Length - 1);
        args = args.Length <= 1 ? null : args.CloneRange(1, args.Length - 1);

        CommandBase cmd = commands.Find(x => x.Name.ToLower() == cmdName.ToLower());

        if (cmd == null)
            return;

        if (!cmd.isAdmin)
        {
            Debug.LogWithTime(LogLevel.Verbose, $"Executing: {cmd.Name} by {msg.author}");
            cmd.Execute(msg, args);
            return;
        }

        //We know now that the command is admin.
        //So if we arent admin then return.
        if (!isAdmin)
            return;

        Debug.LogWithBacktrack(LogLevel.Verbose, $"Executing: {cmd.Name} by {msg.author}");
        cmd.Execute(msg, args);
    }
}
