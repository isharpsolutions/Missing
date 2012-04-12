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
			return CheckString(str, y=> y < 'a' || y > 'z');
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
			return CheckString(str, y => y < 'A' || y > 'Z');
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
			return CheckString(str, y => y < '0' || y > '9');
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

		/// <summary>
		/// Checks the expression <paramref name="pred"/> agains <paramref name="str"/> 
		/// </summary>
		/// <remarks>
		/// <paramref name="str"/> is converted to a char array, so the Predicate <paramref name="pred"/>
		/// should do magic with chars
		/// </remarks>
		/// <param name="str">The STR.</param>
		/// <param name="pred">The pred.</param>
		/// <returns></returns>
		private static bool CheckString(string str, Predicate<char> pred)
		{
			var elements = (from y in str.ToArray<char>()
							where pred(y)
							select y);

			return elements.Count() == 0;
		}
	}
}
