using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Missing.Data.Persistance
{
	public enum RecordState
	{
		/// <summary>
		/// Entity is in an active and usable state
		/// </summary>
		Active,

		/// <summary>
		/// Entity is non active but still usable
		/// </summary>
		NonActive,

		/// <summary>
		/// Entity is marked as deleted and should not be used
		/// </summary>
		Deleted
	}
}
