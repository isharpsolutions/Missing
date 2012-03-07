using System;
using NUnit.Framework;
using System.Collections.Specialized;
using Missing.Collections.Extensions;

namespace Missing
{
	/// <summary>
	/// Tests the extension methods for the NameValueCollection
	/// </summary>
	[TestFixture]
	public class NameValueCollectionExtensionsTest
	{
		private NameValueCollection nvc;
		
		#region SetUp / TearDown
		[SetUp]
		public void SetUp()
		{
			this.nvc = new NameValueCollection(50);
			
			this.nvc.Add("int_valid", "4");
			this.nvc.Add("int_invalid", "hest");
			
			this.nvc.Add("long_valid", "4");
			this.nvc.Add("long_invalid", "hest");
			
			this.nvc.Add("string_valid", "valid");
			
			this.nvc.Add("stringaslines_valid_unix", "line 1\nline 2");
			this.nvc.Add("stringaslines_valid_mac", "line 1\rline 2");
			this.nvc.Add("stringaslines_valid_win", "line 1\r\nline 2");
			this.nvc.Add("stringaslines_valid_mixed", "line 1\nline 2\rline 3\r\nline 4");
			this.nvc.Add("stringaslines_valid_singleline", "line 1");
			
			this.nvc.Add("datetime_valid", (new DateTime(2011, 1, 12, 14, 17, 0)).ToString());
			this.nvc.Add("datetime_invalid", "hest");
			
			this.nvc.Add("bool_valid_1", "1");
			this.nvc.Add("bool_valid_true", "true");
			this.nvc.Add("bool_valid_true_uc", "TRUE");
			this.nvc.Add("bool_valid_0", "0");
			this.nvc.Add("bool_valid_false", "false");
			this.nvc.Add("bool_valid_false_uc", "FALSE");
			this.nvc.Add("bool_invalid", "hest");
			
			this.nvc.Add("decimal_valid_simple", "2.0");
			this.nvc.Add("decimal_valid_negative", "-4.65");
			this.nvc.Add("decimal_valid_negative_spaceaftersign", "- 4.65");
			this.nvc.Add("decimal_valid_negative_zero", "-0.0");
			this.nvc.Add("decimal_valid_positive_sign", "+4.29");
			this.nvc.Add("decimal_valid_positive_spaceaftersign", "+ 4.29");
			this.nvc.Add("decimal_valid_integer", "65");
			this.nvc.Add("decimal_invalid_string", "hest");
			this.nvc.Add("decimal_invalid_numberandstring", "45.3m");
			this.nvc.Add("decimal_invalid_spaceseparator", "6 5");
			
			this.nvc.Add("checkbox_empty", "");
			this.nvc.Add("checkbox_one_value", "val1");
			this.nvc.Add("checkbox_five_values", "val1,val2,val3,val4,val5");
			
			this.nvc.Add("checkboxint_empty", "");
			this.nvc.Add("checkboxint_one_value", "1");
			this.nvc.Add("checkboxint_five_values", "1,2,3,4,5");
			this.nvc.Add("checkboxint_one_invalid_value", "1,2,three,4,5");
			
			this.nvc.Add("stringarray_valid", "should not be seen");
			this.nvc.Add("stringarray_valid[0]", "zero");
			this.nvc.Add("stringarray_valid[1]", "one");
			this.nvc.Add("stringarray_valid[2]", "two");
			this.nvc.Add("stringarray_valid[3]", String.Empty);
		}
		
		[TearDown]
		public void TearDown()
		{
			this.nvc.Clear();
			this.nvc = null;
		}
		#endregion SetUp / TearDown
		
		#region GetInt
		/// <summary>
		/// If given a key for a valid integer value, the method should return that value
		/// </summary>
		[Test]
		public void GetInt_Valid()
		{
			Assert.AreEqual(4, this.nvc.GetInt("int_valid", 0), "GetInt should not return the default value");
		}
		
		/// <summary>
		/// If given a key to an invalid integer (a string for instance), the method should return
		/// the given default value
		/// </summary>
		[Test]
		public void GetInt_Invalid()
		{
			Assert.AreEqual(0, this.nvc.GetInt("int_invalid", 0), "GetInt should return the default value");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, the given default value should be returned
		/// </summary>
		[Test]
		public void GetInt_NotExisting()
		{
			Assert.AreEqual(0, this.nvc.GetInt("int_doesnotexist", 0), "GetInt should return the default value");
		}
		#endregion GetInt
		
		#region GetLong
		/// <summary>
		/// If given a key for a valid long value, the method should return that value
		/// </summary>
		[Test]
		public void GetLong_Valid()
		{
			Assert.AreEqual(4, this.nvc.GetLong("long_valid", 0), "GetLong should not return the default value");
		}
		
		/// <summary>
		/// If given a key to an invalid long (a string for instance), the method should return
		/// the given default value
		/// </summary>
		[Test]
		public void GetLong_Invalid()
		{
			Assert.AreEqual(0, this.nvc.GetLong("long_invalid", 0), "GetLong should return the default value");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, the given default value should be returned
		/// </summary>
		[Test]
		public void GetLong_NotExisting()
		{
			Assert.AreEqual(0, this.nvc.GetLong("long_doesnotexist", 0), "GetLong should return the default value");
		}
		#endregion GetLong
		
		#region GetString
		/// <summary>
		/// If given an existing key the method should return that value
		/// </summary>
		[Test]
		public void GetString_Valid()
		{
			Assert.AreEqual("valid", this.nvc.GetString("string_valid", "default"), "GetString should not return the default value");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, the given default value should be returned
		/// </summary>
		[Test]
		public void GetString_NotExisting()
		{
			Assert.AreEqual("default", this.nvc.GetString("string_doesnotexist", "default"), "GetString should return the default value");
		}
		#endregion GetString
		
		#region GetStringArray
		/// <summary>
		/// If given a key that is the prefix part of a "key[n]" key, the method
		/// should return a string array with all n values
		/// </summary>
		[Test]
		public void GetStringArray_Valid()
		{
			string[] result = this.nvc.GetStringArray("stringarray_valid");
			
			Assert.AreEqual(4, result.Length, "There should be 4 results");
			
			Assert.AreEqual("zero", result[0]);
			Assert.AreEqual("one", result[1]);
			Assert.AreEqual("two", result[2]);
			Assert.AreEqual(String.Empty, result[3]);
		}
		
		/// <summary>
		/// If given a key that does not exist in the "key[n]" form, the method
		/// should return an empty string array
		/// </summary>
		[Test]
		public void GetStringArray_NotExisting()
		{
			Assert.AreEqual(new string[0], this.nvc.GetStringArray("stringarray_nonexistant"));
		}
		#endregion GetStringArray
		
		#region GetStringAsLines
		/// <summary>
		/// If given an existing key, an array should be returned
		/// </summary>
		[Test]
		public void GetStringAsLines_Valid()
		{
			// check unix line endings
			Assert.AreEqual(new string[] { "line 1", "line 2" }, this.nvc.GetStringAsLines("stringaslines_valid_unix"), "GetStringAsLines should not return an empty array for unix");
			
			// check mac line endings
			Assert.AreEqual(new string[] { "line 1", "line 2" }, this.nvc.GetStringAsLines("stringaslines_valid_mac"), "GetStringAsLines should not return an empty array for mac");
			
			// check windows line endings
			Assert.AreEqual(new string[] { "line 1", "line 2" }, this.nvc.GetStringAsLines("stringaslines_valid_win"), "GetStringAsLines should not return an empty array for win");
			
			// check mixed line endings
			Assert.AreEqual(new string[] { "line 1", "line 2", "line 3", "line 4" }, this.nvc.GetStringAsLines("stringaslines_valid_mixed"), "GetStringAsLines should not return an empty array for win");
			
			// only 1 line
			Assert.AreEqual(new string[] { "line 1" }, this.nvc.GetStringAsLines("stringaslines_valid_singleline"), "GetStringAsLines should return a single entry array");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, an empty array should be returned
		/// </summary>
		[Test]
		public void GetStringAsLines_NotExisting()
		{
			Assert.AreEqual(new string[0], this.nvc.GetStringAsLines("stringaslines_doesnotexist"), "GetStringAsLines should return an empty array");
		}
		#endregion GetStringAsLines
		
		#region GetDateTime
		/// <summary>
		/// If given a key for a valid DateTime value, the method should return that value
		/// </summary>
		[Test]
		public void GetDateTime_Valid()
		{
			Assert.AreEqual(new DateTime(2011, 1, 12, 14, 17, 0), this.nvc.GetDateTime("datetime_valid", DateTime.MinValue), "GetDateTime should not return the default value");
		}
		
		/// <summary>
		/// If given a key to an invalid DateTime (a double for instance), the method should return
		/// the given default value
		/// </summary>
		[Test]
		public void GetDateTime_Invalid()
		{
			Assert.AreEqual(DateTime.MinValue, this.nvc.GetDateTime("datetime_invalid", DateTime.MinValue), "GetDateTime should return the default value");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, the given default value should be returned
		/// </summary>
		[Test]
		public void GetDateTime_NotExisting()
		{
			Assert.AreEqual(DateTime.MinValue, this.nvc.GetDateTime("datetime_doesnotexist", DateTime.MinValue), "GetDateTime should return the default value");
		}
		#endregion GetDateTime
		
		#region GetBool
		/// <summary>
		/// If given a key that points to a valid boolean, the method should return a matching boolean
		/// </summary>
		[Test]
		public void GetBool_Valid()
		{
			// parse numbers
			Assert.AreEqual(true, this.nvc.GetBool("bool_valid_1", false), "GetBool should return true for the value '1'");
			Assert.AreEqual(false, this.nvc.GetBool("bool_valid_0", true), "GetBool should return false for the value '0'");
			
			// parse text
			Assert.AreEqual(true, this.nvc.GetBool("bool_valid_true", false), "GetBool should return true for the value 'true'");
			Assert.AreEqual(false, this.nvc.GetBool("bool_valid_false", true), "GetBool should return false for the value 'false'");
			Assert.AreEqual(true, this.nvc.GetBool("bool_valid_true_uc", false), "GetBool should return true for the value 'TRUE'");
			Assert.AreEqual(false, this.nvc.GetBool("bool_valid_false_uc", true), "GetBool should return false for the value 'FALSE'");
		}
		
		/// <summary>
		/// If given a key that points to something that cannot be converted to a boolean, the method should return the default value
		/// </summary>
		[Test]
		public void GetBool_Invalid()
		{
			Assert.AreEqual(false, this.nvc.GetBool("bool_invalid", false), "GetBool should return the default value for invalid data");
		}
		
		/// <summary>
		/// If given a key that does not exist, the method should return the default value
		/// </summary>
		[Test]
		public void GetBool_NotExisting()
		{
			Assert.AreEqual(false, this.nvc.GetBool("bool_doesnotexist", false), "GetBool should return the default value for non-existant keys");
		}
		#endregion GetBool
		
		#region GetDecimal
		/// <summary>
		/// If given a key for a valid long value, the method should return that value
		/// </summary>
		[Test]
		public void GetDecimal_Valid()
		{
			Assert.AreEqual(2.0m, this.nvc.GetDecimal("decimal_valid_simple", 0), "Simple valid input.. should not return default");
			Assert.AreEqual(65.0m, this.nvc.GetDecimal("decimal_valid_integer", 0), "Simple integer input.. should not return default");
			Assert.AreEqual(-4.65m, this.nvc.GetDecimal("decimal_valid_negative", 0), "Simple negative input.. should not return default");
			Assert.AreEqual(-4.65m, this.nvc.GetDecimal("decimal_valid_negative_spaceaftersign", 0), "Negative number with space after sign... should not return default");
			Assert.AreEqual(0.0m, this.nvc.GetDecimal("decimal_valid_negative_zero", 44), "0.0 with negative sign should return unsigned 0.0");
			Assert.AreEqual(4.29m, this.nvc.GetDecimal("decimal_valid_positive_sign", 0), "Positive input with sign.. should not return default");
			Assert.AreEqual(4.29m, this.nvc.GetDecimal("decimal_valid_positive_spaceaftersign", 0), "Positive value with space after sign... should not return default");
		}
		
		/// <summary>
		/// If given a key to an invalid long (a string for instance), the method should return
		/// the given default value
		/// </summary>
		[Test]
		public void GetDecimal_Invalid()
		{
			Assert.AreEqual(0.0m, this.nvc.GetDecimal("decimal_invalid_string", 0.0m), "Input is a string... return default");
			Assert.AreEqual(0.0m, this.nvc.GetDecimal("decimal_invalid_numberandstring", 0.0m), "Number + text is invalid.. return default");
			Assert.AreEqual(0.0m, this.nvc.GetDecimal("decimal_invalid_spaceseparator", 0.0m), "Space is not a valid separator.. return default");
		}
		
		/// <summary>
		/// If given a key that does not exist in the collection, the given default value should be returned
		/// </summary>
		[Test]
		public void GetDecimal_NotExisting()
		{
			Assert.AreEqual(12.1m, this.nvc.GetDecimal("decimal_doesnotexist", 12.1m), "GetDecimal should return the default value");
		}
		#endregion GetDecimal
		
		#region GetCheckboxArray
		/// <summary>
		/// If the key exists but is empty, an empty array should be returned
		/// </summary>
		[Test]
		public void GetCheckboxArray_Empty()
		{
			Assert.AreEqual(new string[0], this.nvc.GetCheckboxArray("checkbox_empty"), "Array should be empty");
		}
		
		/// <summary>
		/// If the key points to a single string value (no commas), an array with length 1 should be
		/// returned (and the value from the key as that value of course)
		/// </summary>
		[Test]
		public void GetCheckboxArray_OneValue()
		{
			Assert.AreEqual(new string[]{"val1"}, this.nvc.GetCheckboxArray("checkbox_one_value"), "Array should have 1 value");
		}
		
		/// <summary>
		/// If the key points to a multi-string value (with commas), an array with that length should be returned
		/// (with the values of course)
		/// </summary>
		[Test]
		public void GetCheckboxArray_MultipleValues()
		{
			Assert.AreEqual(new string[]{"val1","val2","val3","val4","val5"}, this.nvc.GetCheckboxArray("checkbox_five_values"), "Array should have 5 values");
		}
		
		/// <summary>
		/// If the key does not exist, an empty array should be returned
		/// </summary>
		[Test]
		public void GetCheckboxArray_DoesNotExist()
		{
			Assert.AreEqual(new string[0], this.nvc.GetCheckboxArray("checkbox_doesnotexist"), "Array should be empty");
		}
		#endregion GetCheckboxArray
		
		#region GetCheckboxArrayInt
		/// <summary>
		/// If the key exists but is empty, an empty array should be returned
		/// </summary>
		[Test]
		public void GetCheckboxArrayInt_Empty()
		{
			Assert.AreEqual(new int[0], this.nvc.GetCheckboxArrayInt("checkboxint_empty"), "Array should be empty");
		}
		
		/// <summary>
		/// If the key points to a single string value (no commas), an array with length 1 should be
		/// returned (and the value from the key as that value of course)
		/// </summary>
		[Test]
		public void GetCheckboxArrayInt_OneValue()
		{
			Assert.AreEqual(new int[]{1}, this.nvc.GetCheckboxArrayInt("checkboxint_one_value"), "Array should have 1 value");
		}
		
		/// <summary>
		/// If the key points to a multi-string value (with commas), an array with that length should be returned
		/// (with the values of course)
		/// </summary>
		[Test]
		public void GetCheckboxArrayInt_MultipleValues()
		{
			Assert.AreEqual(new int[]{1,2,3,4,5}, this.nvc.GetCheckboxArrayInt("checkboxint_five_values"), "Array should have 5 values");
		}
		
		/// <summary>
		/// If the key does not exist, an empty array should be returned
		/// </summary>
		[Test]
		public void GetCheckboxArrayInt_DoesNotExist()
		{
			Assert.AreEqual(new int[0], this.nvc.GetCheckboxArrayInt("checkboxint_doesnotexist"), "Array should be empty");
		}
		
		/// <summary>
		/// If the key points to a multi-string value (with commas), but 1 of those values cannot be converted to an
		/// integer, an empty array should be returned
		/// </summary>
		[Test]
		public void GetCheckboxArrayInt_OneInvalidValue()
		{
			Assert.AreEqual(new int[0], this.nvc.GetCheckboxArrayInt("checkboxint_one_invalid_value"), "Array should be empty");
		}
		#endregion GetCheckboxArrayInt
	}
}