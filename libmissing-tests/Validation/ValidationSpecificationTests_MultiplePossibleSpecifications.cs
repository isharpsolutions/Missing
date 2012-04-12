using System;
using NUnit.Framework;
using Missing.Validation;

namespace Missing
{
	[TestFixture]
	public class ValidationSpecificationTests_MultiplePossibleSpecifications
	{
		[Test]
		public void MultiplePossibleSpecifications_FindBySameNamespace()
		{
			Missing.Multiple.One entity = new Missing.Multiple.One();
			
			Validator.Validate<Missing.Multiple.One>(entity);
		}
	}
}

#region One
namespace Missing.Multiple.One
{
	public class RepeatedClassName
	{
		public string MyString { get; set; }
	}
	
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
		}
	}
}
#endregion

#region Two
namespace Missing.Multiple.Two
{
	public class RepeatedClassName
	{
		public string MyString { get; set; }
	}
	
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
		}
	}
}
#endregion