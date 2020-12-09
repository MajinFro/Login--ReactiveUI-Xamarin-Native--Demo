using System;
namespace Login.Validation.Rules
{
    public class RequiredRule : BaseValidationRule<string>
    {
        public RequiredRule(string message) : base(message) { }

        public override bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
