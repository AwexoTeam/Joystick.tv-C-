using StupidityClient;
using StupidityClient.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StateHandler : IHandler
{
    public Packets[] packets => new Packets[]
    { 
        Packets.welcome,
        Packets.reject_subscription,
        Packets.confirm_subscription
    };

    public StateHandler()
    {
        EventManager.OnBotDisconnected += OnDistconnection;
        EventManager.OnBotStart += OnBotStart;
    }


    public void Handle(Packets packet, string message)
    {
        switch (packet)
        {
            case Packets.welcome:
                HandleConnection();
                break;

            case Packets.reject_subscription:
                string msg = "Reject Subscription";
                EventManager.InvokeEvent(EventType.OnError, msg);
                break;

            case Packets.confirm_subscription:
                EventManager.InvokeEvent(EventType.OnBotStart, null);
                break;
        }
    }

    public void HandleConnection()
    {
        Debug.Log("Bot connected!");
        Debug.Log("Subscribing...");

        string msg = File.ReadAllText("Info\\SubscribeMessage.txt");
        BotManager.Send(msg);
    }

    private void OnBotStart()
    {
        Debug.Log("Sucessfully subscribed!");
        Bot.shouldTick = true;
    }

    private static void OnDistconnection()
        => Debug.Log("Disconnected ): ");

}