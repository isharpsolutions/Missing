using System;

namespace Missing.Json
{
	public static class ToJsonExtension
	{
		private static JsonSerializer serializer = new JsonSerializer();
		
		public static string ToJson(this object obj)
		{
			return serializer.Serialize(obj);
		}
	}
}