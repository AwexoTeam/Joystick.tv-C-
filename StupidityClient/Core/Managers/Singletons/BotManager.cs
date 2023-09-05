using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using StupidityClient;
using StupidityClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using System.IO;
using System.Runtime.InteropServices;

public static class BotManager
{
    public static WebSocket ws;
    
    public static void Initialize()
    {
        
        StartWebsocket();
    }

    private static void StartWebsocket()
    {
        if(Settings.settings == null)
        {
            DataManager.Save();
            Debug.LogError("Couldn't find settings!");
            return;
        }

        var plainText = $"{Settings.settings.clientId}:{Settings.settings.clientSecret}";
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        var key = System.Convert.ToBase64String(plainTextBytes);

        ws = new WebSocket(Settings.settings.baseUri + key);
        ws.MessageReceived += Ws_MessageReceived;
        ws.Closed += Ws_Closed;
        
        ws.Open();
    }

    private static void Ws_Closed(object sender, EventArgs e)
        => EventManager.InvokeEvent(EventType.OnBotDisconnected,null);

    public static void Send(string json)
    {
        json = json.Replace("\n", "").Replace("\r", "");
        ws.Send(json);
    }

    private static void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        string jsonStr = e.Message;
        var data = (JObject)JsonConvert.DeserializeObject(jsonStr);
        if (data == null)
        {
            Debug.LogError("Null: " + e.Message);
            return;
        }

        string address = "";
        if (data.ContainsKey("type"))
        {
            address = data.SelectToken("type").Value<string>();

        }
        else
        {
            if (!data.ContainsKey("message"))
                return;

            address = Packets.new_message.ToString();
        }

        address = address.ToLower();
        HandlePacket(address, e.Message);
    }

    public static void HandlePacket(string address, string e)
    {
        Packets packet = Packets.ping;
        if (!Enum.TryParse(address, true, out packet))
        {
            Debug.LogWarning(address + " is a new packet!");
            return;
        }

        if (!DataManager.handlers.ContainsKey(packet))
        {
            Debug.LogWarning("No handler for " + packet);
            return;
        }

        DataManager.handlers[packet].Handle(packet, e);
    }

    public static void SendMessage(string message, bool isWhisper = false)
    {
        if (message == string.Empty)
        {
            Debug.LogError("Message cannot be empty!");
            return;
        }

        string sendMessage = DataManager.chatMessageJson;
        sendMessage = sendMessage.Replace("{0}", message);
        sendMessage = sendMessage.Replace("{1}", Settings.settings.channelId);

        ws.Send(sendMessage);
    }

    public static void SendWhisper(string message, string username)
    {
        if (message == string.Empty)
        {
            Debug.LogError("Message cannot be empty!");
            return;
        }

        if (username == string.Empty)
        {
            Debug.LogError("Username cannot be empty!");
            return;
        }

        string sendMessage = DataManager.chatMessageJson;
        sendMessage = sendMessage.Replace("{0}", message);
        sendMessage = sendMessage.Replace("{1}", username);

        ws.Send(sendMessage);
    }
}