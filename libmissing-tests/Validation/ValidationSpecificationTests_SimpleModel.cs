using System;
using NUnit.Framework;
using Missing.Validation;
using System.Text.RegularExpressions;

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
		public string MyRegexString { get; set; }
		public string MyOnlyA { get; set; }
		public string MyHotdog { get; set; }
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
			
			base.Field(y => y.MyRegexString)
				.Allowed(new Regex("abc"));
			
			base.Field(y => y.MyOnlyA)
				.Allowed("a");
			
			base.Field(y => y.MyHotdog)
				.Allowed(new SimpleModelEnforcer());
			
			// this is invalid and is only
			// there to test handling of exceptions
			// from enforcers
			// 
			// as we cannot see whether a Long is
			// not set, including this field makes
			// all tests fail, as the enforcer is
			// always run
			//base.Field(y => y.MyLong)
			//	.AllowedEmail();
		}
	}
	
	public class SimpleModelEnforcer : Enforcer
	{
		#region implemented abstract members of Missing.Validation.Enforcer
		public override string Check(object input)
		{
			if (((string)input).Equals("Hotdog"))
			{
				return String.Empty;
			}
			
			return "Bad hotdog bro!";
		}
		#endregion
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
		
		[Test]
		public void SimpleModel_RegexValid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyRegexString = "abc abc abc";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void SimpleModel_RegexInvalid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyRegexString = "def def def";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyRegexString", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_AllowedCharactersValid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyOnlyA = "aaAAaAAaa";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void SimpleModel_AllowedCharactersInvalid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyOnlyA = "Annabelle";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyOnlyA", result.Errors[0].PropertyName, "The property name is wrong");
		}
		
		[Test]
		public void SimpleModel_CustomEnforcerValid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyHotdog = "Hotdog";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void SimpleModel_CustomEnforcerInvalid()
		{
			SimpleModel model = new SimpleModel();
			model.MyString = "Something";
			model.MyInt = 29;
			model.MyHotdog = "Burger";
			
			ValidationResult result = Validator.Validate<SimpleModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyHotdog", result.Errors[0].PropertyName, "The property name is wrong");
		}
	}
}