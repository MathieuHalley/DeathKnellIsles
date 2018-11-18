using System;

namespace Assets.Scripts.Events
{
//  Pause event handler interfaces

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

//  Pause event classes

    [Serializable]
    public sealed class PauseEvent : GameEvent<PauseEventData> { }

    [Serializable]
    public sealed class PauseStartEvent : GameEvent<PauseEventData> { }

    [Serializable]
    public sealed class PauseStopEvent : GameEvent<PauseEventData> { }

    /// <summary>
    ///     Inherits from <see cref="GameEventData"/>.
    ///     Provides general event utility data and data relevant to any pause events.
    /// </summary>

    [Serializable]
    public class PauseEventData : GameEventData { }
}