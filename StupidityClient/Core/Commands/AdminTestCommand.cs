using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AdminTestCommand : CommandBase
{
    public override string Name => "admintest";

    public override bool isAdmin => true;

    public override void Execute(ChatMessage msg, string[] args)
    {
        Debug.Log(msg.message.channelId);
    }


    public override string GetHelp(ChatMessage msg, string[] args)
        => "secret dont use probably.";
}