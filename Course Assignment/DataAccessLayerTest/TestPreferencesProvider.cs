using ClassLibrary;
using DataAccessLayer;
using MbUnit.Framework;
using Microdesk.Utility.UnitTest;

namespace DataAccessLayerTest {
	///<summary>
	///
	///</summary>
	[TestFixture]
	public class TestPreferencesProvider : DatabaseUnitTestBase {
		#region Fields

		private PreferencesProvider provider;

		private NHibernate.ISession session;

		private SessionManager sessionManager;

		#endregion

		#region Public Methods

		[Test]
		//[ExpectedException(typeof(NHibernate.Exceptions.GenericADOException))]
		public void CanAddPreference() {
			Professor p = new ProfessorProvider(session).GetProfessorByID(19);
			Course c = new CourseProvider(session).GetCourseByID(6);
			var prefID = new PreferencesId(p, c);

			var pref = new Preference(prefID) { Weight = 3 };
			provider.AddPreference(pref);
			Preference testPref = provider.GetPreferenceByProfIdAndCourseId(19, 6);
			Assert.AreEqual(c, testPref.Id.Course);
			Assert.AreEqual(p, testPref.Id.Professor);
			//Assert.IsNotNull(testPref);
			//provider.AddPreference(pref);
		}

		[SetUp]
		public void Setup() {
			DatabaseSetUp();
			session = sessionManager.GetSession();
			provider = new PreferencesProvider(session);
		}

		[TearDown]
		public void TearDown() {
			DatabaseTearDown();
		}

		[FixtureSetUp]
		public void TestFixtureSetup() {
			DatabaseFixtureSetUp();
			sessionManager = new SessionManager();
		}

		[FixtureTearDown]
		public void TestFixtureTearDown() {
			DatabaseFixtureTearDown();
		}

		#endregion

		#region Private Methods

		//private static Course BuildInvalidCourse()
		//{
		//    //course name is assigned a string with more than 50 chars length
		//    return new Course() { CourseName = A60_CHAR_STRING };
		//}

		private void ResetSessionForProvider() {
			session = sessionManager.GetSession();
			provider.Session = session;
		}

		#endregion

	}
}
