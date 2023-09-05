using StupidityClient;
using StupidityClient.Core.Handlers;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BalanceCommand : CommandBase
{
    public override string Name => "balance";

    public override void Execute(ChatMessage msg, string[] args)
    {
        EconemyManager.VerifyUser(msg.author);
        double balance = EconemyManager.GetUserByName(msg.author).cash;

        string response = $"{msg.author} you have {balance}{DataManager.settings.currencyName} available!";
        BotManager.SendMessage(response);
    }


    public override string GetHelp(ChatMessage msg, string[] args)
        => "Shows the user's balance.";
}