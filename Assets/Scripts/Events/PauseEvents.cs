using System;

namespace Game.Events
{
    public interface IPauseEventHandler : IGameEventHandler
    {
        void OnPauseEvent(PauseEventData data);
    }

    public interface IPauseStartEventHandler : IGameEventHandler
    {
        void OnPauseStartEvent(PauseEventData data);
    }

    public interface IPauseStopEventHandler : IGameEventHandler
    {
        void OnPauseStopEvent(PauseEventData data);
    }

    [Serializable] public class PauseEvent : GameEvent<PauseEventData> { }

    [Serializable] public class PauseStartEvent : GameEvent<PauseEventData> { }

    [Serializable] public class PauseStopEvent : GameEvent<PauseEventData> { }
    
    [Serializable] public class PauseEventData : GameEventData { }
}