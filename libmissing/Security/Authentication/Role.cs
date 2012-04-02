using System;
using System.Collections.Generic;

namespace Missing.Security.Authentication
{
	/// <summary>
	/// A user role.
	/// 
	/// Roles are attached to users and privileges are attached to roles.
	/// </summary>
	[Serializable]
	public class Role
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Security.Authentication.Role"/> class.
		/// </summary>
		public Role()
		{
			this.Name = String.Empty;
			this.Description = String.Empty;
			this.Privileges = new List<string>();
		}
		
		/// <summary>
		/// Get/set the name of the role
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Get/set description of the role
		/// </summary>
		public string Description { get; set; }
		
		/// <summary>
		/// Get/set full set of privileges
		/// </summary>
		public List<string> Privileges { get; set; }
		
		#region Object overrides
		/// <summary>
		/// Equality using the name of the roles
		/// </summary>
		/// <param name="obj">
		/// A <see cref="System.Object"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public override bool Equals(object obj)
		{
			Role other = obj as Role;
			
			if (other == null)
			{
				return false;
			}
			
			return this.Name.Equals(other.Name);
		}
		
		/// <summary>
		/// Hashcode based on the role name alone
		/// </summary>
		/// <returns>
		/// A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}
		#endregion Object overrides
	}
}

