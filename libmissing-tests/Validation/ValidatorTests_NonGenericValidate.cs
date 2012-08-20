using System;
using NUnit.Framework;
using Missing.Validation;
using Missing.ValidatorTests.NonGenericValidate;

namespace Missing
{
	[TestFixture]
	public class ValidatorTests_NonGenericValidate
	{
		#region String
		[Test]
		public void ValSpecExists()
		{
			Simple model = new Simple();
			model.MyString = "I am valid";
			
			ValidationResult result = Validator.Validate(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void ValSpecDoesNotExist()
		{
			Two model = new Two();
			model.MyString = "invalid";
			
			try
			{
				Validator.Validate(model);
				Assert.Fail("An UnableToFindValidationSpecificationException should have been thrown");
			}
			catch (UnableToFindValidationSpecificationException)
			{
				// good
			}
		}
		#endregion String
	}
}

#region Classes
namespace Missing.ValidatorTests.NonGenericValidate
{
	public class Simple
	{
		public string MyString { get; set; }
	}
	
	public class SimpleValidationSpecification : ValidationSpecification<Simple>
	{
		public SimpleValidationSpecification()
		{
			base.Field(y => y.MyString)
				.Invalid("invalid")
				.Invalid("also invalid");
		}
	}
	
	public class Two
	{
		public string MyString { get; set; }
	}
}
#endregion