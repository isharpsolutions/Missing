using System;
using NUnit.Framework;
using Missing.Validation;

namespace Missing
{
	#region Model
	public class SimpleModel
	{
		public SimpleModel()
		{
		}
		
		public string MyString { get; set; }
		public int MyInt { get; set; }
		public long MyLong { get; set; }
		public bool MyBool { get; set; }
		public decimal MyDecimal { get; set; }
		public float MyFloat { get; set; }
		public double MyDouble { get; set; }
		
		public string MyEmail { get; set; }
	}
	#endregion Model
	
	#region Validation spec
	public class SimpleModelValidationSpecification : ValidationSpecification<SimpleModel>
	{
		public SimpleModelValidationSpecification()
		{
			base.Field(y => y.MyString)
				.Required()
				.Length(10);
			
			base.Field(y => y.MyInt)
				.Required();
			
			base.Field(y => y.MyEmail)
				.AllowedEmail();
			
			//
			// make a test where email is not required
			// but still calls AllowedEmail
			//
			// not calling required should make the
			// field continue validation only if
			// the field contains a value
			//
		}
	}
	#endregion Validation spec
	
	
	[TestFixture]
	public class ValidationSpecificationTests_SimpleModel
	{
		[Test]
		public void SimpleModel_AllValid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			
			// MyEmail is not required, hence we do not specify it
			// model.MyEmail = "user@domain.tld";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void SimpleModel_StringIsNull()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = null;
			model.MyInt = 29;
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_StringIsEmpty()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = String.Empty;
			model.MyInt = 29;
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_StringIsWhiteSpace()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "   ";
			model.MyInt = 29;
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_StringTooLong()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Very long and annoying string that will never ever pass validation";
			model.MyInt = 29;
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyString", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_EmailValid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyEmail = "user@domain.tld";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void SimpleModel_EmailInvalid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyEmail = "user";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyEmail", result.Errors[0].PropertyName, "The property name is wrong");
		}
	}
}