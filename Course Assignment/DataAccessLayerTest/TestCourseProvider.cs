// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestCourseProvider.cs" company="U of R">
//   2009
// </copyright>
// <summary>
//   Defines the TestCourseProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataAccessLayerTest 
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;
	using ClassLibrary;
	using DataAccessLayer;
	using MbUnit.Framework;
	using Microdesk.Utility.UnitTest;
	using NHibernate.Criterion;

	/// <summary>
	/// Testing data access layer
	/// </summary>
	[TestFixture]
	public class TestCourseProvider : DatabaseUnitTestBase
	{
		#region Constants

		/// <summary>
		/// A60 Char String
		/// </summary>
		private const string A60CharString = "012345678901234567890123456789012345678901234567890123456789";

		#endregion

		#region Fields

		/// <summary>
		/// Course provider
		/// </summary>
		private CourseProvider provider;

		/// <summary>
		/// ISession object
		/// </summary>
		private NHibernate.ISession session;

		/// <summary>
		/// Session Manager
		/// </summary>
		private SessionManager sessionManager;

		#endregion

		#region Public Methods

		/// <summary>
		/// Testing random generator
		/// </summary>
		[Test]
        public void TestRandom()
        {
            var rnd = new Random();
            var names = new List<string>();
            for (int i = 0; i < 1000000; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < 10; j++)
                {
                	try
                	{
                		sb.Append((char) rnd.Next(Convert.ToInt32("a"), Convert.ToInt32("z")));
                	}
                	catch (ArgumentOutOfRangeException)
                	{
                	}
                }

                names.Add(sb.ToString());
            }

            var matches = new List<string>();

            var sw = new Stopwatch();
            sw.Start();
            foreach (string s in names)
            {
                if (s.StartsWith("d"))
                {
                    matches.Add(s);
                }
            }

			sw.Stop();
			Console.WriteLine(
				"standart foreach: {0}",
				sw.Elapsed.TotalMilliseconds);

			sw.Reset();
            sw.Start();

			// var matchesLinq = (from s in names
			//                   where s.StartsWith("d")
			//                   select s).ToArray();
            sw.Stop();
			Console.WriteLine(
				"linq to object: {0}",
				sw.Elapsed.TotalMilliseconds);
        }

		/// <summary>
		/// Generate random numbers
		/// </summary>
		[Test]
        public void GenerateRandomNumbers()
        {
			var r = new Random();
			for (int i = 0; i < 20; i++)
			{
				Console.WriteLine(r.Next(1, 10));
			}
        }

		/// <summary>
		/// Add course with exception test
		/// </summary>
		[Test]

		// [ExpectedException(typeof(NHibernate.HibernateException))]
		[ExpectedException(typeof(ClassLibrary.Rules.RuleViolationException))]

		// [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
		public void AddCourseThrowsExceptionOnFail() 
		{
			provider.AddCourse(BuildInvalidCourse());
		}

		/// <summary>
		/// Cann add course
		/// </summary>
		[Test]
		public void CanAddCourse() 
		{
			// DaysOfWeek = "MWF"
			var course = new Course
			{
				CourseName = "CS199",
				Monday = true,
				Wednesday = true,
				Friday = true,
				StartHour = 13,
				StartMinute = 0,
				EndHour = 13,
				EndMinute = 50,
				AssignedProfessor = null
			};
			int newIdentity = provider.AddCourse(course);
			Course testCourse = provider.GetCourseByID(newIdentity);

			Assert.IsNotNull(testCourse);
		}

		/// <summary>
		/// Determines whether this instance [can delete course].
		/// </summary>
		[Test]
		public void CanDeleteCourse() 
		{
			Course course = provider.GetCourseByID(28);
			provider.DeleteCourse(course);

			course = provider.GetCourseByID(28);
			Assert.IsNull(course);
		}

		/// <summary>
		/// Can evict course from session test
		/// </summary>
		[Test]
		public void CanEvictCourseFromSession() 
		{
			Course course = provider.GetCourseByID(3);

			Assert.IsTrue(session.Contains(course));

			session.Evict(course);

			Assert.IsFalse(session.Contains(course));
		}

		/// <summary>
		/// can get all courses
		/// </summary>
		[Test]
		public void CanGetAllCourses() 
		{
			HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
			Assert.AreEqual(18, provider.GetAllCourses().Count);
		}

		/// <summary>
		/// can get all non nullable prof id
		/// </summary>
		[Test]
		public void CanGetAllNonNullableProfID() 
		{
			Assert.AreEqual(4, provider.GetNonNullableProfID().Count);
		}

		/// <summary>
		/// Can get all nullable prof id
		/// </summary>
		[Test]
		public void CanGetAllNullableProfID() 
		{
			Assert.AreEqual(20, provider.GetNullableProfID().Count);
		}

		/// <summary>
		/// can get course 3
		/// </summary>
		[Test]
		public void CanGetCourse3() 
		{
			Assert.AreEqual(3, provider.GetCourseByID(3).CourseId);
		}

		/// <summary>
		/// can get customer by arbitary critiries
		/// </summary>
		[Test]
		public void CanGetCustomersByArbitraryCriteria() 
		{
			DetachedCriteria cs201Course = DetachedCriteria.For<Course>()
				.Add(Restrictions.Eq("CourseName", "CS201"));

			IList<Course> courses = provider.FindByDetachedCriteria(cs201Course);

			Assert.AreEqual(1, courses.Count);
		}

		/// <summary>
		/// Can save or update courses
		/// </summary>
		[Test]
		
		// [ExpectedException(typeof(NHibernate.NonUniqueObjectException))]
		[ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
		public void CanSaveOrUpdateCourses()
		{
			IList<Course> courses = provider.GetAllCourses();

			if (courses.Count == 0)
			{
			}

			// Assert.Warning("You aren't testing what you think you are here!");
            const string NewCourseCourseName = "CSTesting";
			foreach (Course c in courses)
			{
				c.CourseName = NewCourseCourseName;
			}

			var c1 = new Course
			         	{
			         		CourseName = NewCourseCourseName,
			         		Monday = true,
			         		Wednesday = true,
			         		Friday = true,
			         		StartHour = 13,
			         		StartMinute = 0,
			         		EndHour = 14,
			         		EndMinute = 15
			         	};
			var c2 = new Course
			         	{
			         		CourseName = NewCourseCourseName,
			         		Monday = true,
			         		Wednesday = true,
			         		Friday = true,
			         		StartHour = 13,
			         		StartMinute = 0,
			         		EndHour = 14,
			         		EndMinute = 15
			         	};

			courses.Add(c1);
			courses.Add(c2);

			int initialListCount = courses.Count;

			provider.SaveOrUpdateCourses(courses);

			int testListCount = provider.GetAllCourses().Count;

			Assert.AreEqual(initialListCount, testListCount);
		}

		/// <summary>
		/// Can throw exception on concurrency violation
		/// </summary>
		[Test]
		[ExpectedException(typeof(NHibernate.StaleObjectStateException))]
		public void CanThrowExceptionOnConcurrencyViolation() 
		{
			// using (session)
			// {
			Course c1 = provider.GetCourseByID(3);

			// }
			ResetSessionForProvider();

			// using (session)
			// {
			Course c2 = provider.GetCourseByID(3);

			// }
			ResetSessionForProvider();

			c1.CourseName = "hello";
			c2.CourseName = "goodbye";

			// using (session)
			// {
			provider.UpdateCourse(c1);

			// }
			ResetSessionForProvider();

			// using (session)
			// {
			provider.UpdateCourse(c2);

			// }
		}

		/// <summary>
		/// Can update course
		/// </summary>
		[Test]
		public void CanUpdateCourse() 
		{
			Course course = provider.GetCourseByID(131);
			const string UpdataedName = "CS299";
			course.CourseName = UpdataedName;
			provider.UpdateCourse(course);

			course = provider.GetCourseByID(131);
			Assert.AreEqual(UpdataedName, course.CourseName);
		}

		/// <summary>
		/// Can update course and reset assigned professor
		/// </summary>
		[Test]
		public void CanUpdateCourseAndResetAssignedProfessor() 
		{
			Course course = provider.GetCourseByID(3);
			course.AssignedProfessor = null;
			provider.UpdateCourse(course);

			course = provider.GetCourseByID(3);
			Assert.AreEqual(null, course.AssignedProfessor);
		}

		/// <summary>
		/// Course can reaasociate with new session and set non dirty
		/// </summary>
		[Test]
		public void CourseCanBeReassociatedWithNewSessionAndSetNonDirty() 
		{
			Course course = provider.GetCourseByID(3);

			Assert.IsTrue(session.Contains(course));

			ResetSessionForProvider();

			Assert.IsFalse(session.Contains(course));

			session.Lock(course, NHibernate.LockMode.None);

			provider.UpdateCourse(course);

			Assert.IsTrue(session.Contains(course));
		}

		/// <summary>
		/// course can reassociated with new session
		/// </summary>
		[Test]
		public void CourseCanBeReassociatedWithNewSession() 
		{
			Course course = provider.GetCourseByID(3);

			Assert.IsTrue(session.Contains(course));

			ResetSessionForProvider();

			Assert.IsFalse(session.Contains(course));

			provider.UpdateCourse(course);

			Assert.IsTrue(session.Contains(course));
		}

		/// <summary>
		/// Course is not associated with new session
		/// </summary>
		[Test]
		public void CourseIsNotAssociatedWithNewSession() 
		{
			Course course = provider.GetCourseByID(3);

			Assert.IsTrue(session.Contains(course));

			ResetSessionForProvider();

			Assert.IsFalse(session.Contains(course));
		}

		/// <summary>
		/// Get my test data xml file
		/// </summary>
		
		public void GetMyTestDataXMLFile() 
		{
			var i = 0;
			SaveTestDatabase();
		}

		// [Test]
		// public void SaveOrUpdateCourseThrowsExceptionOnFail()
		// {
		//    IList<Course> courses = provider.GetNullableProfID();

		// if (courses.Count == 0)
		//    {
		//    }
		//    //Assert.Warning("You aren't testing what you think you are here!");

		// const string newCourseName = "CS999";
		//    foreach (Course c in courses)
		//    {
		//        c.CourseName = newCourseName;
		//    }
		// Course c1;
		//    try
		//    {
		//        c1 = BuildInvalidCourse();
		//    }
		//    catch (System.ArgumentOutOfRangeException)
		//    {
		//        return;
		//    }
		// var c2 = new Course { CourseName = newCourseName, AssignedProfessor = null };
		//    try
		//    {
		//        courses.Add(c1);
		//        courses.Add(c2);
		//    }
		//    catch (System.ArgumentOutOfRangeException)
		//    {
		//        ResetSessionForProvider();
		//    }
		// try
		//    {
		//        provider.SaveOrUpdateCourses(courses);
		//    }
		//    catch (NHibernate.HibernateException)
		//    {
		//        ResetSessionForProvider();
		//    }
		//    int testListCount = provider.GetNullableProfID().Count;
		// Assert.AreEqual(26, testListCount);
        
		// }

		/// <summary>
		/// Setup database
		/// </summary>
		[SetUp]
		public void Setup() 
		{
			DatabaseSetUp();
			session = sessionManager.GetSession();
			provider = new CourseProvider(session);
		}

		/// <summary>
		/// Tear down database
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			DatabaseTearDown();
		}

		/// <summary>
		/// Test fixture setup
		/// </summary>
		[FixtureSetUp]
		public void TestFixtureSetup() 
		{
			DatabaseFixtureSetUp();
			sessionManager = new SessionManager();
		}

		/// <summary>
		/// Test fixture tear down
		/// </summary>
		[FixtureTearDown]
		public void TestFixtureTearDown() 
		{
			DatabaseFixtureTearDown();
		}

		/// <summary>
		/// Update course can throw exception on fail
		/// </summary>
		[Test]

		// [ExpectedException(typeof(NHibernate.HibernateException))]
		[ExpectedException(typeof(ClassLibrary.Rules.RuleViolationException))]

		// [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
		public void UpdateCourseCanThrowExceptionOnFail() 
		{
			Course course = provider.GetCourseByID(28);
			course.CourseName = A60CharString;
			provider.UpdateCourse(course);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Builds invalid course
		/// </summary>
		/// <returns>
		/// Course object
		/// </returns>
		public static Course BuildInvalidCourse()
		{
			// course name is assigned a string with more than 50 chars length
			return new Course { CourseName = A60CharString };
		}

		/// <summary>
		/// Reset Session for provider
		/// </summary>
		private void ResetSessionForProvider() 
		{
			session.Close();
			session = sessionManager.GetSession();
			provider.Session = session;
		}

		#endregion
	}
}