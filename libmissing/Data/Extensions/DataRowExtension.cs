using System;
using System.Data;

namespace Missing.Data.Extensions
{
	public static class DataRowExtension
	{		
		public static long GetLong(this DataRow row, string colName, long def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(string.Empty))
			{
				return def;
			}
			
			return Int64.Parse(data);
		}
		
		
		public static int GetInt(this DataRow row, string colName, int def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(string.Empty))
			{
				return def;
			}
			
			return Int32.Parse(data);
		}
		
		public static bool GetBool(this DataRow row, string colName, bool def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(string.Empty))
			{
				return def;
			}
			
			return Boolean.Parse(data);
		}
		
		public static DateTime GetDateTime(this DataRow row, string colName, DateTime def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(string.Empty))
			{
				return def;
			}
			
			return DateTime.Parse(data);
		}
		
		public static Decimal GetDecimal(this DataRow row, string colName, Decimal def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(String.Empty))
			{
				return def;
			}
			
			return Decimal.Parse(data);
		}
		
		public static Double GetDouble(this DataRow row, string colName, Double def)
		{
			string data = GetString(row, colName, String.Empty);
			if (data.Equals(String.Empty))
			{
				return def;
			}
			
			return Double.Parse(data);
		}
		
		/// <summary>
		/// Returns a string instance from the datarow, by extracting the value store in column <see cref="colName"/>
		/// </summary>
		/// <param name="row">
		/// A <see cref="DataRow"/>
		/// </param>
		/// <param name="colName">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="def">
		/// A <see cref="System.String"/> containing the value to return, if the stored value is null
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public static string GetString(this DataRow row, string colName, string def)
		{
			if (row[colName] == DBNull.Value)
			{
				return def;
			}
			
			return Convert.ToString(row[colName]);
		}
	}
}