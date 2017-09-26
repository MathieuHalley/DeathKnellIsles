using Game.Events;
using UnityEngine;

/// <summary>
///     A test MonoBehaviour class that experiments 
///     with basic functionality in the game's event system.
/// </summary>
public class EventTest : MonoBehaviour, IPauseEventHandler, IBellEventHandler
{
    public Game.Events.EventManager EventManager { get; private set; }

    public void Awake()
    {

    }

    //  Listeners for local and global BellEvents, and local and global PauseEvents

    public void OnBellEvent(BellEventData data)
    {
        Debug.Log("BELL Local boop " + data.EventLog);
    }

    public void OnBellGlobalEvent(BellEventData data)
    {
        Debug.Log("BELL Global boop " + data.EventLog);
    }

    public void OnPauseEvent(PauseEventData data)
    {
        Debug.Log("PAUSE Local boop " + data.EventLog);
    }

    public void OnPauseGlobalEvent(PauseEventData data)
    {
        Debug.Log("PAUSE Global boop " + data.EventLog);
    }
    
    void Start()
    {
        EventManager = new EventManager();

        //  New listeners being added to local & global EventManager

        EventManager.AddListener<BellEvent, BellEventData>(EventScope.Local, OnBellEvent);
        EventManager.AddListener<BellEvent, BellEventData>(EventScope.Global, OnBellGlobalEvent);

        EventManager.AddListener<PauseEvent, PauseEventData>(EventScope.Local, OnPauseEvent);
        EventManager.AddListener<PauseEvent, PauseEventData>(EventScope.Global, OnPauseGlobalEvent);
    }

    void Update ()
    {
        //  Global & local Events being invoked, triggering their listeners.

        EventManager.InvokeEvent<BellEvent, BellEventData>(EventScope.Local, new BellEventData());
        EventManager.InvokeEvent<BellEvent, BellEventData>(EventScope.Global, new BellEventData());

        EventManager.InvokeEvent<PauseEvent, PauseEventData>(EventScope.Local, new PauseEventData());
        EventManager.InvokeEvent<PauseEvent, PauseEventData>(EventScope.Global, new PauseEventData());
    }
}
