using System;
using System.Text.RegularExpressions;

namespace Login.Validation.Rules
{
    public class RegexRule : BaseValidationRule<string>
    {
        public string RegExPattern { get; set; }
        public bool Matches { get; set; }

        public RegexRule(string message, string regexPattern, bool matches = true) : base(message)
        {
            RegExPattern = regexPattern;
            Matches = matches;
        }

        public override bool Check(string value)
        {
            if(string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(RegExPattern))
            {
                return false;
            }
            bool matched = Regex.IsMatch(value, RegExPattern);
            return Matches ? matched : !matched;
        }
    }
}
