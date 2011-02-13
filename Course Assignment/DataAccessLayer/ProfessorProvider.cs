using System.Collections.Generic;
using ClassLibrary;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccessLayer
{
	public class ProfessorProvider : GenericDataProvider<Professor>
	{

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ProfDataAccessLayer class.
		/// </summary>
		public ProfessorProvider(ISession session)
			: base(session)
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProfDAL class.
		/// </summary>
		public ProfessorProvider()
		{
		}

		#endregion

		#region Public Methods

		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public int AddProf(Professor prof)
		{
			prof.Validate();
			int newID;
			using (ITransaction tx = Session.BeginTransaction())
			{
				try
				{
					newID = (int)Session.Save(prof);
					Session.Flush();
					tx.Commit();

				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
			//if (prof.UnassignedProf == true)
			//{
			//    clearIsUnassignedProfessorsExceptThis(prof);
			//}
			return newID;

		}

		//private void clearIsUnassignedProfessorsExceptThis(Professor professor)
		//{

		//    IList<Professor> allUnassigned = GetUnassignedProfessors();
		//    foreach (Professor prof in allUnassigned)
		//    {
		//        if (prof != professor && prof.UnassignedProf == true)
		//        {
		//            prof.UnassignedProf = false;
		//        }
		//    }
		//    SaveOrUpdateProfessor(allUnassigned);
		//}

		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void DeleteProf(Professor prof)
		{
			using (ITransaction tx = Session.BeginTransaction())
			{
				try
				{
					Session.Delete(prof);
					Session.Flush();
					tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		public IList<Professor> GetAllProfessors()
		{
			DetachedCriteria allProfessors = DetachedCriteria.For<Professor>()
				.AddOrder(new Order("FirstName", true))
				.AddOrder(new Order("LastName", true))
				.SetFetchMode("Preferences", FetchMode.Eager)
				.SetFetchMode("Courses", FetchMode.Eager)
				.SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer());
			return FindByDetachedCriteria(allProfessors);
		}

		public IList<Professor> GetAssignedProfessors()
		{
			DetachedCriteria unProf = DetachedCriteria.For<Professor>()
				.Add(Restrictions.Eq("UnassignedProf", false))
				.AddOrder(new Order("FirstName", true))
				.AddOrder(new Order("LastName", true))
				.SetFetchMode("Courses", FetchMode.Eager)
				.SetFetchMode("Preferences", FetchMode.Eager)
				.SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer());
			return FindByDetachedCriteria(unProf);
		}

		public IList<Professor> GetProfessorAndCourse(int profID)
		{
			DetachedCriteria prof = DetachedCriteria.For<Professor>()
				.Add(Restrictions.Eq("ProfId", profID));
			return FindByDetachedCriteria(prof);
		}

		public Professor GetProfessorByID(int profID)
		{
			return GetById(profID);
		}

		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void SaveOrUpdateProfessor(IList<Professor> professor)
		{
			using (ITransaction tx = Session.BeginTransaction())
			{
				try
				{
					foreach (Professor p in professor)
					{
						Session.SaveOrUpdate(p);
					}
					Session.Flush();
					tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void UpdateProf(Professor prof)
		{
			prof.Validate();
			using (ITransaction tx = Session.BeginTransaction())
			{
				try
				{
					Session.Update(prof);
					Session.Flush();
					tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
			//if (prof.UnassignedProf == true)
			//{
			//    clearIsUnassignedProfessorsExceptThis(prof);
			//}
		}

		#endregion

		#region Properties

		#endregion

	}
}