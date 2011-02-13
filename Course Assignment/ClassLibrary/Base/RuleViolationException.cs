using System;
using System.Collections.Generic;
using ClassLibrary.Base;

namespace ClassLibrary.Rules
{
    public class RuleViolationException : Exception
    {
        public IList<RuleViolation> RuleValidationIssues { get; private set; }

        public RuleViolationException(IList<RuleViolation> rules)
        {
            RuleValidationIssues = rules;
        }
        public RuleViolationException(string message, IList<RuleViolation> rules)
            : base(message)
        {
            RuleValidationIssues = rules;
        }
    }
}