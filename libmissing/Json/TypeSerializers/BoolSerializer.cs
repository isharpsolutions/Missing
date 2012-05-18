using System;

namespace Missing.Json.TypeSerializers
{
	internal class BoolSerializer : ITypeSerializer
	{
		public BoolSerializer()
		{
		}

		#region ITypeSerializer implementation
		public string Serialize(object obj)
		{
			return obj.ToString().ToLower();
		}
		#endregion
	}
}