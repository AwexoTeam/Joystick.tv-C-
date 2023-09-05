using StupidityClient;
using StupidityClient.Core.Handlers;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RedeemCommand : CommandBase
{
    public override string Name => "redeem";
    
    public override void Execute(ChatMessage msg, string[] args)
    {
        if(args == null)
        {
            ShowList(msg);
            return;
        }

        RedeemItem(msg, args);
    }

    private void ShowList(ChatMessage msg)
    {
        string list = "";

        
        if (Redeemables.redeemables.Count <= 0 && Redeemables.tempRedeems.Count <= 0)
        {
            BotManager.SendMessage("No redeems currently available!");
            return;
        }

        var items = GetAllRedeemables();
        
        foreach (var item in items)
        {
            list += $"{item.Key}:{item.Value}, ";
        }

        BotManager.SendMessage(list);
    }

    private void RedeemItem(ChatMessage msg, string[] args)
    {
        string redeem = "";
        Array.ForEach(args, x => redeem += x + " ");
        redeem = redeem.Remove(redeem.Length - 1, 1);

        var items = GetAllRedeemables();

        if (!items.ContainsKey(redeem))
        {
            BotManager.SendMessage($"Sorry {msg.author}, {redeem} is not valid redeemable. Use /redeem without any arguements to see list!");
            return;
        }

        int cost = items[redeem];

        EconemyManager.VerifyUser(msg.author);
        double balance = EconemyManager.GetUserByName(msg.author).cash;

        if (balance < cost)
        {
            BotManager.SendMessage($"Sorry {msg.author}, you cant afford a {redeem}-redeem!");
            return;
        }

        EconemyManager.GetUserByName(msg.author).cash -= cost;
        BotManager.SendMessage($"{msg.author} has redeemed {redeem}");
        Bot.alerts.Enqueue(new Alert(AlertType.Redeem, msg.author, redeem));
    }

    private Dictionary<string, int> GetAllRedeemables()
    {
        Dictionary<string, int> items = new Dictionary<string, int>();
        foreach (var item in Redeemables.redeemables)
            items.Add(item.Key, item.Value);

        foreach (var item in Redeemables.tempRedeems)
            items.Add(item.Key, item.Value);

        return items;
    }

    public override string GetHelp(ChatMessage msg, string[] args)
        => "Without arguements this shows the list. With an arguement it tries to redeem that item. TODO";
}