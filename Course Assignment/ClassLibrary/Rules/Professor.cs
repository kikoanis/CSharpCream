using System.Collections.Generic;
using ClassLibrary.Base;

namespace ClassLibrary
{
    public partial class Professor : BaseClass
    {
        readonly Settings settings = new Settings();
        public override IList<RuleViolation> GetRuleViolations()
        {
            var validationIssues = new List<RuleViolation>();

            if (string.IsNullOrEmpty(FirstName))
            {
                validationIssues.Add(
                    new RuleViolation("FirstName",
                                      FirstName,
                                      "First Name should not be null or empty!"));
            }
            else
            {
                if (FirstName.Length > 30)
                {
                    validationIssues.Add(
                        new RuleViolation("FirstName",
                                          FirstName,
                                          "First Name should not exceed 30!"));
                }
            }
            if (string.IsNullOrEmpty(LastName))
            {
                validationIssues.Add(
                    new RuleViolation("LastName",
                                      LastName,
                                      "Last Name should not be null or empty!"));
            }
            else
            {
                if (LastName.Length > 30)
                {
                    validationIssues.Add(
                        new RuleViolation("LastName",
                                          LastName,
                                          "Last Name should not exceed 30!"));
                }
            }

            if (NoOfCourses < 0) {
                validationIssues.Add(
                    new RuleViolation("NoOfCourses",
                                      NoOfCourses,
                                      "No of Courses should be greater than or equal to 0!"));
            }

            if (NoOfCourses > settings.MaxNumberOfCoursesPerProfessor) {
                validationIssues.Add(
                    new RuleViolation("NoOfCourses",
                                      NoOfCourses,
                                      "No of Courses should be less than or equal to " + settings.MaxNumberOfCoursesPerProfessor + "!"));
            }

            return validationIssues;
        }

    }
}
