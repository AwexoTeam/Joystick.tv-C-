using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum AlertType
{
    None,
    Redeem,
    Followed,
    Tipped,
    Subscribed,
}

public class Alert
{
    public AlertType type;
    public string username;
    public string otherInfo;

    public Alert(AlertType type, string username, string otherInfo)
    {
        this.type = type;
        this.username = username;
        this.otherInfo = otherInfo;
    }
}