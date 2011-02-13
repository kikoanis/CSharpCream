using System;
using System.Runtime.InteropServices;
using System.Text;
using DataAccessLayer;
using MbUnit.Framework;

namespace DataAccessLayerTest
{
	[TestFixture]
	public class TestSolutionProvider
	{

		#region Public Methods

		[Test]
		public void ThisShouldreturnAllSolutions()
		{
			var myStringBuilder = new StringBuilder(224);
			myStringBuilder.AppendFormat(@"To update a person record, a user must be a member of the customer {0}", Environment.NewLine);
			myStringBuilder.AppendFormat(@"service group or the manager group. After the person has been updated, a {0}", Environment.NewLine);
			myStringBuilder.AppendFormat(@"letter needs to be generated to notify the customer of the information {0}", Environment.NewLine);
			myStringBuilder.AppendFormat(@"change.");


			
			var sol = new SolutionProvider();
			sol.GetAllProfessorsAssignedCourses(1);
			////sol.DeleteAllSolutions();
			//var s = sol.SolutionsBySolutionNo(1, "Professor");
			//Assert.AreEqual(s.Count, 0);
		}

		[DllImport("coredll.dll", SetLastError = true)]
		protected static extern int waveOutGetVolume(IntPtr device, ref uint volume);

		[DllImport("coredll.dll", SetLastError = true)]
		protected static extern int waveOutSetVolume(IntPtr device, uint volume);

		#endregion

	}
}
