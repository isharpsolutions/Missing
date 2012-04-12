using System;
using NUnit.Framework;
using Missing.Validation;

namespace Missing
{
	[TestFixture]
	public class ValidationSpecificationTests_MultipleSpecifications
	{
		[Test]
		public void MultipleSpecifications_SameNamespace_DifferentName()
		{
			Missing.Multiple.One.RepeatedClassName entity = new Missing.Multiple.One.RepeatedClassName();
			
			Assert.IsEmpty(Validator.Validate<Missing.Multiple.One.RepeatedClassName>(entity).Errors, "There should not be any errors");
		}
		
		[Test]
		public void MultipleSpecifications_SubNamespace_SameName()
		{
			Missing.Multiple.Two.RepeatedClassName entity = new Missing.Multiple.Two.RepeatedClassName();
			
			Assert.IsEmpty(Validator.Validate<Missing.Multiple.Two.RepeatedClassName>(entity).Errors, "There should not be any errors");
		}
		
		[Test]
		public void MultipleSpecifications_DifferentNamespace_SameName()
		{
			Missing.Multiple.Three.RepeatedClassName entity = new Missing.Multiple.Three.RepeatedClassName();
			
			Assert.IsEmpty(Validator.Validate<Missing.Multiple.Three.RepeatedClassName>(entity).Errors, "There should not be any errors");
		}
		
		[Test]
		public void MultipleSpecifications_AllCombined()
		{
			Missing.Multiple.Four.RepeatedClassName entity = new Missing.Multiple.Four.RepeatedClassName();
			
			Assert.IsEmpty(Validator.Validate<Missing.Multiple.Four.RepeatedClassName>(entity).Errors, "There should not be any errors");
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
	
	public class OtherRepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public OtherRepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
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

namespace Missing.Multiple.Two.Sub
{
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
		}
	}
}
#endregion

#region Three
namespace Missing.Multiple.Three
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

namespace Missing.Multiple.ThreeOther
{
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<Missing.Multiple.Three.RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
		}
	}
}
#endregion

#region Four
namespace Missing.Multiple.Four
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
	
	public class OtherRepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public OtherRepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
		}
	}
}

namespace Missing.Multiple.Four.Sub
{
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
		}
	}
}

namespace Missing.Multiple.FourOther
{
	public class RepeatedClassNameValidationSpecification : ValidationSpecification<Missing.Multiple.Four.RepeatedClassName>
	{
		public RepeatedClassNameValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
		}
	}
}
#endregion