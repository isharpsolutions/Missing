using System;
using System.Collections.Generic;

namespace Missing.Collections
{
	/// <summary>
	/// Enumerator for <see cref="CircularList"/>
	/// </summary>
	public class CircularListEnumerator<T> : IEnumerator<T>
	{
		/// <summary>
		/// The collection we are enumerating
		/// </summary>
		private CircularList<T> collection;
		
		/// <summary>
		/// The current index
		/// </summary>
		private int curIndex;
		
		/// <summary>
		/// The current element
		/// </summary>
		private T curElement;

		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.CircularListEnumerator`1"/> class.
		/// </summary>
		/// <param name="collection">
		/// The collection to enumerate
		/// </param>
		public CircularListEnumerator(CircularList<T> collection)
		{
			this.collection = collection;
			this.curElement = default(T);
			this.curIndex = -1;
		}
		
		/// <summary>
		/// Move to the next element
		/// </summary>
		/// <returns>
		/// Whether we are at the end of the list
		/// </returns>
		public bool MoveNext()
		{
			// handle empty lists
			if (this.collection.Count == 0)
			{
				return false;
			}

			// move the index
			this.curIndex++;

			// are we at the end of the list?
			if (this.curIndex >= this.collection.Count)
			{
				return false;
			}

			// update the element
			this.curElement = this.collection[this.curIndex];

			return true;
		}
		
		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset()
		{
			this.curIndex = -1;
		}
		
		void IDisposable.Dispose()
		{
		}
		
		/// <summary>
		/// The current element
		/// </summary>
		public T Current
		{
			get { return this.curElement; }
		}

		object System.Collections.IEnumerator.Current
		{
			get { return this.Current; }
		}

	}
}