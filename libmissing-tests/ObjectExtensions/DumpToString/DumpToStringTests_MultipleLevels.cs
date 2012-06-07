using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.MultipleLevels;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void MultipleLevels()
		{
			One obj = new One() {
				Two = new Two() {
					Three = new Three() {
						BarneyIs = "Awe-and-then-some!"
					}
				}
			};
			
			string expected = @"{
	Two = {
		Three = {
			BarneyIs = 'Awe-and-then-some!'
		}
	}
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.MultipleLevels
{
	public class One
	{
		public Two Two { get; set; }
	}
	
	public class Two
	{
		public Three Three { get; set; }
	}
	
	public class Three
	{
		public string BarneyIs { get; set; }
	}
}