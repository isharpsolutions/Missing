using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.SpecialCaseStuff;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void SpecialCase()
		{
			Missing.Diagnostics.ObjectDumper.AddSpecialCase(typeof(SpecialClass), y => {
				return System.String.Format("'{0}'", ((SpecialClass)y).InterestingProp);
			});
			
			SpecialClass obj = new SpecialClass() {
				BoringProp = "Not wanted",
				OtherBoringProp = "Not wanted either",
				InterestingProp = "Thats the stuff"
			};
			
			string expected = @"'Thats the stuff'";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.SpecialCaseStuff
{
	public class SpecialClass
	{
		public string BoringProp { get; set; }
		public string OtherBoringProp { get; set; }
		public string InterestingProp { get; set; }
	}
}