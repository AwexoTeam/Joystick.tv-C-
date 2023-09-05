using StupidityClient.Messages.ChatMessage;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class CommandBase
{
    
    public abstract string Name { get; }
    public abstract void Execute(ChatMessage msg, string[] args);

    public abstract string GetHelp(ChatMessage msg, string[] args);

    public virtual bool isAdmin { get { return false; } }
}
