using System.Collections.Generic;
using ClassLibrary;
using DataAccessLayer;
using MbUnit.Framework;
using Microdesk.Utility.UnitTest;

namespace DataAccessLayerTest
{
    [TestFixture]
    public class TestProfessorProvider : DatabaseUnitTestBase
    {

        #region Constants

        private const string A60CharString = "012345678901234567890123456789012345678901234567890123456789";

        #endregion

        #region Fields

        private ProfessorProvider provider;

        private NHibernate.ISession session;

        private SessionManager sessionManager;

        #endregion

        #region Public Methods

        [Test]
        //[ExpectedException(typeof(NHibernate.HibernateException))]
        [ExpectedException(typeof(ClassLibrary.Rules.RuleViolationException))]
        //[ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void AddProfThrowsExceptionOnFail()
        {
            provider.AddProf(BuildInvalidProf());
        }

        [Test]
        public void CanAddProf()
        {
            var prof = new Professor
            {
                PTitle = "Dr.",
                FirstName = "Lisa",
                LastName = "Afnan" ,
                NoOfCourses =3,
                UnassignedProf = false
            };
            int newIdentity = provider.AddProf(prof);
            Professor testProf = provider.GetProfessorByID(newIdentity);

            Assert.IsNotNull(testProf);

        }

        /// <summary>
        /// Determines whether this instance [can delete prof].
        /// </summary>
        [Test]
        public void CanDeleteProf()
        {
            Professor prof = provider.GetProfessorByID(8);
            provider.DeleteProf(prof);

            prof = provider.GetProfessorByID(8);
            Assert.IsNull(prof);
        }

        [Test]
        [Ignore]
        public void CanDemonstrateLazyLoadingSQLSequence()
        {
            System.Threading.Thread.Sleep(10000);

            Professor prof = provider.GetProfessorByID(24);

            System.Threading.Thread.Sleep(10000);

            int numOrders = prof.Courses.Count;
        }

        [Test]
        public void CanGetAllProf()
        {
            Assert.AreEqual(14, provider.GetAllProfessors().Count);
        }

        [Test]
        public void CanGetProf3()
        {
            Professor p = provider.GetProfessorByID(21);
            //Assert.Warning(p.FullName);
            Assert.AreEqual(21, p.ProfId);
        }

        [Test]
        public void CanGetProfAndCoursesByProfId()
        {
            Professor prof = provider.GetProfessorByID(21);
            Assert.AreEqual(21, prof.ProfId);
            IList<Course> lc = prof.Courses;
            IEnumerator<Course> c = lc.GetEnumerator();
            c.MoveNext();
            //Assert.Warning("Course Name: " + c.Current.CourseName);
            Assert.GreaterThanOrEqualTo(prof.Courses.Count, 1);
        }

        [Test]
        public void CanGetProfAndCourses()
        {
            Assert.AreEqual(1, provider.GetProfessorAndCourse(21).Count);
        }

        [Test]
        public void CanSaveOrUpdateProfessor()
        {
            IList<Professor> professor = provider.GetAllProfessors();

            if (professor.Count == 0)
            {
            	
            }
                //Assert.Warning("You aren't testing what you think you are here!");

            const string newProfProfName = "CSPROFESSOR";
            //foreach (Professor p in Professor)
            //{
            //    p.Name.FirstName = NEW_PROF_PROF_NAME;
            //    p.Name.LastName = "Hmer";
            //}

            var p1 = new Professor { PTitle = "Mr.", FirstName = newProfProfName, LastName = "Hmer" , NoOfCourses = 3, UnassignedProf = false };
            var p2 = new Professor { PTitle = "Mr.", FirstName = newProfProfName, LastName = "Hmker" , NoOfCourses = 3, UnassignedProf = false };
            professor.Add(p1);
            professor.Add(p2);

            int initialListCount = professor.Count;

            provider.SaveOrUpdateProfessor(professor);

            int testListCount = provider.GetAllProfessors().Count;

            Assert.AreEqual(initialListCount, testListCount);
        }

        [Test]
        [ExpectedException(typeof(NHibernate.HibernateException))]
        //[ExpectedException(typeof(MbUnit.Core.Exceptions.ExceptionNotThrownException))]
        //[ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void CanSaveOrUpdateProfessorThrowsExceptionOnFail()
        {
            IList<Professor> professor = provider.GetAllProfessors();

            if (professor.Count == 0)
            {
            	
            }
                //Assert.Warning("You aren't testing what you think you are here!");

            const string newProfProfName = "CSPROFESSOR";
            foreach (Professor p in professor)
            {
                p.FirstName = newProfProfName;
                p.LastName = "Hmer";
            }

            Professor p1 = BuildInvalidProf();
            var p2 = new Professor { FirstName = newProfProfName, LastName = "Hmer" , NoOfCourses = 3, UnassignedProf = false };

            professor.Add(p1);
            professor.Add(p2);

            int initialListCount = professor.Count;
            //try
            //{
            provider.SaveOrUpdateProfessor(professor);
            //}
            //catch (NHibernate.HibernateException)
            //{

            //}

            int testListCount = provider.GetAllProfessors().Count;
            // has a roll back really happened!!!
            Assert.AreNotEqual(initialListCount, testListCount);
        }

        [Test]
       // [ExpectedException(typeof(NHibernate.StaleObjectStateException))]
        public void CanThrowExceptionOnConcurrencyViolationOnDelete()
        {
            Professor p1;
            Professor p2;
            using (session)
            {
                p1 = provider.GetProfessorByID(19);
            }
            ResetSessionForProvider();
            using (session)
            {
                p2 = provider.GetProfessorByID(16);
            }
            ResetSessionForProvider();
            using (session)
            {
                provider.DeleteProf(p1);
            }

            ResetSessionForProvider();

            using (session)
            {
                provider.DeleteProf(p2);
            }
        }

        [Test]
        public void CanUpdateProf()
        {
            Professor prof = provider.GetProfessorByID(5);
            const string updatedName = "LISA A.";
            prof.FirstName = updatedName;
            prof.LastName = "Fan";
            provider.UpdateProf(prof);

            prof = provider.GetProfessorByID(5);
            //Assert.Warning(prof.FullName);
            Assert.AreEqual(updatedName, prof.FirstName);
        }

        public void GetMyTestDataXMLFile()
        {
            SaveTestDatabase();
        }

        [SetUp]
        public void Setup()
        {
            DatabaseSetUp();
            session = sessionManager.GetSession();
            provider = new ProfessorProvider(session);

        }

        [TearDown]
        public void TearDown()
        {
            DatabaseTearDown();
        }

        [FixtureSetUp]
        public void TestFixtureSetup()
        {
            DatabaseFixtureSetUp();
            sessionManager = new SessionManager();
        }

        [FixtureTearDown]
        public void TestFixtureTearDown()
        {
            DatabaseFixtureTearDown();
        }

        [Test]
        //[ExpectedException(typeof(NHibernate.HibernateException))]
        //[ExpectedException(typeof(System.ArgumentOutOfRangeException))]
		[ExpectedException(typeof(ClassLibrary.Rules.RuleViolationException))]
        public void UpdateProfCanThrowExceptionOnFail()
        {
            Professor prof = provider.GetProfessorByID(4);
            prof.FirstName = A60CharString;
            prof.LastName = "Hmer";
            provider.UpdateProf(prof);
        }

        #endregion

        #region Private Methods

        private static Professor BuildInvalidProf()
        {
            //prof name is assigned a string with more than 50 chars length
            return new Professor { FirstName = A60CharString, LastName = A60CharString };
        }

        private void ResetSessionForProvider()
        {
            session = sessionManager.GetSession();
            provider.Session = session;
        }

        #endregion

    }
}