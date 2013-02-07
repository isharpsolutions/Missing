using System;
using System.Web;
using NHibernate;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using System.Configuration;

namespace Missing.NhibernateProvider.Web
{
    /// <summary>
    /// Taken from http://nhforge.org/blogs/nhibernate/archive/2011/03/03/effective-nhibernate-session-management-for-web-apps.aspx
    /// </summary>
    public class SessionPerRequestModule : IHttpModule
    {
		/// <summary>
		/// The name of the appSettings key used to control NHibernate ISession.FlushMode
		/// </summary>
		public static string SessionFlushModeConfigKey = "Missing.NHibernate.ISession.FlushMode";

		/// <summary>
		/// Whether the flushmode has been defined by looking at config
		/// </summary>
		private static bool SessionFlushModeIsSet = false;

		/// <summary>
		/// The flushmode to use
		/// </summary>
		private static FlushMode SessionFlushMode = FlushMode.Auto;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;

			if (!SessionFlushModeIsSet)
			{
				SessionFlushModeIsSet = true;

				string fm = ConfigurationManager.AppSettings.Get(SessionFlushModeConfigKey);

				if (!String.IsNullOrEmpty(fm))
				{
					//
					// let exceptions bubble through, so developers using this
					// will see the error
					//
					try
					{
						SessionFlushMode = (FlushMode)Enum.Parse(typeof(FlushMode), fm, true);
						Missing.Diagnostics.Log.Information("Session.FlushMode value of '{0}' defined in *.config by appSettings.{1}", SessionFlushMode, SessionFlushModeConfigKey);
					}

					catch (Exception ex)
					{
						//
						// found this pattern at
						// http://stackoverflow.com/a/136092
						//

						if (ex is ArgumentException || ex is OverflowException)
						{
							string names = String.Join(", ", Enum.GetNames(typeof(FlushMode)));
							throw new ArgumentException(String.Format("The value of appSettings.{1} must be one of {2}, but was '{3}'", SessionFlushModeConfigKey, names, fm), ex);
						}

						throw;
					}
				}
			}
        }

        private void ContextBeginRequest(object sender, EventArgs e)
        {
            foreach (var sessionFactory in GetSessionFactories())
            {
                var localFactory = sessionFactory;

                LazySessionContext.Bind(new Lazy<ISession>(() => BeginSession(localFactory)), sessionFactory);
            }
        }

        private static ISession BeginSession(ISessionFactory sessionFactory)
        {
            var session = sessionFactory.OpenSession();

			session.FlushMode = SessionFlushMode;

            session.BeginTransaction();
            return session;
        }

        private void ContextEndRequest(object sender, EventArgs e)
        {
            foreach (var sessionfactory in GetSessionFactories())
            {
                var session = LazySessionContext.UnBind(sessionfactory);
                if (session == null) continue;
                EndSession(session);
            }
        }

        private static void EndSession(ISession session)
        {
			if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }

            session.Dispose();
        }

        public void Dispose() { }

        /// <summary>
        /// Retrieves all ISessionFactory instances via IoC
        /// </summary>
        private IEnumerable<ISessionFactory> GetSessionFactories()
        {
            var sessionFactories = ServiceLocator.Current.GetAllInstances<ISessionFactory>();

            if (sessionFactories == null || !sessionFactories.Any())
                throw new TypeLoadException("At least one ISessionFactory has not been registered with IoC");

            return sessionFactories;
        }
    }
}
