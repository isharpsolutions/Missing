using System;
using System.Collections.Generic;

namespace Missing.Collections
{
	public class CircularListEnumerator<T> : IEnumerator<T>
	{
		private CircularList<T> collection;
		private int curIndex;
		private T curElement;
		
		private int firstOuputIndex;

		public CircularListEnumerator(CircularList<T> collection)
		{
			this.collection = collection;
			this.curIndex = this.collection.CurrentIndex;
			this.curElement = default(T);
			
			this.firstOuputIndex = -1;
		}
		
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

		public void Reset()
		{
			this.curIndex = -1;
		}

		void IDisposable.Dispose()
		{
		}

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