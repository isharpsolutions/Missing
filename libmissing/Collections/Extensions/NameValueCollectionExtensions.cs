using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Missing.Collections.Extensions
{
	/// <summary>
	/// Holds extensions methods for <see cref="System.Collections.Specialized.NameValueCollection"/>
	/// </summary>
	public static class NameValueCollectionExtensions
	{
		#region Numbers
		/// <summary>
		/// Get a value as an integer (Int32)
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// The <see cref="System.Int32"/> to return if the key does not exist or is invalid
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		public static int GetInt(this NameValueCollection nvc, string key, int defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			int val = defaultValue;
			int.TryParse(nvc[key], out val);
			
			return val;
		}
		
		/// <summary>
		/// Get a value as a long (Int64)
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// The <see cref="System.Int64"/> to return if the key does not exist or is invalid
		/// </param>
		/// <returns>
		/// A <see cref="System.Int64"/>
		/// </returns>
		public static long GetLong(this NameValueCollection nvc, string key, long defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			long val = defaultValue;
			long.TryParse(nvc[key], out val);
			
			return val;
		}
		
		/// <summary>
		/// Get a value as a decimal
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// A <see cref="System.Decimal"/> to return if the key does not exist or the data is invalid
		/// </param>
		/// <returns>
		/// A <see cref="System.Decimal"/>
		/// </returns>
		public static decimal GetDecimal(this NameValueCollection nvc, string key, decimal defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			decimal val = defaultValue;
			
			// TryParse output 0m if the key is invalid
			if (decimal.TryParse(nvc[key], out val))
			{
				return val;
			}
			
			return defaultValue;
		}
		
		/// <summary>
		/// Get an array of integers. Designed for use with web-applications that use checkbox groups in a form
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32[]"/>
		/// </returns>
		/// <remarks>
		/// If any of the values cannot be converted to an integer, an empty array will be returned.
		/// </remarks>
		public static int[] GetCheckboxArrayInt(this NameValueCollection nvc, string key)
		{
			// get string version
			string[] strings = nvc.GetCheckboxArray(key);
			
			// prepare int version
			int[] ints = new int[strings.Length];
			
			// convert each string
			for (int i=0; i!=ints.Length; i++)
			{
				if (!int.TryParse(strings[i], out ints[i]))
				{
					// the value is invalid as an integer... return
					return new int[0];
				}
			}
			
			return ints;
		}
		#endregion Numbers
		
		#region Strings
		/// <summary>
		/// Get a value as a string
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// The <see cref="System.String"/> to return if the key does not exist
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public static string GetString(this NameValueCollection nvc, string key, string defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			return nvc[key];
		}
		
		/// <summary>
		/// Get a value as an array of strings. The value is split at line endings for known systems.
		/// Currently supports unix, mac and windows.
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>. If the key does not exist an empty array, <c>string[0]</c>, is returned
		/// </returns>
		public static string[] GetStringAsLines(this NameValueCollection nvc, string key)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return new string[0];
			}
			
			string[] s = nvc[key].Split(new string[]{"\r\n", "\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
			
			return s;
		}
		
		/// <summary>
		/// Get an array of strings. Designed for use with web-applications that use checkbox groups in a form
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>
		/// </returns>
		public static string[] GetCheckboxArray(this NameValueCollection nvc, string key)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return new string[0];
			}
			
			string[] val;
			
			val = nvc[key].Split(new char[]{','}, StringSplitOptions.None);
			
			return val;
		}
		
		/// <summary>
		/// Get an array of strings from keys following the form "key[n]".
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the "key" part of "key[n]"
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>
		/// </returns>
		public static string[] GetStringArray(this NameValueCollection nvc, string key)
		{
			List<string> entries = new List<string>();
			
			string[] keys = nvc.AllKeys;
			
			key = String.Concat(key, "[");
			
			foreach (string k in keys)
			{
				if (!k.StartsWith(key))
				{
					// not in the array
					continue;
				}
				
				if (nvc[k] != null)
				{
					entries.Add(nvc[k]);
				}
			}
			
			return entries.ToArray();
		}
		#endregion Strings
		
		#region DateTime
		/// <summary>
		/// Get a value as a DateTime. <see cref="DateTime.TryParse"/> is used on the key data.
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// A <see cref="DateTime"/> with the value to return if the key does not exist or the data behind it
		/// is not convertable to a DateTime
		/// </param>
		/// <returns>
		/// A <see cref="DateTime"/>
		/// </returns>
		public static DateTime GetDateTime(this NameValueCollection nvc, string key, DateTime defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			DateTime val = defaultValue;
			DateTime.TryParse(nvc[key], out val);
			
			return val;
		}
		#endregion DateTime
		
		#region Bool
		/// <summary>
		/// Get a value as a boolean.
		/// </summary>
		/// <param name="nvc">
		/// The <see cref="NameValueCollection"/>
		/// </param>
		/// <param name="key">
		/// A <see cref="System.String"/> with the key
		/// </param>
		/// <param name="defaultValue">
		/// The <see cref="System.Boolean"/> to return if the key does not exist or the data behind
		/// is invalid
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		/// <remarks>
		/// 1 and 'true' returns boolean true
		/// 0 and 'false' returns boolean false
		/// 
		/// The method is case-insensitive
		/// </remarks>
		public static bool GetBool(this NameValueCollection nvc, string key, bool defaultValue)
		{
			if (String.IsNullOrEmpty(nvc[key]))
			{
				return defaultValue;
			}
			
			string x = nvc[key].ToString().ToLower();
			
			return (x.Equals("true") || x.Equals("1"));
		}
		#endregion Bool
	}
}