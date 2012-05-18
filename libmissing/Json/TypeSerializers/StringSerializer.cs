using System;

namespace Missing.Json.TypeSerializers
{
	internal class StringSerializer : ITypeSerializer
	{
		public StringSerializer()
		{
		}
		
		
		#region ITypeSerializer implementation
		public string Serialize(object obj)
		{
			string res = obj.ToString();
			
			// we must use an intermediate replace, to avoid
			// having the backslash handling replace the control characters
			res = res.Replace("\\r\\n", "[N]");
			res = res.Replace("\\r", "[N]");
			res = res.Replace("\\n", "[N]");
			res = res.Replace("\"", "[Q]");
			res = res.Replace("\t", "[T]");
			res = res.Replace("\b", "[B]");
			res = res.Replace("\f", "[F]");
			
			// must be last to avoid replacing actual control characters
			res = res.Replace("\\", "\\\\");
			
			res = res.Replace("[N]", "\\n");
			res = res.Replace("[Q]", "\\\"");
			res = res.Replace("[T]", "\\t");
			res = res.Replace("[B]", "\\b");
			res = res.Replace("[F]", "\\f");
			
			return String.Format("\"{0}\"", res);
		}
		#endregion
	}
}