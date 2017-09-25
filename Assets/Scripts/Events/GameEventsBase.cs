using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public interface IGameEventHandler
    {
        EventManager EventManager { get; }
    }

    [Serializable] public class GameEvent<T> : UnityEvent<T> where T : GameEventData { }

    [Serializable] public class GameEventData : UnityEngine.Object
    {
        public static int EventID { get; private set; }
        public readonly TimeSpan EventTime;
        public readonly string EventName;
        public string EventDetails
        {
            get
            {
                return new StringBuilder()
                    .Append("EventID: " + EventID)
                    .Append(" - EventName: " + EventName)
                    .Append(" - EventTime: " + EventTime.ToString("c"))
                    .ToString();
            }
        }

        public GameEventData()
        {
            EventID += 1;
            EventTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            EventName = new StringBuilder(this.GetType().ToString())
                .Replace("Data","")
                .ToString();
        }
    }
}