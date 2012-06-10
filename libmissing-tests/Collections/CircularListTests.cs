using System;
using NUnit.Framework;
using Missing.Collections;
using Missing.CircularListStuff;

namespace Missing
{
	[TestFixture]
	public class CircularListTests
	{
		#region Constructors
		[Test]
		public void Constructor_Default()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>();
			
			Assert.AreEqual(10, list.Capacity, "Default capacity should be 10");
			Assert.IsEmpty(list, "List should be empty by default");
			Assert.AreEqual(-1, list.CurrentIndex, "Current index should be -1");
		}
		
		[Test]
		public void Constructor_CustomCapacity()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(55);
			
			Assert.AreEqual(55, list.Capacity, "Capacity should be 55");
			Assert.IsEmpty(list, "List should be empty");
			Assert.AreEqual(-1, list.CurrentIndex, "Current index should be -1");
		}
		#endregion Constructors
		
		#region IndexOf
		[Test]
		public void IndexOf_BeforeLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.AreEqual(1, list.IndexOf(two), "Index should be 1 as this is the second element");
			Assert.AreEqual(0, list.IndexOf(one), "Index should be 0 as this is the first element");
			Assert.AreEqual(-1, list.IndexOf(three), "Index should be -1 as the item is not in the list");
		}
		
		[Test]
		public void IndexOf_AboutToLoop()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.AreEqual(1, list.IndexOf(two), "Index should be 1 as this is the second element");
			Assert.AreEqual(0, list.IndexOf(one), "Index should be 0 as this is the first element");
			Assert.AreEqual(-1, list.IndexOf(three), "Index should be -1 as the item is not in the list");
		}
		
		[Test]
		public void IndexOf_AfterLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			list.Add(three);
			
			Assert.AreEqual(1, list.IndexOf(two), "Index should be 1 as this is the second element");
			Assert.AreEqual(-1, list.IndexOf(one), "Index should be -1 as this item has been replaced by new content");
			Assert.AreEqual(0, list.IndexOf(three), "Index should be 0 as the item is the first to be added after looping");
		}
		#endregion IndexOf
		
		#region Clear
		[Test]
		public void Clear_BeforeLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			list.Clear();
			
			Assert.IsEmpty(list, "List should be empty as it has just been cleared");
			Assert.AreEqual(-1, list.CurrentIndex, "The index should have been reset by the clear");
		}
		
		[Test]
		public void Clear_AboutToLoop()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			list.Clear();
			
			Assert.IsEmpty(list, "List should be empty as it has just been cleared");
			Assert.AreEqual(-1, list.CurrentIndex, "The index should have been reset by the clear");
		}
		
		[Test]
		public void Clear_AfterLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			list.Add(three);
			
			list.Clear();
			
			Assert.IsEmpty(list, "List should be empty as it has just been cleared");
			Assert.AreEqual(-1, list.CurrentIndex, "The index should have been reset by the clear");
		}
		#endregion Clear
		
		#region Contains
		[Test]
		public void Contains_BeforeLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.IsTrue(list.Contains(two), "The element should be in the list");
			Assert.IsTrue(list.Contains(one), "The element should be in the list");
			Assert.IsFalse(list.Contains(three), "The element should not be in the list");
		}
		
		[Test]
		public void Contains_AboutToLoop()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.IsTrue(list.Contains(two), "The element should be in the list");
			Assert.IsTrue(list.Contains(one), "The element should be in the list");
			Assert.IsFalse(list.Contains(three), "The element should not be in the list");
		}
		
		[Test]
		public void Contains_AfterLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			list.Add(three);
			
			Assert.IsTrue(list.Contains(two), "The element should be in the list");
			Assert.IsFalse(list.Contains(one), "The element should not be in the list");
			Assert.IsTrue(list.Contains(three), "The element should be in the list");
		}
		#endregion Contains
		
		#region Count
		[Test]
		public void Count_BeforeLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.AreEqual(2, list.Count);
		}
		
		[Test]
		public void Count_AboutToLoop()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			
			Assert.AreEqual(2, list.Count);
		}
		
		[Test]
		public void Count_AfterLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			list.Add(three);
			
			Assert.AreEqual(2, list.Count);
		}
		#endregion Count
	}
}


#region Stuff
namespace Missing.CircularListStuff
{
	public class SimpleElement
	{
		public SimpleElement()
		{
			this.String = String.Empty;
		}
		
		public string String { get; set; }
	}
}
#endregion Stuff