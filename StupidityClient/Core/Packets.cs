using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidityClient.Core
{
    public enum Packets
    {
        ping,
        welcome,
        confirm_subscription,
        reject_subscription,
        new_message,
        enter_stream,
        leave_stream,
        started,
        tipped,
        wheelspinclaimed,
        followed,
        deviceconnected,
        viewercountupdated

    }
}
