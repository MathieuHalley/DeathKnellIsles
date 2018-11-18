using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.Events
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

        readonly Dictionary<System.Type, object> _events;

        static EventManager _g;

        /// <summary>
        ///     A static property <see cref="EventManager"/>
        ///     that holds all of the references to global events.
        /// </summary>

        public static EventManager Global => _g ?? new EventManager();

        public EventManager()
        {
            _g = _g ?? new EventManager();
            _events = _events ?? new Dictionary<System.Type, object>();
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

        public void AddListener<T0, T1>(UnityAction<T1> listener)
            where T0 : GameEvent<T1>, new()
            where T1 : GameEventData
        {
            var eventType = typeof(T0);

            //  If the event isn't already being managed by this EventManager, it'll be instantiated and added to the event collection.

            if (_events.ContainsKey(eventType) == false)
            {
                _events.Add(eventType, new T0());
            }

            ((T0) _events[eventType]).AddListener(listener);
        }

        /// <summary>
        ///     A method in the <see cref="EventManager"/> class that removes
        ///     all of the <see cref="GameEvent{T}"/> objects managed by either
        ///     the global or a local <see cref="EventManager"/>.
        /// </summary>

        public void ClearEvents()
        {
            _events.Clear();
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

        public T0 GetEvent<T0, T1>() where T0 : GameEvent<T1>, new() where T1 : GameEventData
        {
            var eventType = typeof(T0);

            return _events.ContainsKey(eventType) ? _events[eventType] as T0 : null;
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
        public void InvokeEvent<T0, T1>(T1 data) where T0 : GameEvent<T1>, new() where T1 : GameEventData
        {
            var eventType = typeof(T0);

            if (_events.ContainsKey(eventType))
            {
                ((T0) _events[eventType]).Invoke(data);
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
        public void RemoveEvent<T0, T1>() where T0 : GameEvent<T1>, new() where T1 : GameEventData
        {
            _events.Remove(typeof(T0));
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

        public void RemoveListener<T0, T1>(UnityAction<T1> listener) where T0 : GameEvent<T1>, new() where T1 : GameEventData
        {
            ((T0) _events[typeof(T0)]).RemoveListener(listener);
        }
    }
}