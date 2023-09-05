using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public User()
    {
        name = "Unknown";
        cash = 0;

        lastMessage = DateTime.Now;
    }

    public User(string name)
    {
        this.name = name;
        cash = 0;

        lastMessage = DateTime.Now;
    }

    public string name { get; set; }
    public double cash { get; set; }

    public DateTime lastMessage { get; set; }
}