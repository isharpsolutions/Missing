using System;
using NUnit.Framework;
using Missing.ValidationSpecificationTests.Lists;
using System.Collections.Generic;
using Missing.Validation;

namespace Missing
{
	[TestFixture]
	public class ValidationSpecificationTests_Lists
	{
		#region Custom items
		[Test]
		public void CustomItem_Valid()
		{
			ListModel model = new ListModel();
			model.ListItems.Add(new ListItem() {
				MyInt = 1,
				MyString = "something valid"
			});
			
			ValidationResult result = Validator.Validate<ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void CustomItem_Invalid()
		{
			ListModel model = new ListModel();
			model.ListItems.Add(new ListItem() {
				MyInt = 5,
				MyString = "invalid"
			});
			
			ValidationResult result = Validator.Validate<ListModel>(model);
			
			Assert.AreEqual(2, result.Errors.Count, "There should be 2 errors");
			
			Assert.AreEqual("ListItems[0].MyInt", result.Errors[0].PropertyPath, "First: property name is wrong");
			Assert.AreEqual("ListItems[0].MyString", result.Errors[1].PropertyPath, "Second: property name is wrong");
		}
		#endregion
		
		#region Primitive value
		[Test]
		public void PrimitiveValue_Valid()
		{
			ListModel model = new ListModel();
			model.Strings.Add("something valid");
			
			ValidationResult result = Validator.Validate<ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void PrimitiveValue_Invalid()
		{
			ListModel model = new ListModel();
			model.Strings.Add("invalid");
			
			ValidationResult result = Validator.Validate<ListModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("Strings[0]", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion
	}
}

#region Classes
namespace Missing.ValidationSpecificationTests.Lists
{
	public class ListModel
	{
		public ListModel()
		{
			this.ListItems = new List<ListItem>();
			this.Strings = new List<string>();
		}
		
		public List<ListItem> ListItems { get; set; }
		public List<string> Strings { get; set; }
	}
	
	public class ListItem
	{
		public int MyInt { get; set; }
		public string MyString { get; set; }
	}
	
	public class ListModelValidationSpecification : ValidationSpecification<ListModel>
	{
		public ListModelValidationSpecification()
		{
			base.Field(y => y.ListItems)
				.Each<ListItem>(x => {
					x.Field(y => y.MyInt)
						.Invalid(5);
					
					x.Field(y => y.MyString)
						.Required()
						.Invalid("invalid");
				});
			
			/*
			base.Field(y => y.Strings)
				.Each<string>(x => {
					
				});
			*/
		}
	}
}
#endregion Classes