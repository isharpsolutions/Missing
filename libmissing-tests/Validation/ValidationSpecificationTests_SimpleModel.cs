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
			this.Info = new ContactInfo();
		}
		
		public ContactInfo Info { get; set; }
	}
	#endregion Model
	
	#region Validation spec
	public class SimpleModelValidationSpecification : ValidationSpecification<SimpleModel>
	{
		public SimpleModelValidationSpecification()
		{
			base.Field(y => y.Info.Name).Required();
		}
	}
	#endregion Validation spec
	
	[TestFixture]
	public class ValidationSpecificationTests_SimpleModel
	{
		[Test]
		public void Simple()
		{
			SimpleModelValidationSpecification spec = new SimpleModelValidationSpecification();
			
			Assert.AreEqual(1, spec.Properties.Count, "There should be 1 property");
			Assert.IsTrue(spec.Properties[0].IsRequired, "The property should be flagged as required");
			
			Assert.AreEqual(2, spec.Properties[0].PropertyPath.Count, "Path should have 2 elements");
			Assert.AreEqual("Info", spec.Properties[0].PropertyPath[0], "First part of the path is wrong");
			Assert.AreEqual("Name", spec.Properties[0].PropertyPath[1], "Second part of the path is wrong");
		}
	}
}

