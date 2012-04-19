using System;
using NUnit.Framework;
using Missing.Validation;
using System.Collections.Generic;

namespace Missing
{
	[TestFixture]
	public class ValidatorTests_ListFields
	{
		#region Complex list
		[Test]
		public void ComplexList_Valid()
		{
			Missing.ValidatorTests.ListFields.ComplexList.Model model = new Missing.ValidatorTests.ListFields.ComplexList.Model() {
				MyList = new List<ListItem>() {
					new ListItem() {
						String = "valid"
					}
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexList.Model>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void ComplexList_Invalid_Null()
		{
			Missing.ValidatorTests.ListFields.ComplexList.Model model = new Missing.ValidatorTests.ListFields.ComplexList.Model();
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void ComplexList_Invalid_Empty()
		{
			Missing.ValidatorTests.ListFields.ComplexList.Model model = new Missing.ValidatorTests.ListFields.ComplexList.Model() {
				MyList = new List<ListItem>()
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void ComplexList_Invalid_TooManyElements()
		{
			Missing.ValidatorTests.ListFields.ComplexList.Model model = new Missing.ValidatorTests.ListFields.ComplexList.Model() {
				MyList = new List<ListItem>() {
					new ListItem() { String = "a" },
					new ListItem() { String = "b" },
					new ListItem() { String = "c" }
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Primitive list
		[Test]
		public void PrimitiveList_Valid()
		{
			Missing.ValidatorTests.ListFields.PrimitiveList.Model model = new Missing.ValidatorTests.ListFields.PrimitiveList.Model() {
				MyPrimitiveList = new List<string>() {
					"a"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveList.Model>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void PrimitiveList_Invalid_Null()
		{
			Missing.ValidatorTests.ListFields.PrimitiveList.Model model = new Missing.ValidatorTests.ListFields.PrimitiveList.Model();
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void PrimitiveList_Invalid_Empty()
		{
			Missing.ValidatorTests.ListFields.PrimitiveList.Model model = new Missing.ValidatorTests.ListFields.PrimitiveList.Model() {
				MyPrimitiveList = new List<string>()
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void PrimitiveList_Invalid_TooManyElements()
		{
			Missing.ValidatorTests.ListFields.PrimitiveList.Model model = new Missing.ValidatorTests.ListFields.PrimitiveList.Model() {
				MyPrimitiveList = new List<string>() {
					"a",
					"b",
					"c"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveList.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveList", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Complex array
		[Test]
		public void ComplexArray_Valid()
		{
			Missing.ValidatorTests.ListFields.ComplexArray.Model model = new Missing.ValidatorTests.ListFields.ComplexArray.Model() {
				MyListItemArray = new ListItem[] {
					new ListItem() {
						String = "a"
					}
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexArray.Model>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void ComplexArray_Invalid_Null()
		{
			Missing.ValidatorTests.ListFields.ComplexArray.Model model = new Missing.ValidatorTests.ListFields.ComplexArray.Model();
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyListItemArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void ComplexArray_Invalid_Empty()
		{
			Missing.ValidatorTests.ListFields.ComplexArray.Model model = new Missing.ValidatorTests.ListFields.ComplexArray.Model() {
				MyListItemArray = new ListItem[0]
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyListItemArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void ComplexArray_Invalid_TooManyElements()
		{
			Missing.ValidatorTests.ListFields.ComplexArray.Model model = new Missing.ValidatorTests.ListFields.ComplexArray.Model() {
				MyListItemArray = new ListItem[] {
					new ListItem() {
						String = "a"
					},
					new ListItem() {
						String = "b"
					},
					new ListItem() {
						String = "c"
					}
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.ComplexArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyListItemArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
		
		#region Primitive array
		[Test]
		public void PrimitiveArray_Valid()
		{
			Missing.ValidatorTests.ListFields.PrimitiveArray.Model model = new Missing.ValidatorTests.ListFields.PrimitiveArray.Model() {
				MyPrimitiveArray = new string[] {
					"a"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveArray.Model>(model);
			
			Assert.IsFalse(result.HasErrors(), "There should not be any errors");
		}
		
		[Test]
		public void PrimitiveArray_Invalid_Null()
		{
			Missing.ValidatorTests.ListFields.PrimitiveArray.Model model = new Missing.ValidatorTests.ListFields.PrimitiveArray.Model();
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void PrimitiveArray_Invalid_Empty()
		{
			Missing.ValidatorTests.ListFields.PrimitiveArray.Model model = new Missing.ValidatorTests.ListFields.PrimitiveArray.Model() {
				MyPrimitiveArray = new string[0]
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		
		[Test]
		public void PrimitiveArray_Invalid_TooManyElements()
		{
			Missing.ValidatorTests.ListFields.PrimitiveArray.Model model = new Missing.ValidatorTests.ListFields.PrimitiveArray.Model() {
				MyPrimitiveArray = new string[] {
					"a",
					"b",
					"c"
				}
			};
			
			ValidationResult result = Validator.Validate<Missing.ValidatorTests.ListFields.PrimitiveArray.Model>(model);
			
			Assert.AreEqual(1, result.Errors.Count, "There should be 1 error");
			
			Assert.AreEqual("MyPrimitiveArray", result.Errors[0].PropertyPath, "The property path is wrong");
		}
		#endregion
	}
	
	#region List item
	public class ListItem
	{
		public string String { get; set; }
	}
	#endregion
}

#region ComplexList
namespace Missing.ValidatorTests.ListFields.ComplexList
{
	public class Model
	{
		public List<ListItem> MyList { get; set; }
	}
	
	public class ModelValidationSpecification : ValidationSpecification<Model>
	{
		public ModelValidationSpecification()
		{
			base.Field(y => y.MyList)
					.Required()
					.Length(2)
					.NotEmpty();
		}
	}
}
#endregion

#region PrimitiveList
namespace Missing.ValidatorTests.ListFields.PrimitiveList
{
	public class Model
	{
		public List<string> MyPrimitiveList { get; set; }
	}
	
	public class ModelValidationSpecification : ValidationSpecification<Model>
	{
		public ModelValidationSpecification()
		{
			base.Field(y => y.MyPrimitiveList)
					.Required()
					.Length(2)
					.NotEmpty();
		}
	}
}
#endregion

#region ComplexArray
namespace Missing.ValidatorTests.ListFields.ComplexArray
{
	public class Model
	{
		public ListItem[] MyListItemArray { get; set; }
	}
	
	public class ModelValidationSpecification : ValidationSpecification<Model>
	{
		public ModelValidationSpecification()
		{
			base.Field(y => y.MyListItemArray)
					.Required()
					.Length(2)
					.NotEmpty();
		}
	}
}
#endregion

#region PrimitiveArray
namespace Missing.ValidatorTests.ListFields.PrimitiveArray
{
	public class Model
	{
		public string[] MyPrimitiveArray { get; set; }
	}
	
	public class ModelValidationSpecification : ValidationSpecification<Model>
	{
		public ModelValidationSpecification()
		{
			base.Field(y => y.MyPrimitiveArray)
					.Required()
					.Length(2)
					.NotEmpty();
		}
	}
}
#endregion