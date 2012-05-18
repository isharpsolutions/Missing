using System;
using System.Diagnostics;
using Missing.Json;

namespace tojsonperformanceconsoletest
{
	public static class MissingTest
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