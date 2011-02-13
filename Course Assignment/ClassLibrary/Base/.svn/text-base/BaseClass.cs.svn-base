// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseClass.cs" company="U of R">
//   Copyright 2008-2009
// </copyright>
// <summary>
//   Defines the BaseClass type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ClassLibrary.Base
{
    using System.Collections.Generic;
    using Interfaces;
    using Rules;

    /// <summary>
    /// Base Class
    /// </summary>
    public class BaseClass : IRuleEntity
    {
        #region Public Methods

        /// <summary>
        /// Get Rule Violations method
        /// </summary>
        /// <returns>
        /// List of rule violations
        /// </returns>
        public virtual IList<RuleViolation> GetRuleViolations()
        {
            return null;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <exception cref="RuleViolationException">
        /// <c>RuleViolationException</c>.
        /// </exception>
        public virtual void Validate()
        {
            IList<RuleViolation> validationIssues = GetRuleViolations();
            if (!IsEmpty(validationIssues))
            {
                throw new RuleViolationException(validationIssues);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks whther a list is empy or not
        /// </summary>
        /// <param name="list">
        /// The list.of collection
        /// </param>
        /// <typeparam name="T">type of collection
        /// </typeparam>
        /// <returns>
        /// boolean value
        /// </returns>
        private static bool IsEmpty<T>(ICollection<T> list)
        {
            return list.Count == 0;
        }

        #endregion
    }
}
