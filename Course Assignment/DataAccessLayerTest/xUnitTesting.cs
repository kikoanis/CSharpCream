// --------------------------------------------------------------------------------------------------------------------
// <copyright file="xUnitTesting.cs" company="PC">
//   Data Access Layer Test
// </copyright>
// <summary>
//   Defines the XUnitTesting type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataAccessLayerTest
{
	using ClassLibrary;
	using DataAccessLayer;
	using Xunit;

	/// <summary>
	/// Class XUnit Testing
	/// </summary>
	public class XUnitTesting
    {
    	/// <summary>
    	/// CourseProvider provider
    	/// </summary>
    	private  CourseProvider provider;

    	/// <summary>
    	/// Testing if N should not be null
    	/// </summary>
    	[Fact]
        public void NShoudlReturnNull()
    	{
    		const int A = 100 + 100;
    		Assert.Equal(200, A);
    	}

    	/// <summary>
    	/// Testing for assigning course
    	/// </summary>
    	[Fact]
        public void CanAssCourse()
    	{
            provider = new CourseProvider();
    		// DaysOfWeek = "MWF"
    		var course = new Course
    		             	{
    		             		CourseName = "CSTest",
    		             		Monday = true,
    		             		Friday = true,
    		             		StartHour = 13,
    		             		StartMinute = 0,
    		             		EndHour = 14,
    		             		EndMinute = 15,
    		             		AssignedProfessor = null
    		             	};
    	    
    		var newIdentity = provider.AddCourse(course);
    		var testCourse = provider.GetCourseByID(newIdentity);

    		Assert.Equal(testCourse.EndHour, 14);
    	}
    }
}
