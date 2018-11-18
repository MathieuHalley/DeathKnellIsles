using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Track & query the current state of one or more keys
namespace Assets.Scripts.Input
{
    public sealed class InputTest : MonoBehaviour
    {
        private Dictionary<KeyCode, bool> _keyState;
        Dictionary<KeyCode, bool> _keyStateWasUpdated;

        void Start()
        {
            _keyState = new Dictionary<KeyCode, bool>();
            StartCoroutine(KeyStateUpdate());
        }

        IEnumerator KeyStateUpdate()
        {
            while (true)
            {
                _keyStateWasUpdated = new Dictionary<KeyCode, bool>();
                yield return new WaitForEndOfFrame();
            }
        }

        public bool GetKeyState(KeyCode key)
        {
            return _keyState[key];
        }

        public bool[] GetKeyState(params KeyCode[] keys)
        {
            var keyStatus = new bool[keys.Length];

            for (var i = 0 ; i < keys.Length ; i++)
            {
                keyStatus[i] = GetKeyState(keys[i]);
            }

            return keyStatus;
        }

        public bool GetKeyDown(KeyCode key)
        {
            return _keyState[key] && _keyStateWasUpdated[key];
        }

        public bool[] GetKeyDown(params KeyCode[] keys)
        {
            var keyStatus = new bool[keys.Length];

            for (var i = 0 ; i < keys.Length ; i++)
            {
                keyStatus[i] = GetKeyDown(keys[i]);
            }

            return keyStatus;
        }

        public bool GetKeyUp(KeyCode key)
        {
            return !_keyState[key] && _keyStateWasUpdated[key];
        }

        public bool[] GetKeyUp(params KeyCode[] keys)
        {
            var keyStatus = new bool[keys.Length];

            for (var i = 0 ; i < keys.Length ; i++)
            {
                keyStatus[i] = GetKeyUp(keys[i]);
            }

            return keyStatus;
        }

        public void SetKeyDown(KeyCode key)
        {
            _keyState[key] = true;
            _keyStateWasUpdated[key] = true;
        }

        public void SetKeyDown(params KeyCode[] keys)
        {
            foreach (var t in keys)
            {
                SetKeyDown(t);
            }
        }

        public void SetKeyUp(KeyCode key)
        {
            _keyState[key] = false;
            _keyStateWasUpdated[key] = true;
        }

        public void SetKeyUp(params KeyCode[] keys)
        {
            foreach (var t in keys)
            {
                SetKeyUp(t);
            }
        }

        public void ToggleKey(KeyCode key)
        {
            _keyState[key] = !_keyState[key];
            _keyStateWasUpdated[key] = true;
        }

        public void ToggleKey(params KeyCode[] keys)
        {
            foreach (var t in keys)
            {
                ToggleKey(t);
            }
        }
    }
}
