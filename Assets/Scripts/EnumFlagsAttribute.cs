using System;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{

	public class BitMaskAttribute : PropertyAttribute
	{
		public Type propertyType;

		public BitMaskAttribute(Type type)
		{
			propertyType = type;
		}
	}

	public static class BitMaskEditorExtension
	{
		public static int DrawBitMaskField(Rect position, int value, Type enumType, GUIContent mask)
		{
			var itemNames = Enum.GetNames(enumType);
			var itemValues = Enum.GetValues(enumType) as int[];
			var newValue = value;
			var maskValue = 0;

			for (var i = 0; i < itemValues.Length; i++)
			{
				if ((itemValues[i] != 0 && (newValue & itemValues[i]) == itemValues[i]) || newValue == 0)
				{
					maskValue |= 1 << i;
				}
			}

			var newMaskValue = EditorGUI.MaskField(position, mask, maskValue, itemNames);
			var delta = maskValue ^ newMaskValue;

			for (var i = 0; i < itemValues.Length; i++)
			{
				if ((delta & (1 << i)) != 0)
				{
					if ((newMaskValue & (1 << i)) != 0)
					{
						if (itemValues[i] == 0)
						{
							newValue = 0;
							break;
						}
						else
						{
							newValue |= itemValues[i];
						}
					}
					else
					{
						newValue &= ~itemValues[i];
					}
				}
			}

			return newValue;
		}
	}
}