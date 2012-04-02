using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Missing.Security.Authentication
{
	/// <summary>
	/// A collection of user roles
	/// </summary>
	public class RoleCollection : Collection<Role>, IRoleCollection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Security.Authentication.RoleCollection"/> class.
		/// </summary>
		public RoleCollection() : base()
		{
		}
		
		/// <summary>
		/// Check whether this role collection has a role that has the given privilege
		/// </summary>
		/// <returns>
		/// <c>true</c> the privilege is contained within this role collection; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="privilege">
		/// The privilege to check for
		/// </param>
		public bool HasPrivilege(string privilege)
		{
			return	(
						from yy in base.Items
						from xx in yy.Privileges
						where xx == privilege
						select xx
					).FirstOrDefault() != default(string);
		}
	}
}

