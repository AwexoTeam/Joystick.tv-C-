using Newtonsoft.Json;
using StupidityClient.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Redeemables
{
    public Redeemables()
    {
        _redeemables = new Dictionary<string, int>();
        _tempRedeems = new Dictionary<string, int>();

    }

    public static Dictionary<string, int> redeemables
    { 
        get { return DataManager.redeemables._redeemables; } 
        set { DataManager.redeemables._redeemables = value; } 
    }
    public static Dictionary<string, int> tempRedeems
    {
        get { return DataManager.redeemables._tempRedeems; }
        set { DataManager.redeemables._tempRedeems = value; }
    }

    public Dictionary<string, int> _redeemables { get; set; }

    [JsonIgnore]
    public Dictionary<string, int> _tempRedeems { get; set; }

    public static void Save()
    {
        string json = JsonConvert.SerializeObject(DataManager.redeemables, Formatting.Indented);
        File.WriteAllText("Redeemables.json", json);
    }
}