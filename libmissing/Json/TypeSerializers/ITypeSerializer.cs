using System;

namespace Missing.Json.TypeSerializers
{
	internal interface ITypeSerializer
	{
		string Serialize(object obj);
	}
}