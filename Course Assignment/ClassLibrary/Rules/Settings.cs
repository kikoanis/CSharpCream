// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="U of R">
//   Copyright 2008-2009
// </copyright>
// <summary>
//   Defines the Settings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System.Collections.Generic;
    using Base;

    /// <summary>
    /// Setting class
    /// </summary>
    public partial class Settings : BaseClass
    {
        #region Enums

        /// <summary>
        /// Solving methods enumeration
        /// </summary>
        public enum SolvingMethods
        {
            /// <summary>
            /// Branch and Bound 
            /// </summary>
            BranchAndBound = 0,

            /// <summary>
            /// Iterative Branch and Bound
            /// </summary>
            IterativeBranchAndBound = 1,

            /// <summary>
            /// Tabbo search
            /// </summary>
            Taboo = 2,

            /// <summary>
            /// Simulated Annealing Search
            /// </summary>
            SimulatedAnnealing = 3,

            /// <summary>
            /// Random Walk Search
            /// </summary>
            RandomWalk = 4
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get Rule Violations method
        /// </summary>
        /// <returns>List of rule violations</returns>
        public override IList<RuleViolation> GetRuleViolations()
        {
            var validationIssues = new List<RuleViolation>();

            if (MaxBreakMinutesPerSession > 20 || MaxBreakMinutesPerSession < 0)
            {
                validationIssues.Add(new RuleViolation(
                                         "MaxBreakMinutesPerSession",
                                         MaxBreakMinutesPerSession,
                                         "Maximum break minutes per person should be between 0 and 20 minutes!"));
            }

            if (MaxNumberOfCoursesPerProfessor > 5 || MaxNumberOfCoursesPerProfessor < 0)
            {
                validationIssues.Add(
                    new RuleViolation(
                        "MaxNumberOfCoursesPerProfessor",
                        MaxNumberOfCoursesPerProfessor,
                        "Maximum number of courses per professor should be between 0 and 5 courses!"));
            }

            if (MaxNumberOfHoursPerCourse > 4 || MaxNumberOfHoursPerCourse < 1)
            {
                validationIssues.Add(
                    new RuleViolation(
                        "MaxNumberOfHoursPerCourse",
                        MaxNumberOfHoursPerCourse,
                        "Maximum number of hours per course should be between 1 and 4 hours!"));
            }

            if (NumberOfPreferencesPerProfessor > 7 || NumberOfPreferencesPerProfessor < 1)
            {
                validationIssues.Add(
                    new RuleViolation(
                        "NumberOfPreferencesPerProfessor",
                        NumberOfPreferencesPerProfessor,
                        "Number of preferences per professor should be between 1 and 7 hours!"));
            }

            if (MaxNumberOfGeneratedSolutions > 100 || MaxNumberOfGeneratedSolutions < 1)
            {
                validationIssues.Add(
                    new RuleViolation(
                        "MaxNumberOfGeneratedSolutions",
                        MaxNumberOfGeneratedSolutions,
                        "Max Number of generated solutions should be between 1 and 100 solutions!"));
            }

            if (TimeOut > 1000000 || TimeOut < 1000)
            {
                validationIssues.Add(
                    new RuleViolation(
                        "TimeOut",
                        TimeOut,
                        "Time out to generated solutions should be between 1000 and 1,000,000 milliseconds (1 sec - 1,000 sec or ~17 min)!"));
            }

            return validationIssues;
        }

        #endregion
    }
}
