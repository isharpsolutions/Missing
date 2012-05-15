using System;
using Missing.Network;
using Missing.Diagnostics;

namespace easyhttprequestconsoletest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Log.Use().SimpleConsoleColored();
			
			EasyHttpRequest req = EasyHttpRequestFactory.GetInstance(HttpRequestMethod.Get);
			req.Url = "http://lorebook.lotro.com/wiki/Special:LotroResource?id=1879145635";
			
			req.Send();
		}
	}
}