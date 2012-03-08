using System;

namespace Missing.Network
{
	/// <summary>
	/// Factory for <see cref="EasyHttpRequest"/>
	/// </summary>
	public static class EasyHttpRequestFactory
	{
		/// <summary>
		/// Get an instance of a request class
		/// </summary>
		/// <returns>
		/// The instance
		/// </returns>
		/// <param name="method">
		/// The method that the request class should implement
		/// </param>
		/// <exception cref="NotSupportedException">
		/// Thrown if the given method has yet to be supported.
		/// </exception>
		public static EasyHttpRequest GetInstance(HttpRequestMethod method)
		{
			switch (method)
			{
				case HttpRequestMethod.Post:
				{
					return new PostEasyHttpRequest();
				}
				
				case HttpRequestMethod.Get:
				{
					return new GetEasyHttpRequest();
				}
				
				default:
				{
					throw new NotSupportedException(String.Format("The HttpRequestMethod '{0}' is not supported yet", method));
				}
			}
		}
	}
}