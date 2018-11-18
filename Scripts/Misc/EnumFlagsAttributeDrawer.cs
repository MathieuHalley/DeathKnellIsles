using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    [CustomPropertyDrawer(typeof(BitMaskAttribute))]
    public class BitMaskPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text += "(" + property.intValue + ")";
            property.intValue = BitMaskEditorExtension.DrawBitMaskField(position, property.intValue, ((BitMaskAttribute) attribute).PropertyType, label);
        }
    }
}