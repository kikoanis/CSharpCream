using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace ClassLibrary.Validation

{
    public class ValidationException : Exception
    {

        public ValidationException(ValidationResults validationResults)
        {
            this.ValidationResults = validationResults;
        }

        public ValidationResults ValidationResults { get; set; }

    }
}
