using System;
using System.Collections.Generic;

namespace Missing.Json.ClassAnalysis
{
	internal class ClassSpecificationCache : Dictionary<Type, ClassSpecification>
	{
		public ClassSpecificationCache() : base()
		{
		}
	}
}