using System;

namespace Missing.Security.Authentication
{
	/// <summary>
	/// The status of a user
	/// </summary>
	/// <seealso cref="UserBase"/>
	public enum UserStatus
	{
		/// <summary>
		/// The user has been banned / disabled by an admin
		/// </summary>
		Banned = 0,
		
		/// <summary>
		/// The user is currently active and capable of logging in
		/// </summary>
		Active,
		
		/// <summary>
		/// The user has exceded the failed login attemp limit and has therefore been blocked
		/// </summary>
		AutoBlocked,
		
		/// <summary>
		/// The user has been deleted from the system
		/// </summary>
		Deleted
	}
}

