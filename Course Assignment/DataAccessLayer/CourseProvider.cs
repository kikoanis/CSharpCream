using System.Collections.Generic;
using ClassLibrary;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccessLayer {
	/// <summary>
	/// 
	/// </summary>
	public class CourseProvider : GenericDataProvider<Course> {

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CourseProvider class.
		/// </summary>
		public CourseProvider()
		{
		}

		/// <summary>
		/// Initializes a new instance of the CourseProvider class.
		/// </summary>
		/// <param name="session"></param>
		public CourseProvider(ISession session)
			: base(session) {
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds the course.
		/// </summary>
		/// <param name="course">The course.</param>
		/// <returns></returns>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public int AddCourse(Course course) {
			course.Validate();
			using (ITransaction tx = Session.BeginTransaction()) {
				try {

					var newID = (int)Session.Save(course);
					Session.Flush();
					tx.Commit();
					return newID;
				} catch (HibernateException) {
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Deletes the course.
		/// </summary>
		/// <param name="course">The course.</param>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void DeleteCourse(Course course) {
			using (ITransaction tx = Session.BeginTransaction()) {
				try {
					Session.Delete(course);
					Session.Flush();
					tx.Commit();
				} catch (HibernateException) {
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Gets all courses.
		/// </summary>
		/// <returns></returns>
		public IList<Course> GetAllCourses()
		{
			DetachedCriteria allCourses = DetachedCriteria.For<Course>()
				.AddOrder(new Order("CourseName", true))
				.SetFetchMode("AssignedProfessor", FetchMode.Eager)
				.SetFetchMode("ProfessorsAssociatedList", FetchMode.Eager)
				.SetResultTransformer(new NHibernate.Transform.DistinctRootEntityResultTransformer());
			
			return FindByDetachedCriteria(allCourses);
		}

		/// <summary>
		/// Gets the course by ID.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Course GetCourseByID(int id) {
			return GetById(id);
		}

		/// <summary>
		/// Gets the non nullable prof ID.
		/// </summary>
		/// <returns></returns>
		public IList<Course> GetNonNullableProfID() {
			DetachedCriteria nonNullableProfs = DetachedCriteria.For<Course>()
				.Add(Restrictions.IsNotNull("AssignedProfessor"));
			return FindByDetachedCriteria(nonNullableProfs);
			//return GetCoursesByQuery("select from Course c where c.AssignedProfessor is not null");
		}

		/// <summary>
		/// Gets the nullable prof ID.
		/// </summary>
		/// <returns></returns>
		public IList<Course> GetNullableProfID() {
			DetachedCriteria nullableProfs = DetachedCriteria.For<Course>()
				.Add(Restrictions.IsNull("AssignedProfessor"));
			return FindByDetachedCriteria(nullableProfs);
			//return GetCoursesByQuery("select from Course c where c.AssignedProfessor is null");
		}

		/// <summary>
		/// Saves the or update courses.
		/// </summary>
		/// <param name="courses">The courses.</param>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void SaveOrUpdateCourses(IList<Course> courses) {
			using (ITransaction tx = Session.BeginTransaction()) {
				try {
					foreach (Course c in courses) {
						Session.SaveOrUpdate(c);
					}

					Session.Flush();
					tx.Commit();
				} catch (HibernateException) {
					tx.Rollback();
					throw;
				}
			}
		}

		/// <summary>
		/// Updates the course.
		/// </summary>
		/// <param name="course">The course.</param>
		/// <exception cref="HibernateException"><c>HibernateException</c>.</exception>
		public void UpdateCourse(Course course) {
			course.Validate();
			using (ITransaction tx = Session.BeginTransaction()) {
				try
				{
					Session.Update(course);
					tx.Commit();
				}
				catch (HibernateException)
				{
					tx.Rollback();
					throw;
				}
			}
		}

		#endregion

	}
}