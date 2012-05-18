using System;
using System.Diagnostics;
using ServiceStack.Text;

namespace tojsonperformanceconsoletest
{
	public static class ServiceStackTest
	{
		public static Stopwatch Run(object obj)
		{
			Stopwatch sw = new Stopwatch();
			
			sw.Start();
			obj.ToJson();
			sw.Stop();
			
			return sw;
		}
	}
}