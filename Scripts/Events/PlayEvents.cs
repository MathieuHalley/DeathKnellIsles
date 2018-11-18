using System;

namespace Assets.Scripts.Events
{
//  Play event handler interfaces

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

//  Play event classes

    [Serializable]
    public sealed class PlayEvent : GameEvent<PlayEventData> { }

    [Serializable]
    public sealed class PlayStartEvent : GameEvent<PlayEventData> { }

    [Serializable]
    public sealed class PlayStopEvent : GameEvent<PlayEventData> { }

    /// <summary>
    ///     Inherits from <see cref="GameEventData"/>.
    ///     Provides general event utility data and data relevant to any play events.
    /// </summary>

    [Serializable]
    public class PlayEventData : GameEventData { }
}