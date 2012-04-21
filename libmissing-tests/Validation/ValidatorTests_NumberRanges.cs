using System;
using NUnit.Framework;
using Missing.Validation;
using System.Collections.Generic;

namespace Missing
{
	[TestFixture]
	public class ValidatorTests_NumberRanges
	{
		#region Int
		[Test]
		public void Int_Valid()
		{
			Model model = new Model() {
				MyInt = 1,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new IntModelValidationSpecification());
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Int_TooLow()
		{
			Model model = new Model() {
				MyInt = 0,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new IntModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyInt", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void Int_TooHigh()
		{
			Model model = new Model() {
				MyInt = 10,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new IntModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyInt", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Long
		[Test]
		public void Long_Valid()
		{
			Model model = new Model() {
				MyLong = 1L,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new LongModelValidationSpecification());
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Long_TooLow()
		{
			Model model = new Model() {
				MyLong = 0L,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new LongModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyLong", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void Long_TooHigh()
		{
			Model model = new Model() {
				MyLong = 10L,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new LongModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyLong", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Float
		[Test]
		public void Float_Valid()
		{
			Model model = new Model() {
				MyFloat = 1f,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new FloatModelValidationSpecification());
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Float_TooLow()
		{
			Model model = new Model() {
				MyFloat = 0f,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new FloatModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyFloat", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void Float_TooHigh()
		{
			Model model = new Model() {
				MyFloat = 10f,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new FloatModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyFloat", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Double
		[Test]
		public void Double_Valid()
		{
			Model model = new Model() {
				MyDouble = 1d,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DoubleModelValidationSpecification());
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Double_TooLow()
		{
			Model model = new Model() {
				MyDouble = 0d,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DoubleModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyDouble", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void Double_TooHigh()
		{
			Model model = new Model() {
				MyDouble = 10d,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DoubleModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyDouble", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Decimal
		[Test]
		public void Decimal_Valid()
		{
			Model model = new Model() {
				MyDecimal = 1m,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DecimalModelValidationSpecification());
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Decimal_TooLow()
		{
			Model model = new Model() {
				MyDecimal = 0m,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DecimalModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyDecimal", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void Decimal_TooHigh()
		{
			Model model = new Model() {
				MyDecimal = 10m,
			};
			
			ValidationResult result = Validator.Validate<Model>(model, new DecimalModelValidationSpecification());
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			Assert.AreEqual("MyDecimal", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Stuff
		private class Model
		{
			public int MyInt { get; set; }
			public long MyLong { get; set; }
			public float MyFloat { get; set; }
			public double MyDouble { get; set; }
			public decimal MyDecimal { get; set; }
		}
		
		private class IntModelValidationSpecification : ValidationSpecification<Model>
		{
			public IntModelValidationSpecification()
			{
				base.Field(y => y.MyInt).Range(1, 5);
			}
		}
		
		private class LongModelValidationSpecification : ValidationSpecification<Model>
		{
			public LongModelValidationSpecification()
			{
				base.Field(y => y.MyLong).Range(1L, 5L);
			}
		}
		
		private class FloatModelValidationSpecification : ValidationSpecification<Model>
		{
			public FloatModelValidationSpecification()
			{
				base.Field(y => y.MyFloat).Range(1f, 5f);
			}
		}
		
		private class DoubleModelValidationSpecification : ValidationSpecification<Model>
		{
			public DoubleModelValidationSpecification()
			{
				base.Field(y => y.MyDouble).Range(1d, 5d);
			}
		}
		
		private class DecimalModelValidationSpecification : ValidationSpecification<Model>
		{
			public DecimalModelValidationSpecification()
			{
				base.Field(y => y.MyDecimal).Range(1m, 5m);
			}
		}
		#endregion Stuff
	}
}