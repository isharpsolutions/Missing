using System;

namespace Missing.Security.Authentication
{
	/// <summary>
	/// Interface for collections carrying <see cref="Role"/>
	/// </summary>
	public interface IRoleCollection : System.Collections.Generic.IList<Role>
	{
		/// <summary>
		/// Check whether this role collection has a role that has the given privilege
		/// </summary>
		/// <returns>
		/// <c>true</c> the privilege is contained within this role collection; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="privilege">
		/// The privilege to check for
		/// </param>
		bool HasPrivilege(string privilege);
	}
}