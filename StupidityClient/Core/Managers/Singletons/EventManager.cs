using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EventType
{
    OnTick,
    OnError,
    OnPreload,
    OnClosing,
    OnBotStart,
    OnBotDisconnected,
    OnStreamEvent,
    OnTipped,
    OnFollowed,
    OnWheelSpinClaimed,
    OnSubscribed,
}

public static class EventManager
{
    public delegate void _OnTick(DateTime lastTick);
    public delegate void _OnError(string msg);
    public delegate void _OnClosing();
    public delegate void _OnPreload();
    public delegate void _OnBotStart();
    public delegate void _OnBotDisconnected();
    public delegate void _OnStreamEvent(StreamEventData data);
    public delegate void _OnTipped(StreamEventData data);
    public delegate void _OnFollowed(StreamEventData data);
    public delegate void _OnWheelSpinClaimed(StreamEventData data);
    public delegate void _OnSubscribed(StreamEventData data);

    public static event _OnTick OnTick;
    public static event _OnClosing OnClosing;
    public static event _OnError OnError;
    public static event _OnPreload OnPreload;
    public static event _OnBotStart OnBotStart;
    public static event _OnBotDisconnected OnBotDisconnected;
    public static event _OnStreamEvent OnStreamEvent;
    public static event _OnTipped OnTipped;
    public static event _OnFollowed OnFollowed;
    public static event _OnWheelSpinClaimed OnWheelSpinClaimed;
    public static event _OnSubscribed OnSubscribed;

    public static void InvokeEvent(EventType type, object args)
    {
        switch (type)
        {
            case EventType.OnTick:
                OnTick?.Invoke((DateTime)args);
                break;

            case EventType.OnError:
                OnError?.Invoke((string)args);
                break;

            case EventType.OnPreload:
                OnPreload?.Invoke();
                break;

            case EventType.OnClosing:
                OnClosing?.Invoke();
                break;
            case EventType.OnBotStart:
                OnBotStart?.Invoke();
                break;

            case EventType.OnBotDisconnected:
                OnBotDisconnected?.Invoke();
                break;

            case EventType.OnStreamEvent:
                OnStreamEvent?.Invoke((StreamEventData)args);
                break;

            case EventType.OnTipped:
                OnTipped?.Invoke((StreamEventData)args);
                break;

            case EventType.OnFollowed:
                OnFollowed?.Invoke((StreamEventData)args);
                break;

            case EventType.OnSubscribed:
                OnSubscribed?.Invoke((StreamEventData)args);
                break;

            case EventType.OnWheelSpinClaimed:
                OnWheelSpinClaimed?.Invoke((StreamEventData)args);
                break;


        }
    }
}