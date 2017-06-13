using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

public static class EnumExtensions
{
	private static void CheckIsFlagEnum<T>(bool withFlags)
	{
		var type = typeof(T);
		if (!type.IsEnum)
		{
			throw new ArgumentException(string.Format("Type '{0}' is not an enum.", type.FullName));
		}
		if ( withFlags && !Attribute.IsDefined(type, typeof(FlagsAttribute)))
		{
			throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute.", type.FullName));
		}

	}

	public static bool IsFlagSet<T>(this T value, T flag) where T : struct
	{
		CheckIsFlagEnum<T>(true);
		var lValue = Convert.ToInt64(value);
		var lFlag = Convert.ToInt64(flag);

		return (lValue & lFlag) != 0;
	}

	public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
	{
		CheckIsFlagEnum<T>(true);

		foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
		{
			if (value.IsFlagSet(flag))
			{
				yield return flag;
			}
		}
	}

	public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
	{
		CheckIsFlagEnum<T>(true);

		var lValue = Convert.ToInt64(value);
		var lFlags = Convert.ToInt64(flags);

		lValue = on ? lValue |= lFlags : lValue &= (~lFlags);

		return (T)Enum.ToObject(typeof(T), lValue);
	}

	public static T SetFlags<T>(this T value, T flags) where T : struct
	{
		return value.SetFlags(flags, true);
	}

	public static T ClearFlags<T>(this T value, T flags) where T : struct
	{
		return value.SetFlags(flags, false);
	}

	public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
	{
		CheckIsFlagEnum<T>(false);

		var value = 0L;

		foreach (var flag in flags)
		{
			value |= Convert.ToInt64(flag);
		}

		return (T)Enum.ToObject(typeof(T), value);
	}

	public static string GetDescription<T>(this T value) where T : struct
	{
		CheckIsFlagEnum<T>(true);

		var type = typeof(T);
		var name = Enum.GetName(type, value);

		if (name == null)
		{
			return null;
		}

		var field = type.GetField(name);

		if (field == null)
		{
			return null;
		}

		var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

		if (attribute == null)
		{
			return null;
		}

		return attribute.Description;

	}

	private static T ToEnum<T>(long value)
	{
		return (T)Enum.ToObject(typeof(T), value);
	}

	//public static IEnumerable<Enum> GetFlags(this Enum value)
	//{
	//	var values = (Enum[])Enum.GetValues(value.GetType());
	//	return GetFlags(value, values);
	//}

		//public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
		//{
		//	return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
		//}

		//public static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
		//{
		//	var valueL = Convert.ToUInt64(value);
		//	var bits = Convert.ToUInt64(value);
		//	List<Enum> results = new List<Enum>();
		//	for (var i = values.Length - 1; i >= 0; i--)
		//	{
		//		ulong mask = Convert.ToUInt64(values[i]);
		//		if (i == 0 && mask == 0L)
		//		{
		//			break;
		//		}
		//		if ((bits & mask) == mask)
		//		{
		//			results.Add(values[i]);
		//			bits -= mask;
		//		}
		//	}
		//	if (bits != 0L)
		//	{
		//		return Enumerable.Empty<Enum>();
		//	}
		//	if (valueL != 0L)
		//	{
		//		return results.Reverse<Enum>();
		//	}
		//	if (bits == valueL && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
		//	{
		//		return values.Take(1);
		//	}
		//	return Enumerable.Empty<Enum>();
		//}

		//private static IEnumerable<Enum> GetFlagValues(Type enumType)
		//{
		//	ulong flag = 0x1;
		//	foreach(var value in (Enum[])Enum.GetValues(enumType))
		//	{
		//		var bits = Convert.ToUInt64(value);
		//		if (bits == 0L) continue;
		//		while (flag < bits) flag <<= 1;
		//		if (flag == bits) yield return value;
		//	}
		//}
}
