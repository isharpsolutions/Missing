using System;
using Missing.Data.Persistance.DataInterfaces;

namespace Missing.MongoProvider
{
	public class DbContext : IDbContext
	{
		public DbContext()
		{
		}

		#region IDbContext implementation
		public IDisposable BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public void CommitChanges()
		{
			throw new NotImplementedException();
		}

		public void CommitTransaction()
		{
			throw new NotImplementedException();
		}

		public void RollbackTransaction()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}

