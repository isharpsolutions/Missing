using System;
using System.Collections.Generic;
using System.Collections;

namespace Missing.Collections
{
	/// <summary>
	/// A circular list, is a list with a maximum size, where the
	/// oldest elements are overwritten when new elements come in
	/// after reaching max length.
	/// 
	/// Enumeration outputs the elements in the order they came in
	/// i.e. oldest elements first
	/// </summary>
	public class CircularList<T> : IList<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.CircularList`1"/> class
		/// with a max size of 10
		/// </summary>
		public CircularList() : this(10)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.CircularList`1"/> class.
		/// </summary>
		/// <param name="maxLength">
		/// The maximum number of elements allowed
		/// </param>
		public CircularList(int maxLength)
		{
			this.items = new List<T>(maxLength);
			this.CurrentIndex = -1;
		}
		
		/// <summary>
		/// The underlying list
		/// </summary>
		private List<T> items;
		
		/// <summary>
		/// Gets or sets the maximum number of elements allowed
		/// </summary>
		public int MaxLength
		{
			get { return this.items.Capacity; }
			set { this.items.Capacity = value; }
		}
		
		/// <summary>
		/// Gets the index of the most recently
		/// added item
		/// </summary>
		public int CurrentIndex
		{
			get;
			private set;
		}
		
		/// <summary>
		/// Calculate the next index
		/// </summary>
		/// <returns>
		/// The next index
		/// </returns>
		private int GetNextIndex()
		{
			return (this.CurrentIndex + 1) % this.items.Capacity;
		}
		
		/// <summary>
		/// Increments the index
		/// </summary>
		private void IncrementIndex()
		{
			this.CurrentIndex = this.GetNextIndex();
		}

		#region IList[T] implementation
		/// <summary>
		/// Determines the index of a specific item
		/// </summary>
		/// <returns>
		/// The index of item if found in the list; otherwise, -1.
		/// </returns>
		/// <param name="item">
		/// The object to locate
		/// </param>
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}
		
		/// <summary>
		/// Inserts an item at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index at which item should be inserted.
		/// </param>
		/// <param name="item">
		/// The object to insert
		/// </param>
		/// <exception cref="NotSupportedException">
		/// Always thrown, as this operation is not allowed
		/// </exception>
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}
		
		/// <summary>
		/// Removes the item at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the item to remove.
		/// </param>
		/// <exception cref="NotSupportedException">
		/// Always thrown, as this operation is not allowed
		/// </exception>
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}
		
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">
		/// The zero-based index of the element to get or set.
		/// </param>
		/// <exception cref="NotSupportedException">
		/// SET: Always thrown, as this operation is not allowed
		/// </exception>
		public T this[int index]
		{
			get { return this.items[index]; }
			set { throw new NotSupportedException(); }
		}
		#endregion

		#region IEnumerable[T] implementation
		/// <summary>
		/// Returns an enumerator that iterates through the List
		/// </summary>
		/// <returns>
		/// The enumerator.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return new CircularListEnumerator<T>(this);
		}
		#endregion

		#region IEnumerable implementation
		/// <summary>
		/// Returns an enumerator that iterates through the List
		/// </summary>
		/// <returns>
		/// The enumerator.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		#endregion

		#region ICollection[T] implementation
		/// <summary>
		/// Adds an object to the list
		/// </summary>
		/// <param name="item">
		/// The object to be added. The value can be null for reference types.
		/// </param>
		public void Add(T item)
		{
			this.IncrementIndex();
			
			if (this.CurrentIndex >= this.items.Count)
			{
				this.items.Add(item);
			}
			
			else
			{
				this.items[this.CurrentIndex] = item;
			}
		}
		
		/// <summary>
		/// Removes all elements
		/// </summary>
		public void Clear()
		{
			this.items.Clear();
			this.CurrentIndex = -1;
		}
		
		/// <summary>
		/// Determines whether an element is in the List
		/// </summary>
		/// <param name="item">
		/// The object to locate. The value can be null for reference types.
		/// </param>
		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}
		
		/// <summary>
		/// Copies the entire List to a compatible one-dimensional array, 
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">
		/// The one-dimensional Array that is the destination of the elements copied from List. The Array must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">
		/// The zero-based index in array at which copying begins.
		/// </param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			foreach (T cur in this)
			{
				if (arrayIndex == array.Length)
				{
					return;
				}
				
				array[arrayIndex] = cur;
				arrayIndex++;
			}
		}
		
		/// <summary>
		/// Removes the first occurrence of a specific object from the List
		/// </summary>
		/// <returns>
		/// <c>true</c> if item is successfully removed; otherwise, <c>false</c>. This method also returns false if item was not found in the List.
		/// </returns>
		/// <param name="item">
		/// The object to remove from the List. The value can be null for reference types.
		/// </param>
		/// <exception cref="NotSupportedException">
		/// Always thrown, as this operation is not allowed
		/// </exception>
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}
		
		/// <summary>
		/// Gets the number of elements actually contained in the List
		/// </summary>
		public int Count
		{
			get { return this.items.Count; }
		}
		
		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <remarks>
		/// Currently it always returns <c>false</c>
		/// </remarks>
		public bool IsReadOnly
		{
			get { return false; }
		}
		#endregion
	}
}