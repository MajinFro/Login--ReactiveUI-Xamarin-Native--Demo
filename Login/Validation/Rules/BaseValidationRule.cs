using System;
namespace Login.Validation.Rules
{
    public class BaseValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public BaseValidationRule(string message)
        {
            ValidationMessage = message;
        }

        public virtual bool Check(T value)
        {
            throw new NotImplementedException();
        }
    }
}
