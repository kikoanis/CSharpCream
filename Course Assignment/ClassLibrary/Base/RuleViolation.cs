namespace ClassLibrary.Base
{
    public class RuleViolation
    {
        public string PropertyName { get; private set; }
        public object PropertyValue { get; private set; }
        public string ErrorMessage { get; private set; }

        public RuleViolation(string propertyName, object propertyValue, string errorMessage)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            ErrorMessage = errorMessage;
        }
    }
}
