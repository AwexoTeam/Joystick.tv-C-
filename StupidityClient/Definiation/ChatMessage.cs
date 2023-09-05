using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidityClient.Messages.ChatMessage
{
    public class Author
    {
        public string slug { get; set; }
        public string username { get; set; }
        public object usernameColor { get; set; }
        public string displayNameWithFlair { get; set; }
        public string signedPhotoUrl { get; set; }
        public string signedPhotoThumbUrl { get; set; }
        public bool isStreamer { get; set; }
        public bool isModerator { get; set; }
        public bool isSubscriber { get; set; }
    }

    public class Message
    {
        public string @event { get; set; }
        public DateTime createdAt { get; set; }
        public string messageId { get; set; }
        public string type { get; set; }
        public string visibility { get; set; }
        public string text { get; set; }
        public string botCommand { get; set; }
        public string botCommandArg { get; set; }
        public List<object> emotesUsed { get; set; }
        public Author author { get; set; }
        public Streamer streamer { get; set; }
        public string channelId { get; set; }
        public bool mention { get; set; }
        public object mentionedUsername { get; set; }
    }

    public class ChatMessage
    {
        public string identifier { get; set; }
        public Message message { get; set; }

        public string author
        {
            get
            {
                if (message == null)
                    return "Unknown";

                if (message.author == null)
                    return "Unknown";

                return message.author.username;
            }
        }
        public string text { get { return message.text; } }
    }

    public class Streamer
    {
        public string slug { get; set; }
        public string username { get; set; }
        public object usernameColor { get; set; }
        public string signedPhotoUrl { get; set; }
        public string signedPhotoThumbUrl { get; set; }
    }

}
