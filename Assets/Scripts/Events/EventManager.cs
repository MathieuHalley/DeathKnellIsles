using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public enum EventScope { Global, Local };

    /// <summary>
    ///     Manages active events through a singleton/multiton pattern.
    ///     Independent instances of <see cref="EventManager"/> are used for 
    ///     local events (e.g.: AttackedEvent) and a private static instance of 
    ///     <see cref="EventManager"/> manages global events (e.g.: GameStopEvent).
    /// </summary>

    public class EventManager
    {

        /// <summary>
        ///     A collection that holds references to all of the 
        ///     <see cref="GameEvent{T}"/> objects managed by this EventManager 
        ///     and their types. System.Type and System.Object are used as the 
        ///     keys and values to allow for unknown types.
        /// </summary>

        private readonly Dictionary<System.Type, System.Object> events;

        private static EventManager g;

        /// <summary> 
        ///     A static property <see cref="EventManager"/> 
        ///     that holds all of the references to global events.
        /// </summary>
        
        public static EventManager Global
        {
            get
            {
                if (g == null)
                {
                    g = new EventManager();
                }

                return g;
            }
        }

        public EventManager()
        {
            if (events == null)
            {
                events = new Dictionary<System.Type, System.Object>();
            }
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that 
        ///     adds a <see cref="UnityAction"/> as a listener to a 
        ///     <see cref="GameEvent{T}"/> managed by either the global 
        ///     <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that 
        ///     <paramref name="listener"/> is being added to.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/> and <paramref name="listener"/>.
        /// </typeparam>
        /// <param name="listener">
        ///     Used to specify the <see cref="UnityAction"/> is being
        ///     added to listen for the <typeparamref name="T0"/> event.
        /// </param>

        public void AddListener<T0,T1>(UnityAction<T1> listener)
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            var eventType = typeof(T0);

            //  If the event isn't already being managed by this EventManager, 
            //  it'll be instantiated and added to the event collection.

            if (events.ContainsKey(eventType) == false)
            {
                events.Add(eventType, new T0() as System.Object);
            }

            (events[eventType] as T0).AddListener(listener);
        }

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes 
        ///     all of the <see cref="GameEvent{T}"/> objects managed by either 
        ///     the global or a local <see cref="EventManager"/>.
        /// </summary>

        public void ClearEvents()
        {
            events.Clear();
        }

       /// <summary>
        ///     A method in the <see cref="EventManager"/> class that retrieves 
        ///     an active <see cref="GameEvent{T}"/> that's stored within either 
        ///     the global <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being returned.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <returns>
        ///     Returns the requested <see cref="GameEvent{T}"/> or null.
        /// </returns>

        public T0 GetEvent<T0, T1>()
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            var eventType = typeof(T0);

            return events.ContainsKey(eventType) ? events[eventType] as T0 : null;
        }

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that invokes 
        ///     an active <see cref="GameEvent{T}"/> that's stored within either 
        ///     the global <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being invoked.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="data">
        ///     Used to provide the invoked event with relevant data.
        /// </param>
        /// 
        public void InvokeEvent<T0, T1>(T1 data)
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            var eventType = typeof(T0);

            if (events.ContainsKey(eventType))
            {
                (events[eventType] as T0).Invoke(data);
            }
        }

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes an 
        ///     active <see cref="GameEvent{T}"/> from within either the global 
        ///     <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being removed.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's 
        ///     used with <typeparamref name="T0"/>.
        /// </typeparam>

        public void RemoveEvent<T0, T1>()
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            events.Remove(typeof(T0));
        }

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes a 
        ///     <see cref="UnityAction{T0}"/> from a <see cref="GameEvent{T}"/> 
        ///     managed by either the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's having 
        ///     <paramref name="listener"/> removed from it.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="listener">
        ///     Used to specify the <see cref="UnityAction{T}"/> 
        ///     that's being removed.
        /// </param>

        public void RemoveListener<T0, T1>(UnityAction<T1> listener)
        where T0 : GameEvent<T1>, new()
        where T1 : GameEventData
        {
            (events[typeof(T0)] as T0).RemoveListener(listener);
        }


        //  --------------------------------------------------------------------------------
        //  Old implementation of EventManager methods. Retained for backwards compatiblity.
        //  --------------------------------------------------------------------------------

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that 
        ///     adds a <see cref="UnityAction"/> as a listener to a 
        ///     <see cref="GameEvent{T}"/> managed by either the global 
        ///     <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that 
        ///     <paramref name="listener"/> is being added to.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/> and <paramref name="listener"/>.
        /// </typeparam>
        /// <param name="scope">
        ///     Used to determine whether <paramref name="listener"/> 
        ///     is being added to the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>
        /// <param name="listener">
        ///     Used to specify the <see cref="UnityAction"/> is being
        ///     added to listen for the <typeparamref name="T0"/> event.
        /// </param>

        public void AddListener<T0, T1>(EventScope scope, UnityAction<T1> listener)
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            var eventType = typeof(T0);

            //  If scope is EventScope.Global this method will run again, 
            //  adding listener to the global EventManager instead of to 
            //  a local EventManager.

            if (scope == EventScope.Global)
            {
                Global.AddListener<T0, T1>(EventScope.Local, listener);
                return;
            }

            //  If the event isn't already being managed by this EventManager, 
            //  it'll be instantiated and added to the event collection.

            if (events.ContainsKey(eventType) == false)
            {
                events.Add(eventType, new T0() as System.Object);
            }

            (events[eventType] as T0).AddListener(listener);
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes 
        ///     all of the <see cref="GameEvent{T}"/> objects managed by either 
        ///     the global or a local <see cref="EventManager"/>.
        /// </summary>
        /// <param name="scope">
        ///     Used to determine whether <typeparamref name="T0"/> is being 
        ///     removed from the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>

        public void ClearEvents(EventScope scope)
        {
            //  If scope is EventScope.Global this method will run again, 
            //  clearing all of the global events instead of the events 
            //  in a local EventManager.

            if (scope == EventScope.Global)
            {
                ClearEvents(EventScope.Local);
                return;
            }

            events.Clear();
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that retrieves 
        ///     an active <see cref="GameEvent{T}"/> that's stored within either 
        ///     the global <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being returned.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="scope">
        ///     Used to determine whether <typeparamref name="T0"/> is being 
        ///     retrieved from the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>
        /// <returns>
        ///     Returns the requested <see cref="GameEvent{T}"/> or null.
        /// </returns>

        public T0 GetEvent<T0, T1>(EventScope scope)
            where T0 : GameEvent<T1>
            where T1 : GameEventData
        {
            //  If scope is EventScope.Global this method will run again, 
            //  retrieving the GameEvent from the global EventManager 
            //  instead of a local EventManager.

            if (scope == EventScope.Global)
            {
                return Global.GetEvent<T0, T1>(EventScope.Local);
            }

            var eventType = typeof(T0);

            return events.ContainsKey(eventType) ? events[eventType] as T0 : null;
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that invokes 
        ///     an active <see cref="GameEvent{T}"/> that's stored within either 
        ///     the global <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being invoked.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="scope">
        ///     Used to determine whether <typeparamref name="T0"/> is 
        ///     being invoked from the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>
        /// <param name="data">
        ///     Used to provide the invoked event with relevant data.
        /// </param>

        public void InvokeEvent<T0, T1>(EventScope scope, T1 data)
            where T0 : GameEvent<T1>
            where T1 : GameEventData
        {
            //  If scope is EventScope.Global this method will run again, 
            //  invoking the event via the global EventManager instead 
            //  of via a local EventManager.

            if (scope == EventScope.Global)
            {
                Global.InvokeEvent<T0, T1>(EventScope.Local, data);
                return;
            }

            var eventType = typeof(T0);

            if (events.ContainsKey(eventType))
            {
                (events[eventType] as T0).Invoke(data);
            }
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes an 
        ///     active <see cref="GameEvent{T}"/> from within either the global 
        ///     <see cref="EventManager"/> or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's being removed.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's 
        ///     used with <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="scope">
        ///     Used to determine whether <typeparamref name="T0"/> is being 
        ///     removed from the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>

        public void RemoveEvent<T0, T1>(EventScope scope)
            where T0 : GameEvent<T1>
            where T1 : GameEventData
        {
            //  If the scope is identified as Global this method will run again, 
            //  removing the event from the global EventManager instead of 
            //  a local EventManager.

            if (scope == EventScope.Global)
            {
                Global.RemoveEvent<T0, T1>(EventScope.Local);
                return;
            }

            events.Remove(typeof(T0));
        }


        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes a 
        ///     <see cref="UnityAction{T0}"/> from a <see cref="GameEvent{T}"/> 
        ///     managed by either the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </summary>
        /// <typeparam name="T0">
        ///     The type of <see cref="GameEvent{T}"/> that's having 
        ///     <paramref name="listener"/> removed from it.
        /// </typeparam>
        /// <typeparam name="T1">
        ///     The type of <see cref="GameEventData"/> that's used with 
        ///     <typeparamref name="T0"/>.
        /// </typeparam>
        /// <param name="scope">
        ///     Used to determine whether <paramref name="listener"/> 
        ///     is being removed from the global <see cref="EventManager"/> 
        ///     or a local <see cref="EventManager"/>.
        /// </param>
        /// <param name="listener">
        ///     Used to specify the <see cref="UnityAction{T}"/> 
        ///     that's being removed.
        /// </param>

        public void RemoveListener<T0, T1>(EventScope scope, UnityAction<T1> listener)
            where T0 : GameEvent<T1>
            where T1 : GameEventData
        {
            if (scope == EventScope.Global)
            {
                Global.RemoveListener<T0, T1>(scope, listener);
                return;
            }

            (events[typeof(T0)] as T0).RemoveListener(listener);
        }
    }
}