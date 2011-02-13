using System.Collections.Generic;
using ClassLibrary;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccessLayer
{
    public class PreferencesProvider : GenericDataProvider<Preference>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CourseDAL class.
        /// </summary>
        public PreferencesProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CourseDataAccessLayer class.
        /// </summary>
        public PreferencesProvider(ISession session)
            : base(session)
        {
        }

        #endregion

        #region Public Methods

    	/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
    	public void AddPreference(Preference preference)
        {
            preference.Validate();
            using (ITransaction tx = Session.BeginTransaction())
            {
                try
                {
                    Session.Save(preference);
                    Session.Flush();
                    tx.Commit();
                    //return newID.Id;
                }
                catch (HibernateException)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

    	/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
    	public void DeletePreference(Preference preference)
        {
            using (ITransaction tx = Session.BeginTransaction())
            {
                try
                {
                    Session.Delete(preference);
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

        public IList<Preference> GetAllPreferencesByProfId(int profId)
        {
            DetachedCriteria preferences = DetachedCriteria.For<Preference>()
                .Add(Restrictions.Eq("Id.Professor.ProfId", profId))
                .AddOrder(new Order("Weight", false));
            IList<Preference> p = FindByDetachedCriteria(preferences);
            return p;

        }

        public Preference GetPreferenceByProfIdAndCourseId(int profId, int courseId)
        {
            DetachedCriteria preference = DetachedCriteria.For<Preference>()
                .Add(Restrictions.Eq("Id.Professor.ProfId", profId))
                .Add(Restrictions.Eq("Id.Course.CourseId", courseId));
            IList<Preference> p = FindByDetachedCriteria(preference);
            if (p != null) return p[0];
            return null;
        }

    	/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
    	public void SaveOrUpdatePreference(IList<Preference> preference)
        {
            using (ITransaction tx = Session.BeginTransaction())
            {
                try
                {
                    foreach (Preference p in preference)
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
    	public void UpdatePreference(Preference pref)
        {
            pref.Validate();
            using (ITransaction tx = Session.BeginTransaction())
            {
                try
                {
                    Session.Update(pref);
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

        public IList<Preference> GetAllPreferences()
        {

            DetachedCriteria allPreferences = DetachedCriteria.For<Preference>()
                .AddOrder(new Order("Id", true));
            return FindByDetachedCriteria(allPreferences);
        }
    }
}
