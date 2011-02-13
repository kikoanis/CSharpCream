using System.Collections.Generic;
using ClassLibrary.Base;

namespace ClassLibrary.Interfaces
{
    public interface IRuleEntity
    {
        IList<RuleViolation> GetRuleViolations();
        void Validate();
    }


}
