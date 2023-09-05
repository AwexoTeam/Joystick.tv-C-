using Newtonsoft.Json;
using StupidityClient;
using StupidityClient.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

public class EconemyManager : ManagerBase
{
    public static List<string> activeUser;
    private static BankDatabase bankDatabase;
    public static List<User> bank
    { 
        get { return bankDatabase.bank; } 
        set { bankDatabase.bank = value; }
    }

    private DateTime lastCashOut = DateTime.Now;

    public override void Initialize()
    {
        activeUser = new List<string>();
        bankDatabase = new BankDatabase();
        bankDatabase.bank = new List<User>();

        if (File.Exists("Bank.json"))
            bankDatabase = JsonConvert.DeserializeObject<BankDatabase>(File.ReadAllText("Bank.json"));
    }

    public override void Tick(DateTime lastTick)
    {
        var diff = (lastTick - lastCashOut).TotalSeconds;
        if (diff < DataManager.settings.cashoutDelay)
            return;

        foreach (var user in activeUser)
        {
            VerifyUser(user);
            User person = bank.Find(x => x.name == user);

            person.cash += GetCashout(person);
        }

        string json = JsonConvert.SerializeObject(bankDatabase, Formatting.Indented);
        File.WriteAllText("Bank.json", json);

        lastCashOut = DateTime.Now;
    }

    public static void VerifyUser(string username)
    {
        User person = bank.Find(x => x.name == username);
        if (person == null)
            bank.Add(new User(username));
    }

    public static double GetCashout(User user)
    {
        var span = DateTime.Now - user.lastMessage;

        if (span.TotalSeconds < Settings.settings.activeDelay)
            return Settings.settings.activeCashout;

        if(span.TotalSeconds < Settings.settings.inactiveDelay)
            return Settings.settings.inactiveCashout;

        return Settings.settings.afkCashout;
    }

    public static User GetUserByName(string username)
        => bank.Find(x => x.name == username);
}