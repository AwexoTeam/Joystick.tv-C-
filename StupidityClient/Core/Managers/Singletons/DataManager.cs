using Newtonsoft.Json;
using StupidityClient;
using StupidityClient.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DataManager
{
    public static Dictionary<Packets, IHandler> handlers;
    public static List<ManagerBase> managers;
    public static Settings settings;
    public static AlertSettings alertSettings;
    public static Redeemables redeemables;
    public static string chatMessageJson;

    public static void Initialize()
    {
        EventManager.OnTick += OnTick;
        EventManager.OnClosing += OnClosing;

        handlers = new Dictionary<Packets, IHandler>();
        settings = new Settings();
        alertSettings = new AlertSettings();
        redeemables = new Redeemables();

        chatMessageJson = File.ReadAllText("Info\\SendMessage.txt");

        RegisterManagers();
        RegisterHandlers();

        LoadSettings();
    }

    private static void RegisterManagers()
    {
        managers = Utils.GetAllAbstract<ManagerBase>();

        foreach (var manager in managers)
        {
            manager.Initialize();
            Debug.Log(LogLevel.Debug, "Registered Manager " + manager);
        }
    }

    private static void RegisterHandlers()
    {
        var type = typeof(IHandler);
        var types = Utils.GetAllInterface<IHandler>();
        foreach (var handler in types)
        {
            string output = $"Registered Handler {handler.GetType().Name} for [";

            for (int i = 0; i < handler.packets.Length; i++)
            {
                var p = handler.packets[i];
                handlers.Add(p, handler);
                output += $"{p.ToString()}";

                if (i < handler.packets.Length - 1)
                    output += ", ";
            }

            output += "]";
            Debug.Log(LogLevel.Debug, output);
        }
    }

    private static void LoadSettings()
    {
        settings = Load<Settings>("Settings.json", settings);
        alertSettings = Load<AlertSettings>("AlertSettings.json", alertSettings);
        redeemables = Load<Redeemables>("Redeemables.json", redeemables);
    }

    private static T Load<T>(string path, object def)
    {
        string json = "";
        if (!File.Exists(path))
        {
            json = JsonConvert.SerializeObject(def, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }

    private static void OnTick(DateTime lastTick)
    {
        if (!Bot.hasLoaded)
            return;

        managers.ForEach(x => x.Tick(lastTick));
        Save();
    }

    private static void OnClosing()
    {
        if (!Bot.hasLoaded)
            return;

        Save();
    }

    public static void Save()
    {
        if(Settings.settings == null)
            settings = new Settings();

        string settingjson = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText("Settings.json", settingjson);
    }
}