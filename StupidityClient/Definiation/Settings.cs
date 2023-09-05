using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Settings
{
    public static Settings settings { get { return DataManager.settings; } }

    public Settings()
    {
        currencyName = "$";
        prefix = "/";
        activeCashout = 5;
        cashoutDelay = 15;
        loglevel = LogLevel.Minimal;
    }

    public LogLevel loglevel { get; set; }
    public string channelId { get; set; }
    public string baseUri { get; set; }
    public string clientSecret { get; set; }
    public string clientId { get; set; }
    public string currencyName { get; set; }
    public string prefix { get; set; }
    public int cashoutDelay { get; set; }
    public double cashoutGoal { get; set; }
    public int tickDelayGoal { get; set; }
    public int activeDelay { get; set; }
    public int inactiveDelay { get; set; }

    public double activeCashout { get; set; }

    [JsonIgnore]
    public double inactiveCashout
    { 
        get 
        { 
            return Math.Ceiling(activeCashout/2f);
        }
    }

    [JsonIgnore]
    public double afkCashout
    {
        get
        {
            double floor = Math.Floor(inactiveCashout / 2f);
            return Math.Max(floor, 1);
        }
    }

}