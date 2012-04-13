using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Missing.Validation;
using System.Linq;

namespace Missing.Collections
{
	/// <summary>
	/// A list in which the elements are always ordered when you ask for them. The sorting
	/// is done using a user provided <see cref="IComparer`1"/> or a
	/// <see cref="PropertyPath"/>
	/// </summary>
	/// <remarks>
	/// Sorting is not done until you request an item.
	/// 
	/// It has not been tested with large datasets, so it might not
	/// scale very well.
	/// </remarks>
	public class OrderedList<T> : IList<T> where T : class
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.OrderedList`1"/> class.
		/// </summary>
		public OrderedList() : base()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.OrderedList`1"/> class.
		/// </summary>
		/// <param name="comparer">
		/// The comparer to use
		/// </param>
		public OrderedList(IComparer<T> comparer) : base()
		{
			this.Comparer = comparer;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.OrderedList`1"/> class.
		/// </summary>
		/// <param name="orderingKeyExpression">
		/// An expression that selects the property to sort by
		/// </param>
		public OrderedList(Expression<Func<T, object>> orderingKeyExpression) : base()
		{
			this.OrderingKey = PropertyPath.From<T>(orderingKeyExpression);
		}
		#endregion Constructors
		
		#region Sorting
		/// <summary>
		/// The comparer.
		/// </summary>
		private IComparer<T> comparer = null;
		
		/// <summary>
		/// Gets or sets the comparer.
		/// </summary>
		public IComparer<T> Comparer
		{
			get { return this.comparer; }
			set
			{
				this.comparer = value;
				this.isSorted = false;
			}
		}
		
		/// <summary>
		/// The ordering key.
		/// </summary>
		private PropertyPath orderingKey = default(PropertyPath);
		
		/// <summary>
		/// Gets or sets the ordering key.
		/// </summary>
		public PropertyPath OrderingKey
		{
			get { return this.orderingKey; }
			set
			{
				this.comparer = new GenericComparer<T>() {
					SortExpression = value
				};
				this.orderingKey = value;
				this.isSorted = false;
			}
		}

		/// <summary>
		/// The items.
		/// </summary>
		protected List<T> items = new List<T>();
		
		/// <summary>
		/// Whether the list is currently sorted
		/// </summary>
		protected bool isSorted = true;
		
		/// <summary>
		/// Sort this instance.
		/// </summary>
		protected void Sort()
		{
			if (this.Comparer != null)
			{
				this.items.Sort(this.Comparer);
			}

			else
			{
				this.items.Sort();
			}
			
			this.isSorted = true;
		}
		#endregion Sorting

		#region IList[T] implementation
		/// <summary>
		/// Indexs the of.
		/// </summary>
		/// <returns>
		/// The of.
		/// </returns>
		/// <param name='item'>
		/// Item.
		/// </param>
		public int IndexOf(T item)
		{
			if (!this.isSorted)
			{
				this.Sort();
			}
			
			return this.items.IndexOf(item);
		}
		
		/// <summary>
		/// Insert the specified index and item.
		/// </summary>
		/// <param name='index'>
		/// Index.
		/// </param>
		/// <param name='item'>
		/// Item.
		/// </param>
		public void Insert(int index, T item)
		{
			this.items.Insert(index, item);
			this.isSorted = true;
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name='index'>
		/// Index.
		/// </param>
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
			
			// removing an item does not
			// change the order of elements
		}
		
		/// <summary>
		/// Gets or sets the <see cref="Missing.Collections.OrderedList`1"/> at the specified index.
		/// </summary>
		/// <param name='index'>
		/// Index.
		/// </param>
		public T this[int index]
		{
			get
			{
				if (!this.isSorted)
				{
					this.Sort();
				}
				return this.items[index];
			}
			
			set
			{
				this.items[index] = value;
				this.isSorted = false;
			}
		}
		#endregion

		#region IEnumerable[T] implementation
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>
		/// The enumerator.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			if (!this.isSorted)
			{
				this.Sort();
			}
			return this.items.GetEnumerator();
		}
		#endregion

		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>
		/// The enumerator.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			if (!this.isSorted)
			{
				this.Sort();
			}
			return this.items.GetEnumerator();
		}
		#endregion

		#region ICollection[T] implementation
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name='item'>
		/// Item.
		/// </param>
		public void Add(T item)
		{
			this.items.Add(item);
			this.isSorted = false;
		}
		
		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			this.items.Clear();
			this.isSorted = true;
		}

		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name='item'>
		/// If set to <c>true</c> item.
		/// </param>
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}
		
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name='array'>
		/// Array.
		/// </param>
		/// <param name='arrayIndex'>
		/// Array index.
		/// </param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (!this.isSorted)
			{
				this.Sort();
			}
			this.items.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name='item'>
		/// If set to <c>true</c> item.
		/// </param>
		public bool Remove(T item)
		{
			return this.items.Remove(item);
			
			// removing an item does not
			// change the order of elements
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public int Count
		{
			get { return this.items.Count; }
		}
		
		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
		/// </value>
		public bool IsReadOnly
		{
			get { return false; }
		}
		#endregion
	}
}