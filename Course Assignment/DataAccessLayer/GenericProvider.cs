// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="GenericProvider.cs" company="PC">
//   Copyright 2008-2009 Ali Hmer
// </copyright>
// <summary>
//   Defines the GenericDataProvider type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
using System;

namespace DataAccessLayer
{
	using System.Collections.Generic;
	using NHibernate;
	using NHibernate.Criterion;

	/// <summary>
	/// Generic Data Provider Class
	/// </summary>
	/// <typeparam name="T">Generic type</typeparam>
	public class GenericDataProvider<T>
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the GenericDataProvider class.
		/// </summary>
		public GenericDataProvider()
		{
			Session = (new SessionManager()).GetSession();
		}

		/// <summary>
		/// Initializes a new instance of the GenericDataProvider class. 
		/// </summary>
		/// <param name="session">
		/// Issesion session
		/// </param>
		public GenericDataProvider(ISession session)
		{
			Session = session;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		/// <value>The session.</value>
		public ISession Session { get; set; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Finds the by detached criteria.
		/// </summary>
		/// <param name="dc">The detched criteria.</param>
		/// <returns>List of detached cirteria</returns>
		public IList<T> FindByDetachedCriteria(DetachedCriteria dc)
		{
			IList<T> list;
			using (var tx = Session.BeginTransaction())
			{
				try
				{
					list = FindByDetachedCriteria(dc, tx);
					tx.Rollback();
				}
				catch (Exception)
				{
					tx.Rollback();
					throw;
				}
			}
			return list;
		}

		/// <summary>
		/// Finds the by detached criteria.
		/// </summary>
		/// <param name="dc">The Detached Critirea.</param>
		/// <param name="tx">The transaction.</param>
		/// <returns></returns>
		public IList<T> FindByDetachedCriteria(DetachedCriteria dc, ITransaction tx)
		{
			return Session.Transaction == tx ? dc.GetExecutableCriteria(Session).List<T>() : FindByDetachedCriteria(dc);
		}

		/// <summary>
		/// Finds the by query.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns>List fin by query</returns>
		public IList<T> FindByQuery(string query)
		{
			IList<T> list;
			using (var tx = Session.BeginTransaction())
			{
				try
				{
					list = FindByQuery(query, tx);
					tx.Rollback();

				}
				catch (Exception)
				{
					tx.Rollback();
					throw;
				}
			}
			return list;
		}

		public IList<T> FindByQuery(string query, ITransaction tx)
		{
			return Session.Transaction == tx
			       	? Session.CreateQuery(query).List<T>()
			       	: FindByQuery(query);
		}

		public T GetById(object id, ITransaction tx)
		{
			return Session.Transaction == tx ? Session.Get<T>(id) : GetById(id);
		}

		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="id">The identity.</param>
		/// <returns>an instance of type T</returns>
		public T GetById(object id)
		{
			T object1;
			using (var tx = Session.BeginTransaction())
			{
				try
				{

					object1 = GetById(id, tx);
					tx.Rollback();
				}
				catch (Exception)
				{
					tx.Rollback();
					throw;
				}
			}
			return object1;
		}

		#endregion

		// public T FindOne(DetachedCriteria dc)
		// {
		//    return dc.GetExecutableCriteria(session).UniqueResult<T>;
		// }
	}
}
