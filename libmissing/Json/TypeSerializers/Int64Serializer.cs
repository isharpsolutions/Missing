using System;

namespace Missing.Json.TypeSerializers
{
	internal class Int64Serializer : ITypeSerializer
	{
		public Int64Serializer()
		{
		}

		#region ITypeSerializer implementation
		public string Serialize(object obj)
		{
			return obj.ToString();
		}
		#endregion
	}
}