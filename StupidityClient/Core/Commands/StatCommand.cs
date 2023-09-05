using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatCommand : CommandBase
{
    public override string Name => "stat";

    public override void Execute(ChatMessage msg, string[] args)
    {
        EconemyManager.VerifyUser(msg.author);
        User user = EconemyManager.GetUserByName(msg.author);
        double cashout = EconemyManager.GetCashout(user);
        string currency = Settings.settings.currencyName;

        var span = DateTime.Now - user.lastMessage;
        var secs = span.TotalSeconds;
        secs = Math.Floor(secs);

        string message = $"{secs} secs since last chat message currently getting {cashout}{currency}.";

        BotManager.SendMessage(message);
    }

    public override string GetHelp(ChatMessage msg, string[] args)
        => $"Shows how long since the user last has spoken and how many {Settings.settings.currencyName} they get pr cashout.";
}