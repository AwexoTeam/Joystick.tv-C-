using StupidityClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHandler
{
    Packets[] packets { get; }
    void Handle(Packets packet, string message);
}
