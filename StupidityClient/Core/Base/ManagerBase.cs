using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ManagerBase
{
    public abstract void Initialize();
    public virtual void Close() { }
    public virtual void Tick(DateTime lastTick) { }
}