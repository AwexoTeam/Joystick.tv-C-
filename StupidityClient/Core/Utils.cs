using Newtonsoft.Json;
using StupidityClient.Messages.ChatMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Utils
{
    public static List<T> GetAllAbstract<T>()
    {
        var type = typeof(T);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract)
            .ToList();

        List<T> rtn = new List<T>();
        types.ForEach(x => rtn.Add((T)Activator.CreateInstance(x)));

        return rtn;
    }
    public static List<T> GetAllInterface<T>()
    {
        var type = typeof(T);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
            .ToList();


        List<T> rtn = new List<T>();
        types.ForEach(x => rtn.Add((T)Activator.CreateInstance(x)));

        return rtn;
    }
    public static T DeserializeJson<T>(string path, ref object defaultobj)
    {
        if (!File.Exists(path))
        {
            string text = JsonConvert.SerializeObject(defaultobj);
            File.WriteAllText(path, text);
        }

        return JsonConvert.DeserializeObject<T>(path);
    }

    public static bool isMessageFromAdmin(ChatMessage msg)
        => msg.author == "ZanajSuki";
}