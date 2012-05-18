using System;

namespace Missing.Json.TypeSerializers
{
	internal class Int32Serializer : ITypeSerializer
	{
		public Int32Serializer()
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