using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StupidityClient;
using StupidityClient.Core;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class PingHandler : IHandler
{
    public Packets[] packets => new Packets[] { Packets.ping };

    public void Handle(Packets packet, string message)
    {
        DateTime stamp = DateTime.Now;

        var data = (JObject)JsonConvert.DeserializeObject(message);
        string unix = data.SelectToken("message").Value<string>();
        double seconds = 0;
        double.TryParse(unix, out seconds);

        DateTime s = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        s = s.AddSeconds(seconds).ToLocalTime();


        Bot.lastPing = $"Last Ping - {s.Hour}:{s.Minute}:{s.Second}" ;
    }
}