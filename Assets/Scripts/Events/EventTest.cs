using Game.Events;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour, IPauseEventHandler
{
    public Game.Events.EventManager EventManager { get; private set; }

    public void OnPauseEvent(PauseEventData data)
    {
        Debug.Log("boop1  " + data.EventDetails);
    }

    public void OnPauseGlobalEvent(PauseEventData data)
    {
        Debug.Log("boop2  " + data.EventDetails);
    }
    
    void Start()
    {
        EventManager = new EventManager();
        EventManager.AddListener<PauseEvent, PauseEventData>(EventScope.Local, OnPauseEvent);
        EventManager.AddListener<PauseEvent, PauseEventData>(EventScope.Global, OnPauseGlobalEvent);
    }

    void Update ()
    {
        EventManager.InvokeEvent<PauseEvent, PauseEventData>(EventScope.Local, new PauseEventData());
        EventManager.InvokeEvent<PauseEvent, PauseEventData>(EventScope.Global, new PauseEventData());
    }
}
