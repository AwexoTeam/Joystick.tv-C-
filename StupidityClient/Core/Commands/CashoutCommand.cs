using StupidityClient.Messages.ChatMessage;
using StupidityClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

public class CashoutCommand : CommandBase
{
    public override string Name => "cashout";

    public override bool isAdmin => true;
    public override void Execute(ChatMessage msg, string[] args)
    {
        if (args == null)
        {
            BotManager.SendMessage("You need atleast 1 number!");
            return;
        }

        double cashout = 0;
        if (!double.TryParse(args[0], out cashout))
        {
            BotManager.SendMessage(args[0] + " is not a valid number!");
            return;
        }

        DataManager.settings.activeCashout = cashout;
        BotManager.SendMessage($"Cashouts are now {cashout}{DataManager.settings.currencyName} pr tick!");

        string json = JsonConvert.SerializeObject(DataManager.settings);
        File.WriteAllText("Settings.json", json);
    }

    public override string GetHelp(ChatMessage msg, string[] args)
        => "Changes the base cashout value needs a number as an arugement.";
}