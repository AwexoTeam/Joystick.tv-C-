using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidityClient.Messages.UserPresence
{
    public class Message
    {
        public string id { get; set; }
        public string @event { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string channelId { get; set; }
        public DateTime created_at { get; set; }
    }

    public class UserPresence
    {
        public string identifier { get; set; }
        public Message message { get; set; }
    }

}
