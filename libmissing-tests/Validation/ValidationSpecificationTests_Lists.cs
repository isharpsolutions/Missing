using System;
using NUnit.Framework;
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
			Missing.ValidationSpecificationTests.Lists.ListModel model = new Missing.ValidationSpecificationTests.Lists.ListModel();
			model.ListItems.Add(new Missing.ValidationSpecificationTests.Lists.ListItem() {
				MyInt = 1,
				MyString = "something valid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Lists.ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void CustomItem_Invalid()
		{
			Missing.ValidationSpecificationTests.Lists.ListModel model = new Missing.ValidationSpecificationTests.Lists.ListModel();
			model.ListItems.Add(new Missing.ValidationSpecificationTests.Lists.ListItem() {
				MyInt = 5,
				MyString = "invalid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Lists.ListModel>(model);
			
			Assert.AreEqual(2, result.Errors.Count, "There should be 2 errors");
			
			Assert.AreEqual("ListItems[0].MyInt", result.Errors[0].PropertyPath, "First: property name is wrong");
			Assert.AreEqual("ListItems[0].MyString", result.Errors[1].PropertyPath, "Second: property name is wrong");
		}
		#endregion
		
		#region Primitive value
		[Test]
		public void PrimitiveValue_Valid()
		{
			Missing.ValidationSpecificationTests.Lists.ListModel model = new Missing.ValidationSpecificationTests.Lists.ListModel();
			model.Strings.Add("something valid");
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Lists.ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void PrimitiveValue_Invalid()
		{
			Missing.ValidationSpecificationTests.Lists.ListModel model = new Missing.ValidationSpecificationTests.Lists.ListModel();
			model.Strings.Add("invalid");
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Lists.ListModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("Strings[0]", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		#endregion
		
		#region Derived list
		[Test]
		public void DerivedList_CustomItem_Valid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyList.Add(new Missing.ValidationSpecificationTests.DerivedLists.ListItem() {
				MyInt = 1,
				MyString = "something valid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void DerivedList_CustomItem_Invalid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyList.Add(new Missing.ValidationSpecificationTests.DerivedLists.ListItem() {
				MyInt = 5,
				MyString = "invalid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.AreEqual(2, result.Errors.Count, "There should be 2 errors");
			
			Assert.AreEqual("MyList[0].MyInt", result.Errors[0].PropertyPath, "First: property name is wrong");
			Assert.AreEqual("MyList[0].MyString", result.Errors[1].PropertyPath, "Second: property name is wrong");
		}
		
		[Test]
		public void DerivedList_Primitive_Valid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyStrings.Add("valid");
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void DerivedList_Primitive_Invalid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyStrings.Add("invalid");
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyStrings[0]", result.Errors[0].PropertyPath, "The property name is wrong");
		}
		
		[Test]
		public void DerivedCollection_CustomItem_Valid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyCollection.Add(new Missing.ValidationSpecificationTests.DerivedLists.ListItem() {
				MyInt = 1,
				MyString = "something valid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void DerivedCollection_CustomItem_Invalid()
		{
			Missing.ValidationSpecificationTests.DerivedLists.ListModel model = new Missing.ValidationSpecificationTests.DerivedLists.ListModel();
			model.MyCollection.Add(new Missing.ValidationSpecificationTests.DerivedLists.ListItem() {
				MyInt = 5,
				MyString = "invalid"
			});
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.DerivedLists.ListModel>(model);
			
			Assert.AreEqual(2, result.Errors.Count, "There should be 2 errors");
			
			Assert.AreEqual("MyCollection[0].MyInt", result.Errors[0].PropertyPath, "First: property name is wrong");
			Assert.AreEqual("MyCollection[0].MyString", result.Errors[1].PropertyPath, "Second: property name is wrong");
		}
		#endregion
		
		#region Arrays
		[Test]
		public void Arrays_ComplexType_Valid()
		{
			Missing.ValidationSpecificationTests.Arrays.ArrayModel model = new Missing.ValidationSpecificationTests.Arrays.ArrayModel();
			model.ComplexTypeArray = new Missing.ValidationSpecificationTests.Arrays.ArrayItem[] {
				new Missing.ValidationSpecificationTests.Arrays.ArrayItem() {
					MyInt = 1,
					MyString = "something valid"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Arrays.ArrayModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Arrays_ComplexType_Invalid()
		{
			Missing.ValidationSpecificationTests.Arrays.ArrayModel model = new Missing.ValidationSpecificationTests.Arrays.ArrayModel();
			model.ComplexTypeArray = new Missing.ValidationSpecificationTests.Arrays.ArrayItem[] {
				new Missing.ValidationSpecificationTests.Arrays.ArrayItem() {
					MyInt = 5,
					MyString = "invalid"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Arrays.ArrayModel>(model);
			
			Assert.AreEqual(2, result.Errors.Count, "There should be 2 errors");
			
			Assert.AreEqual("ComplexTypeArray[0].MyInt", result.Errors[0].PropertyPath, "First: property name is wrong");
			Assert.AreEqual("ComplexTypeArray[0].MyString", result.Errors[1].PropertyPath, "Second: property name is wrong");
		}
		
		[Test]
		public void Arrays_Primitive_Valid()
		{
			Missing.ValidationSpecificationTests.Arrays.ArrayModel model = new Missing.ValidationSpecificationTests.Arrays.ArrayModel();
			model.Strings = new string[] { "valid" };
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Arrays.ArrayModel>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void Arrays_Primitive_Invalid()
		{
			Missing.ValidationSpecificationTests.Arrays.ArrayModel model = new Missing.ValidationSpecificationTests.Arrays.ArrayModel();
			model.Strings = new string[] { "invalid" };
			
			ValidationResult result = Validator.Validate<Missing.ValidationSpecificationTests.Arrays.ArrayModel>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("Strings[0]", result.Errors[0].PropertyPath, "First: property name is wrong");
		}
		#endregion
	}
}

#region Lists
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
			
			base.Field(y => y.Strings)
				.EachPrimitive<string>(x => {
					x.Value()
						.Required()
						.Invalid("invalid");
				});
		}
	}
}
#endregion

#region Derived Lists
namespace Missing.ValidationSpecificationTests.DerivedLists
{
	public class ListModel
	{
		public ListModel()
		{
			this.MyList = new DerivedList();
			this.MyStrings = new DerivedListPrimitive();
			this.MyCollection = new ListItemCollection();
		}
		
		public DerivedList MyList { get; set; }
		public DerivedListPrimitive MyStrings { get; set; }
		public ListItemCollection MyCollection { get; set; }
	}
	
	public class DerivedList : List<ListItem>
	{
	}
	
	public class DerivedListPrimitive : List<string>
	{
	}
	
	public class ListItemCollection : System.Collections.ObjectModel.Collection<ListItem>
	{
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
			base.Field(y => y.MyList)
				.Each<ListItem>(x => {
					x.Field(y => y.MyInt)
						.Invalid(5);
					
					x.Field(y => y.MyString)
						.Required()
						.Invalid("invalid");
				});
			
			base.Field(y => y.MyCollection)
				.Each<ListItem>(x => {
					x.Field(y => y.MyInt)
						.Invalid(5);
					
					x.Field(y => y.MyString)
						.Required()
						.Invalid("invalid");
				});
			
			base.Field(y => y.MyStrings)
				.EachPrimitive<string>(x => {
					x.Value()
						.Required()
						.Invalid("invalid");
				});
		}
	}
}
#endregion

#region Arrays
namespace Missing.ValidationSpecificationTests.Arrays
{
	public class ArrayItem
	{
		public int MyInt { get; set; }
		public string MyString { get; set; }
	}
	
	public class ArrayModel
	{
		public ArrayModel()
		{
			this.ComplexTypeArray = new ArrayItem[1];
			this.Strings = new string[1];
		}
		
		public ArrayItem[] ComplexTypeArray { get; set; }
		public string[] Strings { get; set; }
	}
	
	public class ArrayModelValidationSpecification : ValidationSpecification<ArrayModel>
	{
		public ArrayModelValidationSpecification()
		{
			base.Field(y => y.ComplexTypeArray)
				.Each<ArrayItem>(x => {
					x.Field(y => y.MyInt)
						.Invalid(5);
					
					x.Field(y => y.MyString)
						.Required()
						.Invalid("invalid");
				});
			
			base.Field(y => y.Strings)
				.EachPrimitive<string>(x => {
					x.Value()
						.Required()
						.Invalid("invalid");
				});
		}
	}
}
#endregion