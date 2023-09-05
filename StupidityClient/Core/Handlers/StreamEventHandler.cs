using StupidityClient.Core;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StreamEventData
{
    public string message;
    public Packets packet;

    public StreamEventData(string message, Packets packet)
    {
        this.message = message;
        this.packet = packet;
    }
}

public class StreamEventHandler : IHandler
{
    public Packets[] packets => new Packets[]
    {
        Packets.tipped,
        Packets.followed,
        Packets.wheelspinclaimed,
    };

    public void Handle(Packets packet, string message)
    {
        StreamEventData data = new StreamEventData(message, packet);
        EventManager.InvokeEvent(EventType.OnStreamEvent, data);

        switch (packet)
        {
            case Packets.tipped:
                EventManager.InvokeEvent(EventType.OnTipped, data);
                break;
            case Packets.wheelspinclaimed:
                EventManager.InvokeEvent(EventType.OnWheelSpinClaimed, data);
                break;
            case Packets.followed:
                EventManager.InvokeEvent(EventType.OnFollowed, data);
                break;
        }
    }
}