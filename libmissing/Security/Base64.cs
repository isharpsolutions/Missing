using System;
using System.Text;

namespace Missing.Security
{
	/// <summary>
	/// Base64 encoding helper
	/// </summary>
	public static class Base64
	{
		#region Encode
		/// <summary>
		/// Base64 encode the given plaintext string
		/// </summary>
		/// <param name="plaintext">The plaintext string to encode</param>
		/// <param name="encoding">The charset encoding of the plaintext</param>
		public static string Encode(string plaintext, Encoding encoding)
		{
			return System.Convert.ToBase64String(encoding.GetBytes(plaintext));
		}

		/// <summary>
		/// Base64 encode the given plaintext string using <see cref="Encoding.UTF8"/>
		/// </summary>
		/// <param name="plaintext">The plaintext string to encode</param>
		public static string Encode(string plaintext)
		{
			return Encode(plaintext, Encoding.UTF8);
		}
		#endregion Encode

		#region Decode
		/// <summary>
		/// Decode the given base64 encoded string
		/// </summary>
		/// <param name="encodedtext">The encodedtext string to decode</param>
		/// <param name="encoding">The charset encoding of the original plaintext</param>
		public static string Decode(string encodedtext, Encoding encoding)
		{
			return encoding.GetString(System.Convert.FromBase64String(encodedtext));
		}

		/// <summary>
		/// Decode the given base64 encoded string using <see cref="Encoding.UTF8"/>
		/// </summary>
		/// <param name="encodedtext">The encodedtext string to decode</param>
		public static string Decode(string encodedtext)
		{
			return Decode(encodedtext, Encoding.UTF8);
		}
		#endregion Decode
	}
}