using Newtonsoft.Json;
using StupidityClient;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TickDelayCommand : CommandBase
{
    public override string Name => "tickdelay";

    public override bool isAdmin => true;
    public override void Execute(ChatMessage msg, string[] args)
    {
        if (args == null)
        {
            BotManager.SendMessage("You need atleast 1 number!");
            return;
        }

        int delay = 0;
        if (!int.TryParse(args[0], out delay))
        {
            BotManager.SendMessage(args[0] +" is not a valid number!");
            return;
        }

        DataManager.settings.cashoutDelay = delay;
        BotManager.SendMessage($"Cashouts happen each {delay} second(s)!");

        string json = JsonConvert.SerializeObject(DataManager.settings);
        File.WriteAllText("Settings.json", json);
    }

    public override string GetHelp(ChatMessage msg, string[] args)
        => "Changes how long in miliseconds before the internal checks happens. Do try keep this above 100 ms to reduce lagspikes! The internal tick is what saves and does all checks for the different modules!";
}