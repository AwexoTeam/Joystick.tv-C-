using Newtonsoft.Json;
using StupidityClient;
using StupidityClient.Core;
using StupidityClient.Core.Handlers;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChatMessageHandler : IHandler
{
    public Packets[] packets => new Packets[] { Packets.new_message };

    public void Handle(Packets packet, string message)
    {
        ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(message);
        char prefix = DataManager.settings.prefix[0];

        if (!EconemyManager.activeUser.Contains(msg.author))
            EconemyManager.activeUser.Add(msg.author);

        if (msg.text[0] == prefix)
        {
            CommandManager.HandleCommand(msg);
            return;
        }

        if (msg.text[0] != prefix && msg.message.type != "new_message")
        {
            BotManager.HandlePacket(msg.message.type, message);
            return;
        }

        User user = EconemyManager.GetUserByName(msg.author);
        user.lastMessage = DateTime.Now;
    }
}