using System;
namespace Login.Validation.Rules
{
    public class LengthRule : BaseValidationRule<string>
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public LengthRule(string message, int minLength = -1, int maxLength = -1) : base(message)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public override bool Check(string value)
        {
            string valueToCheck = value ?? string.Empty;
            bool retVal = MinLength > -1 ? valueToCheck.Length >= MinLength : true;
            retVal = retVal && (MaxLength > -1 ? valueToCheck.Length <= MaxLength : true);
            return retVal;
        }
    }
}
