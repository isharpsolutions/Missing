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
		/// The index of the first item
		/// we output
		/// </summary>
		private int firstOuputIndex;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Collections.CircularListEnumerator`1"/> class.
		/// </summary>
		/// <param name="collection">
		/// The collection to enumerate
		/// </param>
		public CircularListEnumerator(CircularList<T> collection)
		{
			this.collection = collection;
			this.curIndex = this.collection.CurrentIndex;
			this.curElement = default(T);
			
			this.firstOuputIndex = -1;
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
			
			// "flip" the index around the edge if necessary
			if (this.curIndex >= this.collection.Count)
			{
				this.curIndex = 0;
			}
			
			// if we have reached the first index we output
			// we are at the end of the list
			if (this.curIndex == this.firstOuputIndex)
			{
				return false;
			}
			
			
			// store the first index we output
			if (this.firstOuputIndex == -1)
			{
				this.firstOuputIndex = this.curIndex;
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
			this.firstOuputIndex = -1;
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