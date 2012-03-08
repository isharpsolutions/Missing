using System;

namespace Missing.Network
{
	/// <summary>
	/// HTTP request methods... Used by <see cref="EasyHttpRequestBase"/>
	/// </summary>
	public enum HttpRequestMethod
	{
		/// <summary>
		/// A POST request
		/// </summary>
		Post = 1,
		
		/// <summary>
		/// A GET request
		/// </summary>
		Get = 2
	}
}