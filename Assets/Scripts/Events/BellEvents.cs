using System;

namespace Game.Events
{
    public interface IBellEventHandler : IGameEventHandler
    {
        void OnBellEvent(BellEventData data);
    }

    public interface IBellStartEventHandler : IGameEventHandler
    {
        void OnBellStartEvent(BellEventData data);
    }

    public interface IBellStopEventHandler : IGameEventHandler
    {
        void OnBellStopEvent(BellEventData data);
    }

    [Serializable] public class BellEvent : GameEvent<BellEventData> { }

    [Serializable] public class BellStartEvent : GameEvent<BellEventData> { }

    [Serializable] public class BellStopEvent : GameEvent<BellEventData> { }

    [Serializable] public class BellEventData : GameEventData { }
}