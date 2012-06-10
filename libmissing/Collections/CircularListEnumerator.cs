using System;
using System.Collections.Generic;

namespace Missing.Collections
{
	public class CircularListEnumerator<T> : IEnumerator<T>
	{
		private CircularList<T> collection;
		private int curIndex;
		private T curBox;

		public CircularListEnumerator(CircularList<T> collection)
		{
			this.collection = collection;
			this.curIndex = -1;
			this.curBox = default(T);
		}

		public bool MoveNext()
		{
			//Avoids going beyond the end of the collection.
			if (++curIndex >= this.collection.Count)
			{
				return false;
			}
			
			else
			{
				// Set current box to next item in collection.
				this.curBox = this.collection[this.curIndex];
			}
			
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
			get { return this.curBox; }
		}

		object System.Collections.IEnumerator.Current
		{
			get { return this.Current; }
		}

	}
}