// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="PC">
//   2008-2009
// </copyright>
// <summary>
//   Defines the SessionManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataAccessLayer 
{
    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    /// Class for managing nhibernate sessions
    /// </summary>
    public class SessionManager 
    {
		#region Fields

        /// <summary>
        /// private variable for sessionFactory
        /// </summary>
        private readonly ISessionFactory sessionFactory;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SessionManager"/> class. 
		/// Initializes a new instance of the NHibernateSessionManager class.
		/// </summary>
		public SessionManager() 
        {
			sessionFactory = SessionFactory;
        }

        #endregion

		#region Properties

	    /// <summary>
	    /// Gets SessionFactory.
	    /// </summary>
	    private static ISessionFactory SessionFactory 
        {
			get 
            {
				return (new Configuration()).Configure().BuildSessionFactory();
			}
		}

		#endregion

		#region Public Methods

        /// <summary>
        /// A public method to return an Nuhibernate session
        /// </summary>
        /// <returns>
        /// ISession object
        /// </returns>
        public ISession GetSession() 
        {
			return sessionFactory.OpenSession(new ProviderInterceptor());
		}

		#endregion
    }
}
