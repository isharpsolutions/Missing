using System;
using NUnit.Framework;
using Missing.ObjectExtensions;
using Missing.DumpToStringHelpers.PrivateProperty;

namespace Missing
{
	[TestFixture]
	public partial class DumpToStringTests
	{
		[Test]
		public void ClassWithPrivateProperty()
		{
			ClassWithPrivateProperty obj = new ClassWithPrivateProperty() {
				Public = "Public value"
			};
			
			string expected = @"{
	Public = 'Public value'
	Private = 'Private value'
}";
			
			Assert.AreEqual(expected, obj.DumpToString());
		}
	}
}


namespace Missing.DumpToStringHelpers.PrivateProperty
{
	public class ClassWithPrivateProperty
	{
		public ClassWithPrivateProperty()
		{
			this.Private = "Private value";
		}
		
		public string Public { get; set; }
		private string Private { get; set; }
	}
}