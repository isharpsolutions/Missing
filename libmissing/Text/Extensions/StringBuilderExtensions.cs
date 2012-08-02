using System;
using System.Text;

namespace Missing.Text.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="StringBuilder"/>
	/// </summary>
	public static class StringBuilderExtensions
	{
		/// <summary>
		/// Removes the last occurence of the given string - sort of.
		/// 
		/// In reality it just removes the last N characters from the string
		/// where N = toRemove.Length
		/// </summary>
		/// <param name="sb">
		/// The instance
		/// </param>
		/// <param name="toRemove">
		/// The string to remove
		/// </param>
		public static void RemoveLast(this StringBuilder sb, string toRemove)
		{
			int start = Math.Max(0, sb.Length-toRemove.Length);

			if ((start + toRemove.Length) > sb.Length)
			{
				return;
			}

			sb = sb.Remove(start, toRemove.Length);
		}
		
		/// <summary>
		/// Removes the last <see cref="System.Environment.NewLine"/>
		/// </summary>
		/// <param name="sb">
		/// The instance
		/// </param>
		/// <seealso cref="RemoveLast"/>
		public static void RemoveLastNewLine(this StringBuilder sb)
		{
			sb.RemoveLast(System.Environment.NewLine);
			
		}
		
		/// <summary>
		/// Removes the last comma
		/// </summary>
		/// <param name="sb">
		/// The instance
		/// </param>
		/// <seealso cref="RemoveLast"/>
		public static void RemoveLastComma(this StringBuilder sb)
		{
			sb.RemoveLast(",");
		}
	}
}