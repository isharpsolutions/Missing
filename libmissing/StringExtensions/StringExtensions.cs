using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.StringExtensions
{
	public static class StringExtensions
	{

		/// <summary>
		/// Determines whether this string contains only alpha lowercase symbols
		/// </summary>
		/// <param name="str">The string</param>
		/// <returns>
		///   <c>true</c> if [contains only alpha lowercase]; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsOnlyAlphaLowercase(this string str)
		{
			// convert to char array, then we can check the char values.
			var elements = (from y in str.ToArray<char>()
							where y < 97
							||
							y > 122
							select y);

			// if this is not empty, then we had values outside the a-z range
			return elements.Count() == 0;

		}

		/// <summary>
		/// Determines whether this string contains only alpha capital symbols
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns>
		///   <c>true</c> if [contains only alpha capital]; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsOnlyAlphaCapital(this string str)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether this string contains only numbers
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns>
		///   <c>true</c> if [contains only numbers] ; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsOnlyNumbers(this string str)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether this string contains only symbols
		/// </summary>
		/// <param name="str">The STR.</param>
		/// <returns>
		///   <c>true</c> if [contains only symbols]; otherwise, <c>false</c>.
		/// </returns>
		public static bool ContainsOnlySymbols(this string str)
		{
			throw new NotImplementedException();
		}
	}
}
