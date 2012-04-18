using System;
using NUnit.Framework;
using Missing.Validation;
using Missing.ValidatorTests.InvalidValues;

namespace Missing
{
	[TestFixture]
	public class ValidatorTests_InvalidValues
	{
		#region String
		[Test]
		public void String_Valid()
		{
			Simple model = new Simple();
			model.MyString = "I am valid";
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void String_Invalid_FirstEntry()
		{
			Simple model = new Simple();
			model.MyString = "invalid";
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		
		[Test]
		public void String_Invalid_SecondEntry()
		{
			Simple model = new Simple();
			model.MyString = "also invalid";
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion String
		
		#region Int
		[Test]
		public void Int_Valid()
		{
			Simple model = new Simple();
			model.MyInt = 1;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Int_Invalid_FirstEntry()
		{
			Simple model = new Simple();
			model.MyInt = 5;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyInt", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		
		[Test]
		public void Int_Invalid_SecondEntry()
		{
			Simple model = new Simple();
			model.MyInt = 9;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyInt", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion Int
		
		#region Long
		[Test]
		public void Long_Valid()
		{
			Simple model = new Simple();
			model.MyLong = 1L;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Long_Invalid_FirstEntry()
		{
			Simple model = new Simple();
			model.MyLong = 5L;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyLong", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		
		[Test]
		public void Long_Invalid_SecondEntry()
		{
			Simple model = new Simple();
			model.MyLong = 9L;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyLong", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion Long
		
		#region Decimal
		[Test]
		public void Decimal_Valid()
		{
			Simple model = new Simple();
			model.MyDecimal = 1.1m;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Decimal_Invalid_FirstEntry()
		{
			Simple model = new Simple();
			model.MyDecimal = 5.1m;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyDecimal", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		
		[Test]
		public void Decimal_Invalid_SecondEntry()
		{
			Simple model = new Simple();
			model.MyDecimal = 3.17m;
			
			ValidationResult result = Validator.Validate<Simple>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyDecimal", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion Decimal
	}
}

#region Classes
namespace Missing.ValidatorTests.InvalidValues
{
	public class Simple
	{
		public string MyString { get; set; }
		public int MyInt { get; set; }
		public long MyLong { get; set; }
		public decimal MyDecimal { get; set; }
	}
	
	public class SimpleValidationSpecification : ValidationSpecification<Simple>
	{
		public SimpleValidationSpecification()
		{
			base.Field(y => y.MyString)
				.Invalid("invalid")
				.Invalid("also invalid");
			
			base.Field(y => y.MyInt)
				.Invalid(5)
				.Invalid(9);
			
			base.Field(y => y.MyLong)
				.Invalid(5L)
				.Invalid(9L);
			
			base.Field(y => y.MyDecimal)
				.Invalid(5.1m)
				.Invalid(3.17m);
		}
	}
}
#endregion