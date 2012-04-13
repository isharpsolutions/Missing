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
			if (!this.isSorted)
			{
				this.Sort();
			}
			
			return this.items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			this.items.Insert(index, item);
			this.isSorted = true;
		}

		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
			
			// removing an item does not
			// change the order of elements
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
			return this.items.GetEnumerator();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}
		#endregion

		#region ICollection[T] implementation
		public void Add(T item)
		{
			this.items.Add(item);
			this.isSorted = false;
		}

		public void Clear()
		{
			this.items.Clear();
			this.isSorted = true;
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
			return this.items.Remove(item);
			
			// removing an item does not
			// change the order of elements
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