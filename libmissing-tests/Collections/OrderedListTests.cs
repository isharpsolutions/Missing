using System;
using NUnit.Framework;
using Missing.Collections;
using System.Collections.Generic;
using Missing.Validation;

namespace Missing
{
	[TestFixture]
	public class OrderedListTests
	{
		[Test]
		public void EmptyByDefault()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			Assert.IsEmpty(list, "The list should be empty by default");
		}
		
		[Test]
		public void Count()
		{
			OrderedList<Score> list = new OrderedList<Score>(new ScoreComparer());
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual(3, list.Count, "Count is wrong");
		}
		
		#region Default comparer
		[Ignore("I am not sure at this point, how the default sort would sort the items")]
		[Test]
		public void DefaultComparer()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		#endregion
		
		#region Custom comparer
		[Test]
		public void CustomComparer_InConstructor()
		{
			OrderedList<Score> list = new OrderedList<Score>(new ScoreComparer());
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void CustomComparer_InProperty_BeforeAdd()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			list.Comparer = new ScoreComparer();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void CustomComparer_InProperty_WhileAdding()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Comparer = new ScoreComparer();
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void CustomComparer_InProperty_AfterAdding()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			list.Comparer = new ScoreComparer();
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		#endregion
		
		#region Property based sort
		[Test]
		public void PropertyBased_InConstructor()
		{
			OrderedList<Score> list = new OrderedList<Score>(y => y.Match);
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void PropertyBased_InProperty_BeforeAdd()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			list.OrderingKey = PropertyPath.From<Score>(y => y.Match);
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void PropertyBased_InProperty_WhileAdding()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.OrderingKey = PropertyPath.From<Score>(y => y.Match);
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		
		[Test]
		public void PropertyBased_InProperty_AfterAdding()
		{
			OrderedList<Score> list = new OrderedList<Score>();
			
			list.Add(new Score() {
				Match = 2,
				Name = "Two"
			});
			
			list.Add(new Score() {
				Match = 1,
				Name = "One"
			});
			
			list.Add(new Score() {
				Match = 3,
				Name = "Three"
			});
			
			list.OrderingKey = PropertyPath.From<Score>(y => y.Match);
			
			Assert.AreEqual("One", list[0], "First element is wrong");
			Assert.AreEqual("Two", list[1], "Second element is wrong");
			Assert.AreEqual("Three", list[2], "Third element is wrong");
		}
		#endregion
		
		#region Test class
		private class Score
		{
			public int Match { get; set; }
			public string Name { get; set; }
		}
		#endregion
		
		#region Comparer
		private class ScoreComparer : IComparer<Score>
		{
			public int Compare(Score x, Score y)
			{
				return x.Match.CompareTo(y.Match);
			}
		}
		#endregion
	}
}