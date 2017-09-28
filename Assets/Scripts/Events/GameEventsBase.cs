using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

//  The base game event classes & interfaces are included in this one file 
//  as they're deeply related and their definitions are incredibly short 
//  or empty.

namespace Game.Events
{
    /// <summary>
    ///     A base interface for any class that handles <see cref="GameEvent{T}"/>. 
    ///     Its purpose is to ensure that every implementing class
    ///     has a reference to an <see cref="EventManager"/>.
    /// </summary>

    public interface IGameEventHandler
    {
        EventManager EventManager { get; }
    }


    /// <summary>
    ///     Inherits from the abstract generic class <see cref="UnityEvent{T0}"/>. 
    ///     Any in-game events should use <see cref="GameEvent{T}"/> as a base class. 
    ///     It's important that any inheriting class has the Serializable attribute.
    /// </summary>
    /// <typeparam name="T">
    /// The <see cref="GameEventData"/> type used by this <see cref="GameEvent{T}"/>.
    /// </typeparam>

    [Serializable]
    public abstract class GameEvent<T> : UnityEvent<T> where T : GameEventData
    { }


    /// <summary>
    ///     Collates data for <see cref="GameEvent{T}"/>. 
    ///     Any class that provides data to an event should use 
    ///     <see cref="GameEventData"/> as a base class. 
    ///     It's important that any inheriting class has the Serializable attribute.
    /// </summary>

    [Serializable]
    public class GameEventData
    {
        public static uint EventID { get; private set; }
        public TimeSpan EventTime { get; private set; }
        public string EventType { get; private set; }
        public string EventLog
        {
            get
            {
                return new StringBuilder()
                    .AppendLine("Event: " + EventType)
                    .AppendLine("EventTime: " + FormatEventTime())
                    .AppendLine("EventID: " + EventID)
                    .ToString();
            }
        }

        public GameEventData()
        {
            EventID += 1;
            EventTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            EventType = new StringBuilder(this.GetType().ToString())
                .Replace("Data", "")
                .ToString();
        }

        private string FormatEventTime()
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:####}", 
                (int)EventTime.TotalHours, 
                (int)EventTime.TotalMinutes, 
                EventTime.TotalSeconds,
                EventTime.TotalMilliseconds);
        }
    }
}