using System.Collections.Generic;
using ClassLibrary.Base;

namespace ClassLibrary
{
    ///<summary>
    /// Preference Class
    ///</summary>
    public partial class Preference : BaseClass
    {
        #region Enums

        ///<summary>
        ///Enumeration for preferences types 
        ///</summary>
        public enum PreferenceTypes
        {
            Equal,
            NotEqual
        } ;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the rule violations.
        /// </summary>
        /// <returns></returns>
        public override IList<RuleViolation> GetRuleViolations()
        {
            var validationIssues = new List<RuleViolation>();

            if (Weight < 1 || Weight > 5)
            {
                validationIssues.Add(
                    new RuleViolation("Weight",
                                      Weight,
                                      "Weight should be between 1 and 5!"));
            }

            if (PreferenceType != PreferenceTypes.Equal && PreferenceType != PreferenceTypes.NotEqual)
            {
                validationIssues.Add(
                    new RuleViolation("PreferenceType",
                                      PreferenceType,
                                      "Preference Type should be equal or not equal only!"));
            }
            return validationIssues;
        }

        #endregion

    }
}
