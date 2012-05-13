using System;
using NUnit.Framework;
using Missing.Network;

namespace Missing
{
	[TestFixture]
	public class EasyHttpRequest_GetTests
	{
		[Test]
		public void UrlWithoutQueryString()
		{
			EasyHttpRequest request = EasyHttpRequestFactory.GetInstance(HttpRequestMethod.Get);
			request.Url = "http://google.com";
			
			request.Send();
		}
		
		[Test]
		public void UrlWithQueryString_AddData()
		{
			EasyHttpRequest request = EasyHttpRequestFactory.GetInstance(HttpRequestMethod.Get);
			request.Url = "http://www.youtube.com/watch";
			
			request.AddData("v", "4ikH9ZRcF2Q");
			
			request.Send();
		}
		
		[Test]
		public void UrlWithQueryString_InUrl()
		{
			EasyHttpRequest request = EasyHttpRequestFactory.GetInstance(HttpRequestMethod.Get);
			request.Url = "http://www.youtube.com/watch?v=4ikH9ZRcF2Q";
			
			request.Send();
		}
	}
}