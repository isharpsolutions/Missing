using System;
using System.Net;

namespace Missing.Network
{
	/// <summary>
	/// Represents a response status (code + message)
	/// </summary>
	public class HttpRequestStatus
	{
		#region Static methods
		/// <summary>
		/// Create a Status instance from a string
		/// </summary>
		/// <param name="input">
		/// A <see cref="System.String"/> with an HTML compliant status message
		/// </param>
		/// <returns>
		/// A <see cref="Status"/> with the code and message
		/// </returns>
		public static HttpRequestStatus FromString(string input)
		{
			string[] x = input.Split(new char[]{' '}, 2);
			HttpRequestStatus s = new HttpRequestStatus(Convert.ToInt32(x[0]), x[1]);
			x = null;
			return s;
		}
		
		/// <summary>
		/// Create a status from custom headers like (cs-status-code)
		/// </summary>
		/// <param name="headers">
		/// A <see cref="WebHeaderCollection"/>
		/// </param>
		/// <param name="prefix">
		/// A <see cref="System.String"/> with the prefix (prefix-status-code)
		/// </param>
		/// <returns>
		/// A <see cref="Status"/>
		/// </returns>
		public static HttpRequestStatus FromHeaders(WebHeaderCollection headers, string prefix)
		{
			string code = String.Format("{0}-status-code", prefix);
			string msg = String.Format("{0}-status-message", prefix);
			
			HttpRequestStatus s = new HttpRequestStatus(Convert.ToInt32(headers[code]), headers[msg]);
			return s;
		}
		#endregion Static methods
		
		#region Fields
		private int code;
		private string message;
		#endregion Fields
		
		#region Constructors
		/// <summary>
		/// Create a new status for a response. A status contains
		/// a code (integer) and message (string). The code is fixed
		/// but the message can be altered if needed.
		/// </summary>
		/// <param name="code">
		/// A <see cref="System.Int32"/> with a machine readable status
		/// </param>
		/// <param name="message">
		/// A <see cref="System.String"/> with a human readable status
		/// </param>
		public HttpRequestStatus(int code, string message)
		{
			this.code = code;
			this.message = message;
		}
		#endregion Constructors
		
		#region Public methods
		/// <summary>
		/// Generate HTTP1.1 compliant status string
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> in the format "code message" as defined in HTTP1.1
		/// </returns>
		public string GetHtmlCompliantString()
		{
			return string.Format("{0} {1}", this.Code, this.Message);
		}
		#endregion Public methods
		
		#region Properties
		/// <summary>
		/// Get the status code
		/// </summary>
		public int Code
		{
			get
			{
				return this.code;
			}
		}
		
		/// <summary>
		/// Get/set the status message
		/// </summary>
		public string Message
		{
			get
			{
				return this.message;
			}
			
			set
			{
				this.message = value;
			}
		}
		#endregion Properties
	}
}