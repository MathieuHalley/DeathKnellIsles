using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
	[CustomPropertyDrawer(typeof(BitMaskAttribute))]
	public class BitMaskPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var typeAttribute = attribute as BitMaskAttribute;

			label.text += "(" + property.intValue + ")";
			property.intValue
				= BitMaskEditorExtension.DrawBitMaskField(
					position,
					property.intValue,
					typeAttribute.propertyType,
					label);

		}
	}
}
