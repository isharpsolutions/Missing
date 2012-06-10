using System;
using System.Collections.Generic;
using System.Collections;

namespace Missing.Collections
{
	public class CircularList<T> : IList<T>
	{
		public CircularList() : this(10)
		{
		}
		
		public CircularList(int capacity)
		{
			this.items = new List<T>(capacity);
			this.CurrentIndex = -1;
		}
		
		private List<T> items;
		
		public int Capacity
		{
			get { return this.items.Capacity; }
			set { this.items.Capacity = value; }
		}
		
		public int CurrentIndex
		{
			get;
			private set;
		}

		#region IList[T] implementation
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public T this[int index]
		{
			get { return this.items[index]; }
			set { this.items[index] = value; }
		}
		#endregion

		#region IEnumerable[T] implementation
		public IEnumerator<T> GetEnumerator()
		{
			return new CircularListEnumerator<T>(this);
		}
		#endregion

		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		#endregion

		#region ICollection[T] implementation
		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			this.items.Clear();
			this.CurrentIndex = -1;
		}

		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public int Count
		{
			get { return this.items.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}
		#endregion
	}
}