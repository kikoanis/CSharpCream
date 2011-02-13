using System;
using System.Collections.Generic;
using ClassLibrary.Base;

namespace ClassLibrary
{
    public partial class Course : BaseClass
    {
        readonly Settings settings = new Settings();

        #region Public Methods

        public override IList<RuleViolation> GetRuleViolations()
        {
            var validationIssues = new List<RuleViolation>();

            if (string.IsNullOrEmpty(CourseName))
            {
                validationIssues.Add(
                    new RuleViolation("CourseName",
                                      CourseName,
                                      "Course Code should not be null or empty!"));
            }
            else
            {
                if (CourseName.Length > 50)
                {
                    validationIssues.Add(
                        new RuleViolation("CourseName",
                                          CourseName,
                                          "Course Code should not exceed 50!"));
                }
            }
            if (String.IsNullOrEmpty(DaysOfWeek))
            {
                validationIssues.Add(
                    new RuleViolation("DaysOfWeek",
                                      DaysOfWeek,
                                      "At least one day should be selected!"));
            }
            if (StartHour < 0 || StartHour > 23)
            {
                validationIssues.Add(
                    new RuleViolation("StartHour",
                                      StartHour,
                                      "Start Hour should be between 0 and 23!"));
            }
            if (EndHour < 0 || EndHour > 23)
            {
                validationIssues.Add(
                    new RuleViolation("EndHour",
                                      EndHour,
                                      "End Hour should be between 0 and 23!"));
            }
            if (StartMinute < 0 || StartMinute > 59)
            {
                validationIssues.Add(
                    new RuleViolation("StartMinute",
                                      StartMinute,
                                      "Start Minute should be between 0 and 59!"));
            }
            if (EndMinute < 0 || EndMinute > 59)
            {
                validationIssues.Add(
                    new RuleViolation("EndMinute",
                                      EndMinute,
                                      "End Minute should be between 0 and 59!"));
            }
            if (EndHour < StartHour)
            {
                validationIssues.Add(
                    new RuleViolation("EndHour",
                                      EndHour,
                                      "End Hour should be greater than or equal Start Hour!"));
            }
            if (EndHour == StartHour && StartMinute >= EndMinute)
            {
                validationIssues.Add(
                    new RuleViolation("EndMinute",
                                      EndMinute,
                                      "End Minute should be greater than Start Minute when they are in the same hour!"));
            }

            if (!string.IsNullOrEmpty(DaysOfWeek))
            {

                int timeDifferenceInMins = CalculateTotalTimePerSession(StartHour, startMinute, EndHour, EndMinute);

                int totalTime = timeDifferenceInMins == 0
                                    ? 0
                                    : (timeDifferenceInMins + settings.MaxBreakMinutesPerSession) * DaysOfWeek.Length;

                if (totalTime < settings.MaxNumberOfHoursPerCourse * 60)  
                {
                    validationIssues.Add(
                        new RuleViolation("EndHour",
                                          EndHour,
                                          "Total time for the course over the week must be at least " + settings.MaxNumberOfHoursPerCourse +
                                          " hours including the maximum break minutes (" + (settings.MaxBreakMinutesPerSession - 5) + "-" + settings.MaxBreakMinutesPerSession + " mins) for each session"));
                }

                if (totalTime > settings.MaxNumberOfHoursPerCourse * 60 + (5 * DaysOfWeek.Length))
                {
                    validationIssues.Add(
                        new RuleViolation("EndHour",
                                          EndHour,
                                          "Total time for the course over the week must be at most " + settings.MaxNumberOfHoursPerCourse +
                                          " hours including the maximum break minutes (" + (settings.MaxBreakMinutesPerSession - 5) + "-" + settings.MaxBreakMinutesPerSession + " mins) for each session"));
                }
            }
            return validationIssues;
        }

        private static int CalculateTotalTimePerSession(int startHour, int startMinute, int endHour, int endMinute)
        {
            return (endHour*60 + endMinute) - (startHour*60 + startMinute);
        }

        #endregion

    }
}
