using System;

namespace Missing.Collections
{
	/// <summary>
	/// Sort direction
	/// </summary>
	/// <remarks>
	/// We have this in Missing despite the fact that
	/// it already exists as <see cref="System.Web.UI.WebControls"/>.
	/// We think it would be very annoying having to include that namespace
	/// whenever you wish to use functionality from <see cref="Missing.Collections"/>
	/// </remarks>
	public enum SortDirection
	{
		/// <summary>
		/// Sort from smallest to largest. For example, from A to Z.
		/// </summary>
		Ascending,
		
		/// <summary>
		/// Sort from largest to smallest. For example, from Z to A.
		/// </summary>
		Descending
	}
}