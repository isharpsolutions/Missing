using System;
using NUnit.Framework;
using Missing.Validation;
using Missing.ValidatorTests.InvalidValues;
using ValidatorTestsIfSpecExists;

namespace Missing
{
	[TestFixture]
	public class ValidatorTests_IfSpecExists
	{
		#region Using the generic method
		[Test]
		public void HasSpec_Valid_Generic()
		{
			var input = new InputWithSpec() {
				String = "Valid"
			};

			var res = Validator.ValidateIfSpecExists<InputWithSpec>(input);

			Assert.IsFalse(res.HasErrors());
		}

		[Test]
		public void HasSpec_Invalid_Generic()
		{
			var input = new InputWithSpec() {
				String = String.Empty
			};

			var res = Validator.ValidateIfSpecExists<InputWithSpec>(input);

			Assert.IsTrue(res.HasErrors());
		}

		[Test]
		public void NoSpec_Generic()
		{
			var input = new HasNoSpec() {
				String = "Valid"
			};

			var res = Validator.ValidateIfSpecExists<HasNoSpec>(input);

			Assert.IsFalse(res.HasErrors());
		}
		#endregion

		#region Using the object method
		[Test]
		public void HasSpec_Valid_Object()
		{
			var input = new InputWithSpec() {
				String = "Valid"
			};

			var res = Validator.ValidateIfSpecExists(input);

			Assert.IsFalse(res.HasErrors());
		}

		[Test]
		public void HasSpec_Invalid_Object()
		{
			var input = new InputWithSpec() {
				String = String.Empty
			};

			var res = Validator.ValidateIfSpecExists(input);

			Assert.IsTrue(res.HasErrors());
		}

		[Test]
		public void NoSpec_Object()
		{
			var input = new HasNoSpec() {
				String = "Valid"
			};

			var res = Validator.ValidateIfSpecExists(input);

			Assert.IsFalse(res.HasErrors());
		}
		#endregion
	}
}




namespace ValidatorTestsIfSpecExists
{
	public class InputWithSpec
	{
		public string String { get; set; }
	}

	public class InputWithSpecValidationSpecification : ValidationSpecification<InputWithSpec>
	{
		public InputWithSpecValidationSpecification()
		{
			base.Field(yy => yy.String).Required();
		}
	}

	public class HasNoSpec
	{
		public string String { get; set; }
	}
}