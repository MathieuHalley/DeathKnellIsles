using System;

namespace Game.Events
{
    public interface IPlayEventHandler : IGameEventHandler
    {
        void OnPlayEvent(PlayEventData data);
    }

    public interface IPlayStartEventHandler : IGameEventHandler
    {
        void OnPlayStartEvent(PlayEventData data);
    }

    public interface IPlayStopEventHandler : IGameEventHandler
    {
        void OnPlayStopEvent(PlayEventData data);
    }

    [Serializable] public class PlayEvent : GameEvent<PlayEventData> { }

    [Serializable] public class PlayStartEvent : GameEvent<PlayEventData> { }

    [Serializable] public class PlayStopEvent : GameEvent<PlayEventData> { }

    [Serializable] public class PlayEventData : GameEventData { }
}