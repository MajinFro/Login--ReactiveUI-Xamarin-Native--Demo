using Login.Validation.Rules;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Linq;
using System;

namespace Login.Validation
{
    public interface IValidatableObject
    {
        bool IsValid { get; set; }
    }
    public class ValidatableObject<T> : ReactiveObject, IValidatableObject
    {
        [Reactive]
        public T Value { get; set; }

        [Reactive]
        public bool IsValid { get; set; }

        [Reactive]
        public string ErrorMessage { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public List<IValidationRule<T>> Validations { get; set; } = new List<IValidationRule<T>>();

        public ValidatableObject()
        {
            this.WhenAny(obj => obj.Value,
                value => value).Subscribe((value) =>
                {
                    Validate();
                });
        }

        public bool TryValidate()
        {
            return TryValidate(Value);
        }

        public bool TryValidate(T val)
        {
            Errors.Clear();
            Errors = new List<string>(Validations.Where(v => !v.Check(val)).Select(v => v.ValidationMessage));
            return !Errors.Any();
        }

        public bool Validate()
        {
            IsValid = TryValidate();
            if (Errors.Any())
            {
                ErrorMessage = string.Join(Environment.NewLine, Errors);
            }
            else
            {
                ErrorMessage = string.Empty;
            }
            return IsValid;
        }
    }
}
