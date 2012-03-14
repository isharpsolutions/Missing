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
	}
	#endregion Model
	
	#region Validation spec
	public class SimpleModelValidationSpecification : ValidationSpecification<SimpleModel>
	{
		public SimpleModelValidationSpecification()
		{
			base.Field(y => y.MyString).Required();
			base.Field(y => y.MyInt).Required();
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
	}
}