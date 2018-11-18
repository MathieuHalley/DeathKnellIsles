using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public enum InputType
    {
        KeyOrMouseButton,
        MouseMovement,
        JoystickAxis,
    };

    public static class InputAxes
    {
        static HashSet<SerializedProperty> _axes;

        public static Dictionary<string, SerializedProperty> Axes
        {
            get
            {
                var dict = new Dictionary<string, SerializedProperty>();

                foreach (var axis in _axes)
                {
                    dict.Add(axis.FindPropertyRelative("m_Name").stringValue, axis);
                }

                return dict;
            }
        }

        [MenuItem("Assets/Get Input Axes")]
        public static void GetInputAxes()
        {
            IdentifyInputAxes();
        }

        static void IdentifyInputAxes()
        {
            var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
            var axisArray = new SerializedObject(inputManager).FindProperty("m_Axes");

            if (_axes == null)
                _axes = new HashSet<SerializedProperty>();

            if (axisArray.arraySize == 0)
            {
                Debug.Log("No Input Axes");
            }

            Debug.Log("Input Axes:");

            for (var i = 0 ; i < axisArray.arraySize ; ++i)
            {
                var axis = axisArray.GetArrayElementAtIndex(i);
                var value = axis.FindPropertyRelative("axis").intValue;
                var inputType = (InputType) axis.FindPropertyRelative("type").intValue;

                _axes.Add(axis);
            }
        }
    }
}