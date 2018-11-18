using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class BitMaskAttribute : PropertyAttribute
    {
        public readonly Type PropertyType;

        public BitMaskAttribute(Type type)
        {
            PropertyType = type;
        }
    }

    public static class BitMaskEditorExtension
    {
        /// <summary></summary>
        /// <param name="position"></param>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static int DrawBitMaskField(Rect position, int value, Type enumType, GUIContent mask)
        {
            var itemNames = Enum.GetNames(enumType);
            var itemValues = Enum.GetValues(enumType) as int[];
            var newValue = value;
            var maskValue = 0;

            if (itemValues != null)
            {
                for (var i = 0 ; i < itemValues.Length ; i++)
                {
                    if ((itemValues[i] != 0 && (newValue & itemValues[i]) == itemValues[i]) || newValue == 0)
                    {
                        maskValue |= 1 << i;
                    }
                }
            }

            var newMaskValue = EditorGUI.MaskField(position, mask, maskValue, itemNames);
            var delta = maskValue ^ newMaskValue;

            if (itemValues == null) return newValue;

            for (var i = 0; i < itemValues.Length; i++)
            {
                if ((delta & (1 << i)) == 0) continue;
                if ((newMaskValue & (1 << i)) != 0)
                {
                    if (itemValues[i] == 0)
                    {
                        newValue = 0;
                        break;
                    }

                    newValue |= itemValues[i];
                }
                else
                {
                    newValue &= ~itemValues[i];
                }
            }

            return newValue;
        }
    }
}