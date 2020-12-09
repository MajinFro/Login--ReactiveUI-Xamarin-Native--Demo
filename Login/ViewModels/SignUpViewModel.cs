using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using Login.Models;
using Login.Services;
using Login.Validation;
using Login.Validation.Rules;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using Splat;

namespace Login.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IAccountService accountService;
        private readonly IUserDialogs userDialogs;

        [Reactive]
        public ValidatableObject<string> FirstName { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> LastName { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> Username { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> Phone { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> ServiceStartDate { get; set; } = new ValidatableObject<string>();

        public ReactiveCommand<Unit, Unit> SignUp { get; }
       

        public SignUpViewModel(IAccountService accountservice = null, IUserDialogs userDialogs = null)
        {
            this.accountService = accountService ?? Locator.Current.GetService<IAccountService>();
            this.userDialogs = userDialogs ?? Locator.Current.GetService<IUserDialogs>();
            SignUp = ReactiveCommand.Create(SignUpImpl, this.IsValid());
            SetupValidation();

            var canSignUpObservable = this.WhenAnyValue(
                vm => vm.Username.Value,
                vm => vm.Password.Value,
                vm => vm.FirstName.Value,
                vm => vm.LastName.Value,
                vm => vm.Phone.Value,
                vm => vm.ServiceStartDate.Value,
                (username, password, firstname, lastname, phone, serviceStartDate) => {
                    return !(string.IsNullOrEmpty(username) ||
                             string.IsNullOrEmpty(password) ||
                             string.IsNullOrEmpty(firstname) ||
                             string.IsNullOrEmpty(lastname) ||
                             string.IsNullOrEmpty(phone) ||
                             string.IsNullOrEmpty(serviceStartDate));
                }
            );

            SignUp = ReactiveCommand.Create(SignUpImpl, canSignUpObservable);
        }

        private Account GetAccountModel()
        {
            return new Account()
            {
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                Username = Username.Value,
                Password = Password.Value,
                Phone = Phone.Value,
                ServiceStartDate = ServiceStartDate.Value

            };
        }

        private void SignUpImpl()
        {
            AccountStatus status =
                Validate() ?
                this.accountService.SignUp(GetAccountModel()) :
                new AccountStatus(Status.validationError, "Validation Error");
             
            userDialogs.ShowDialog(status);
        }

        private bool Validate()
        {
            return Username.Validate() &&
                   Password.Validate() &&
                   FirstName.Validate() &&
                   LastName.Validate() &&
                   Phone.Validate() &&
                   ServiceStartDate.Validate();
        }

        private void SetupValidation()
        {
            Username.Validations.Add(new RequiredRule("Username is required."));
            Password.Validations.Add(new RequiredRule("Password is required."));
            Password.Validations.Add(new LengthRule("Password must be between 8 and 15 characters.", 8, 15));
            Password.Validations.Add(new RegexRule("Password must contain a capital letter.", @"[A-Z]+"));
            Password.Validations.Add(new RegexRule("Password must contain a lower case letter.", @"[a-z]+"));
            Password.Validations.Add(new RegexRule("Password cannot have a repeating sequence of characters.", @"(\w+)\1", false));
            FirstName.Validations.Add(new RequiredRule("First Name is required."));
            FirstName.Validations.Add(new RegexRule("First Name can not contain !@#$%^&", @"^[^!@#$%^&]+$"));
            LastName.Validations.Add(new RequiredRule("Last Name is required."));
            LastName.Validations.Add(new RegexRule("Last Name can not contain !@#$%^&", @"^[^!@#$%^&]+$"));
            Phone.Validations.Add(new RequiredRule("Phone Number is required."));
            Phone.Validations.Add(new RegexRule("Phone Number must be in the format of (###)-###-####", @"^\(\d{3}\)-\d{3}-\d{4}$"));
            ServiceStartDate.Validations.Add(new RequiredRule("Service Start Date is required."));
            ServiceStartDate.Validations.Add(new RegexRule("Service Start Date must be in the format of MM/DD/YYYY.", @"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$"));
            ServiceStartDate.Validations.Add(new OnOrAfterDateRule("Service Start Date must not be in the past", DateTime.Now));
            ServiceStartDate.Validations.Add(new OnOrBeforeDateRule("It is too early to create an account", DateTime.Now.AddDays(30)));

        }
    }
}
