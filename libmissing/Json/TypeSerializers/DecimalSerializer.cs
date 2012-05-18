using System;

namespace Missing.Json.TypeSerializers
{
	internal class DecimalSerializer : ITypeSerializer
	{
		public DecimalSerializer()
		{
		}

		#region ITypeSerializer implementation
		public string Serialize(object obj)
		{
			string res = obj.ToString();
			res = res.Replace(",", ".");
			
			return res;
		}
		#endregion
	}
}