using System;
using System.Linq;
using System.Collections.Generic;

public static class EnumExtension
{
	public static IEnumerable<Enum> GetFlags(this Enum value)
	{
		var values = (Enum[])Enum.GetValues(value.GetType());
		return GetFlags(value, values);
	}

	public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
	{
		return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
	}

	public static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
	{
		var valueL = Convert.ToUInt64(value);
		var bits = Convert.ToUInt64(value);
		List<Enum> results = new List<Enum>();
		for (var i = values.Length - 1; i >= 0; i--)
		{
			ulong mask = Convert.ToUInt64(values[i]);
			if (i == 0 && mask == 0L)
			{
				break;
			}
			if ((bits & mask) == mask)
			{
				results.Add(values[i]);
				bits -= mask;
			}
		}
		if (bits != 0L)
		{
			return Enumerable.Empty<Enum>();
		}
		if (valueL != 0L)
		{
			return results.Reverse<Enum>();
		}
		if (bits == valueL && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
		{
			return values.Take(1);
		}
		return Enumerable.Empty<Enum>();
	}

	private static IEnumerable<Enum> GetFlagValues(Type enumType)
	{
		ulong flag = 0x1;
		foreach(var value in (Enum[])Enum.GetValues(enumType))
		{
			var bits = Convert.ToUInt64(value);
			if (bits == 0L) continue;
			while (flag < bits) flag <<= 1;
			if (flag == bits) yield return value;
		}
	}
}
