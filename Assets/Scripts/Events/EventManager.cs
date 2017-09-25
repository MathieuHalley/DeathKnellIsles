using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Game.Events
{
    public enum EventScope { Global, Local };
    
    public class EventManager
    {
        private Dictionary<Type, System.Object> events;

        private static EventManager global;
        private static EventManager Global
        {
            get
            {
                if (global == null)
                {
                    global = new EventManager();
                }

                return global;
            }
        }

        public EventManager()
        {
            if (events == null)
            {
                events = new Dictionary<Type, System.Object>();
            }

        }

        public void AddListener<T1,T2>(EventScope scope, UnityAction<T2> listener)
            where T1 : GameEvent<T2>, new()
            where T2 : GameEventData
        {
            var eventType = typeof(T1);

            if (scope == EventScope.Global)
            {
                Global.AddListener<T1,T2>(EventScope.Local, listener);
                return;
            }

            if (events.ContainsKey(eventType) == false)
            {
                events.Add(eventType, new T1() as System.Object);
            }

            (events[eventType] as T1).AddListener(listener);
        }


        public T1 GetEvent<T1,T2>(EventScope scope)
            where T1 : GameEvent<T2>
            where T2 : GameEventData
        {
            if (scope == EventScope.Global)
            {
                return Global.GetEvent<T1, T2>(EventScope.Local);
            }

            var eventType = typeof(T1);

            return events.ContainsKey(eventType) ? events[eventType] as T1 : null;
        }

        //public IEnumerable<GameEvent<GameEventData>> GetEvents(EventScope scope)
        //{
        //    return scope == EventScope.Global 
        //        ? Global.GetEvents(EventScope.Local) 
        //        : events.Values.ToList() as IEnumerable<GameEvent<GameEventData>>;
        //}

        public void InvokeEvent<T1,T2>(EventScope scope, T2 data)
            where T1 : GameEvent<T2>
            where T2 : GameEventData
        {
            if (scope == EventScope.Global)
            {
                Global.InvokeEvent<T1,T2>(EventScope.Local, data);
                return;
            }

            var eventType = typeof(T1);

            if (events.ContainsKey(eventType))
            {
                (events[eventType] as T1).Invoke(data);
            }
        }

        public void RemoveEvent<T1,T2>(EventScope scope)
            where T1 : GameEvent<T2>
            where T2 : GameEventData
        {
            if (scope == EventScope.Global)
            {
                Global.RemoveEvent<T1,T2>(EventScope.Local);
                return;
            }

            events.Remove(typeof(T1));
        }

        public void RemoveListener<T1,T2>(EventScope scope, UnityAction<T2> listener)
            where T1 : GameEvent<T2>
            where T2 : GameEventData
        {
            if (scope == EventScope.Global)
            {
                Global.RemoveListener<T1, T2>(scope, listener);
                return;
            }

            (events[typeof(T1)] as T1).RemoveListener(listener);
        }


        public void ClearEvents(EventScope scope)
        {
            if (scope == EventScope.Global)
            {
                ClearEvents(EventScope.Local);
                return;
            }

            events = new Dictionary<Type, System.Object>();
        }
    }
}