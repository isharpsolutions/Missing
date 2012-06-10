using System;
using NUnit.Framework;
using Missing.Collections;
using Missing.CircularListStuff;
using System.Collections.Generic;

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
		
		#region Enumerator
		[Test]
		public void Enumerator_BeforeLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			
			list.Add(one);
			list.Add(two);
			
			List<SimpleElement> actual = new List<SimpleElement>();
			foreach (SimpleElement cur in list)
			{
				actual.Add(cur);
			}
			
			Assert.AreEqual(2, actual.Count, "The foreach should have yielded 2 items");
			Assert.AreEqual(one, actual[0], "First yielded item is wrong");
			Assert.AreEqual(two, actual[1], "Second yielded item is wrong");
		}
		
		[Test]
		public void Enumerator_AboutToLoop()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			
			list.Add(one);
			list.Add(two);
			
			List<SimpleElement> actual = new List<SimpleElement>();
			foreach (SimpleElement cur in list)
			{
				actual.Add(cur);
			}
			
			Assert.AreEqual(2, actual.Count, "The foreach should have yielded 2 items");
			Assert.AreEqual(one, actual[0], "First yielded item is wrong");
			Assert.AreEqual(two, actual[1], "Second yielded item is wrong");
		}
		
		[Test]
		public void Enumerator_AfterLooping()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(2);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			
			list.Add(one);
			list.Add(two);
			list.Add(three);
			
			List<SimpleElement> actual = new List<SimpleElement>();
			foreach (SimpleElement cur in list)
			{
				actual.Add(cur);
			}
			
			Assert.AreEqual(2, actual.Count, "The foreach should have yielded 2 items");
			Assert.AreEqual(two, actual[0], "First yielded item is wrong");
			Assert.AreEqual(three, actual[1], "Second yielded item is wrong");
		}
		
		[Test]
		public void Enumerator_AfterLooping_LongerList()
		{
			CircularList<SimpleElement> list = new CircularList<SimpleElement>(3);
			
			SimpleElement one = new SimpleElement() { String = "One" };
			SimpleElement two = new SimpleElement() { String = "Two" };
			SimpleElement three = new SimpleElement() { String = "Three" };
			SimpleElement four = new SimpleElement() { String = "Four" };
			SimpleElement five = new SimpleElement() { String = "FIve" };
			
			list.Add(one); // ==> 0
			list.Add(two); // ==> 1
			list.Add(three); // ==> 2
			list.Add(four); // ==> 0
			list.Add(five); // ==> 1
			
			List<SimpleElement> actual = new List<SimpleElement>();
			foreach (SimpleElement cur in list)
			{
				actual.Add(cur);
			}
			
			Assert.AreEqual(3, actual.Count, "The foreach should have yielded 3 items");
			Assert.AreEqual(three, actual[0], "First yielded item is wrong");
			Assert.AreEqual(four, actual[1], "Second yielded item is wrong");
			Assert.AreEqual(five, actual[2], "Third yielded item is wrong");
		}
		#endregion Enumerator
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
		
		public override string ToString()
		{
			return string.Format("[SimpleElement: String={0}]", String);
		}
	}
}
#endregion Stuff