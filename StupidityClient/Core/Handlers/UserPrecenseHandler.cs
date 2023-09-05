using Newtonsoft.Json;
using StupidityClient.Messages.UserPresence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidityClient.Core.Handlers
{
    public class UserPrecenseHandler : IHandler
    {
        public Packets[] packets => new Packets[]
        { 
            Packets.enter_stream,
            Packets.leave_stream ,
            Packets.viewercountupdated
        };

        public void Handle(Packets packet, string message)
        {
            UserPresence data = null;
            if(packet != Packets.viewercountupdated)
                data = JsonConvert.DeserializeObject<UserPresence>(message);

            switch (packet)
            {
                case Packets.enter_stream:
                    OnEnter(data);
                    break;
                case Packets.leave_stream:
                    OnLeave(data);
                    break;
                case Packets.viewercountupdated:
                    break;
                default:
                    break;
            }
        }

        private void OnEnter(UserPresence data)
        {
            string username = data.message.text;
            EconemyManager.VerifyUser(username);
            EconemyManager.activeUser.Add(data.message.text);
        }

        private void OnLeave(UserPresence data)
        {
            EconemyManager.activeUser.Remove(data.message.text);
        }
    }
}
