using System;
namespace Login.Validation.Rules
{
    public class OnOrBeforeDateRule : BaseValidationRule<string>
    {
        public DateTime Date { get; set; }

        public OnOrBeforeDateRule(string message, DateTime date) : base(message)
        {
            Date = date;
        }

        public override bool Check(string value)
        {
            DateTime result;
            DateTime.TryParse(value, out result);
            return result == null ? false : result.Date <= Date.Date;
        }
    }
}
