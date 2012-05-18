using System;

namespace Missing.Json.TypeSerializers
{
	internal class SingleSerializer : ITypeSerializer
	{
		public SingleSerializer()
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