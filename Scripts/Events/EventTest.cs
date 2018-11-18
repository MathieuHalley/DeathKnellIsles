using UnityEngine;

//  A test MonoBehaviour class that experiments with basic functionality in the game's event system.
namespace Assets.Scripts.Events
{
    public class EventTest : MonoBehaviour, IPauseEventHandler, IBellEventHandler
    {
        public EventManager EventManager { get; } = new EventManager();

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
            //  New listeners being added to local & global EventManager
            EventManager.AddListener<BellStartEvent, BellEventData>(OnBellEvent);
            EventManager.AddListener<PauseStartEvent, PauseEventData>(OnPauseEvent);
            EventManager.Global.AddListener<BellStartEvent, BellEventData>(OnBellGlobalEvent);
            EventManager.Global.AddListener<PauseStartEvent, PauseEventData>(OnPauseGlobalEvent);
        }

        void Update()
        {
            //  Global & local Events being invoked, triggering their listeners.
            EventManager.InvokeEvent<BellEvent, BellEventData>(new BellEventData());
            EventManager.InvokeEvent<PauseEvent, PauseEventData>(new PauseEventData());
            EventManager.Global.InvokeEvent<BellEvent, BellEventData>(new BellEventData());
            EventManager.Global.InvokeEvent<PauseEvent, PauseEventData>(new PauseEventData());
        }
    }
}