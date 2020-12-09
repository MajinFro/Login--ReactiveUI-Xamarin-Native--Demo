using System;
namespace Login.Validation.Rules
{
    public class OnOrAfterDateRule : BaseValidationRule<string>
    {
        public DateTime Date { get; set; }

        public OnOrAfterDateRule(string message, DateTime date) : base(message)
        {
            Date = date;
        }

        public override bool Check(string value)
        {
            DateTime result;
            DateTime.TryParse(value, out result);
            return result == null ? false : result.Date >= Date.Date;
        }
    }
}
