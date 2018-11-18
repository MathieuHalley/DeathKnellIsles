using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Assets.Scripts.Misc
{
    public static class EnumExtensions
    {
        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="withFlags"></param>
        static void CheckIsFlagEnum<T>(bool withFlags)
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException($"Type '{type.FullName}' is not an enum.");
            }

            var attributeIsDefined = Attribute.IsDefined(type, typeof(FlagsAttribute));

            if (withFlags && !attributeIsDefined)
            {
                throw new ArgumentException($"Type '{type.FullName}' doesn't have the 'Flags' attribute.");
            }
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool IsFlagSet<T>(this T value, T flag) where T : struct
        {
            CheckIsFlagEnum<T>(true);

            return (Convert.ToInt64(value) & Convert.ToInt64(flag)) != 0;
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
        {
            CheckIsFlagEnum<T>(true);

            foreach (var flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagSet(flag))
                { yield return flag; }
            }
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
        {
            CheckIsFlagEnum<T>(true);

            var lValue = Convert.ToInt64(value);
            var lFlags = Convert.ToInt64(flags);
            var flagsOn = lValue | lFlags;
            var flagsOff = lValue & ~lFlags;

            return (T) Enum.ToObject(typeof(T), on ? flagsOn : flagsOff);
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static T SetFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, true);
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static T ClearFlags<T>(this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, false);
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
        {
            CheckIsFlagEnum<T>(false);

            var value = 0L;

            foreach (var flag in flags)
            {
                value |= Convert.ToInt64(flag);
            }

            return (T) Enum.ToObject(typeof(T), value);
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            CheckIsFlagEnum<T>(true);

            var type = typeof(T);
            var name = Enum.GetName(type, value);

            if (name == null)
            { return null; }

            var field = type.GetField(name);

            if (field == null)
            { return null; }

            var attribute = (DescriptionAttribute) Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute?.Description;
        }

        /// <summary></summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        static T ToEnum<T>(long value)
        {
            return (T) Enum.ToObject(typeof(T), value);
        }
    }
}