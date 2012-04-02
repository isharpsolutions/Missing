using System;

namespace Missing.Security.Authentication
{
	/// <summary>
	/// Base class for user systems.
	/// 
	/// The class has been implemented without any persistence methods.
	/// The idea is that each consumer must wrap the class to suit his
	/// particular persistence strategy.
	/// 
	/// This is also why we have not included an "Id" property.
	/// 
	/// All members are marked as "virtual" to support frameworks like NHibernate.
	/// </summary>
	/// <typeparam name="TRoleCollection">
	/// The type implementing the role collection.
	/// 
	/// If your persistence strategy does not require specific collection classes,
	/// you can just use <see cref="RoleCollection"/>
	/// </typeparam>
	[Serializable]
	public abstract class UserBase<TRoleCollection> : System.Security.Principal.IIdentity where TRoleCollection : IRoleCollection, new() 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Missing.Security.Authentication.UserBase`1"/> class.
		/// </summary>
		public UserBase()
		{
			this.IsAuthenticated = false;
			this.Created = DateTime.MinValue;
			this.LastLogin = DateTime.MinValue;
			this.Status = UserStatus.Banned;
			this.UserName = String.Empty;
			this.FullName = String.Empty;
			this.Roles = new TRoleCollection();
			this.FailedLoginAttempts = 0;
		}
		
		
		
		#region IIdentity implementation
		/// <summary>
		/// Get the authentication type
		/// </summary>
		public virtual string AuthenticationType
		{
			get { return "SQLAuthentication"; }
		}

		/// <summary>
		/// Get whether the user has beeen authenticated or not.
		/// </summary>
		public virtual bool IsAuthenticated { get; set; }

		/// <summary>
		/// Get the username
		/// </summary>
		public virtual string Name
		{
			get { return this.UserName; }
		}
		#endregion
		
		/// <summary>
		/// Get/set username
		/// </summary>
		public virtual string UserName { get; set; }
		
		/// <summary>
		/// Get/set the full real name of the user
		/// </summary>
		public virtual string FullName { get; set; }
		
		/// <summary>
		/// Get when the user was created
		/// </summary>
		public virtual DateTime Created { get; protected set; }
		
		/// <summary>
		/// Get when last (successfull) login occurred
		/// </summary>
		public virtual DateTime LastLogin { get; protected set; }
		
		/// <summary>
		/// Get/set status
		/// </summary>
		public virtual UserStatus Status { get; set; }
		
		/// <summary>
		/// Get/set the set of roles that user has
		/// </summary>
		public virtual TRoleCollection Roles { get; set; }
		
		/// <summary>
		/// Get/set the number of failed login attempts (since last successfull login)
		/// </summary>
		public virtual int FailedLoginAttempts { get; set; }
		
		/// <summary>
		/// Check whether this user has a given role
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance has the specified role; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="role">
		/// If set to <c>true</c> role.
		/// </param>
		public virtual bool HasRole(Role role)
		{
			return this.Roles.Contains(role);
		}
		
		/// <summary>
		/// Determines whether the user has a given <paramref name="privilege"/> 
		/// </summary>
		/// <param name="privilege">
		/// A <see cref="System.String"/> containing the privilege to check for
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/> which is <c>true</c> if and only if the user has the given <paramref name="privilege"/>. <c>false</c> otherwise
		/// </returns>
		public virtual bool HasPrivilege(string privilege)
		{
			return this.Roles.HasPrivilege(privilege);
		}
	}
}