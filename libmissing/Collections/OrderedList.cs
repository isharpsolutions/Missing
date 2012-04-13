using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Missing.Validation;

namespace Missing.Collections
{
	public class OrderedList<T> : IList<T> where T : class
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.OrderedList`1"/> class.
		/// </summary>
		public OrderedList() : base()
		{
		}
		
		public OrderedList(IComparer<T> comparer) : base()
		{
			this.Comparer = comparer;
		}
		
		public OrderedList(Expression<Func<T, object>> memberExpression) : base()
		{
			this.OrderingKey = PropertyPath.From<T>(memberExpression);
		}
		#endregion Constructors
		
		#region Sorting
		public IComparer<T> Comparer { get; set; }
		public PropertyPath OrderingKey { get; set; }
		
		protected List<T> items = new List<T>();
		protected bool isSorted = true;
		
		protected void Sort()
		{
			if (this.Comparer != null)
			{
				this.items.Sort(this.Comparer);
			}
			
			else if (this.OrderingKey != default(PropertyPath))
			{
				throw new NotImplementedException();
			}
			
			else
			{
				this.items.Sort();
			}
			
			this.isSorted = true;
		}
		#endregion Sorting

		#region IList[T] implementation
		public int IndexOf(T item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

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
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region ICollection[T] implementation
		public void Add(T item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(T item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(T item)
		{
			throw new NotImplementedException();
		}

		public int Count {
			get {
				throw new NotImplementedException();
			}
		}

		public bool IsReadOnly {
			get {
				throw new NotImplementedException();
			}
		}
		#endregion
	}
}