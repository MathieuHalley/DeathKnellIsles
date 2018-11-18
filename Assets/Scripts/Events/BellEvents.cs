using System;

namespace Assets.Scripts.Events
{
//  Bell event handler interfaces

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

//  Bell event classes

    [Serializable]
    public sealed class BellEvent : GameEvent<BellEventData> { }

    [Serializable]
    public sealed class BellStartEvent : GameEvent<BellEventData> { }

    [Serializable]
    public sealed class BellStopEvent : GameEvent<BellEventData> { }

    /// <summary>
    ///     Inherits from <see cref="GameEventData"/>.
    ///     Provides general event utility data and data relevant to any Bell events.
    /// </summary>

    [Serializable]
    public class BellEventData : GameEventData { }
}