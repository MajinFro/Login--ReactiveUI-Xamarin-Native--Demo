using System;
using System.Text.RegularExpressions;
using Login.Models;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;

namespace Login.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        [Reactive]
        public string FirstName { get; set; }

        [Reactive]
        public string LastName { get; set; }

        [Reactive]
        public string Username { get; set; }

        [Reactive]
        public string Password { get; set; }

        [Reactive]
        public string Phone { get; set; }

        [Reactive]
        public string ServiceStartDate { get; set; }

        public SignUpViewModel()
        {
            SetupValidation();
        }

        private Account GetAccountModel()
        {
            return new Account()
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Username = this.Username,
                Password = this.Password,
                Phone = this.Phone,
                ServiceStartDate = this.ServiceStartDate

            };
        }

        private void SetupValidation()
        {
            SetupUsernameValidation();
            SetupPasswordValidation();
            SetupFirstNameValidation();
            SetupLastNameValidation();
            SetupPhoneValidation();
            SetupServiceStartDateValidation();

        }

        private void SetupUsernameValidation()
        {
            this.ValidationRule(
               vm => vm.Username,
               name => !string.IsNullOrWhiteSpace(name),
               "Username is required.");
        }

        private void SetupPasswordValidation()
        {
            this.ValidationRule(
                vm => vm.Password,
                password => !string.IsNullOrWhiteSpace(password),
                "Password is required.");

            this.ValidationRule(
                vm => vm.Password,
                password => password?.Length >= 8 && password?.Length <= 15,
                password => $"Password must be between 8 and 15 characters, current length: {password.Length}");

            this.ValidationRule(
                vm => vm.Password,
                password => Regex.IsMatch(password, @"^[A-Z]+$"),
                password => $"Password must contain a capital letter.");

            this.ValidationRule(
                vm => vm.Password,
                password => Regex.IsMatch(password, @"^[a-z]+$"),
                password => $"Password must contain a lower case letter.");

            this.ValidationRule(
                vm => vm.Password,
                password => !Regex.IsMatch(password, @"(\w+)\1"),
                password => $"Password cannot have a repeating sequence of characters.");
        }

        private void SetupFirstNameValidation()
        {
            this.ValidationRule(
               vm => vm.FirstName,
               name => !string.IsNullOrWhiteSpace(name),
               "First Name is required.");

            this.ValidationRule(
                vm => vm.FirstName,
                name => Regex.IsMatch(name, @"^[^!@#$%^&]+$"),
                "First Name can not contain !@#$%^&");
        }

        private void SetupLastNameValidation()
        {
            this.ValidationRule(
               vm => vm.LastName,
               name => !string.IsNullOrWhiteSpace(name),
               "Last Name is required.");

            this.ValidationRule(
                vm => vm.LastName,
                name => Regex.IsMatch(name, @"^[^!@#$%^&]+$"),
                "Last Name can not contain !@#$%^&");
        }

        private void SetupPhoneValidation()
        {
            this.ValidationRule(
               vm => vm.Phone,
               phone => !string.IsNullOrWhiteSpace(phone),
               "Phone is required.");

            this.ValidationRule(
               vm => vm.Phone,
               phone => Regex.IsMatch(phone, @"^\(\d{3}\)-\d{3}-\d{4}$"),
               "Phone Number must be in the format of (###)-###-####");
        }

        private void SetupServiceStartDateValidation()
        {
            this.ValidationRule(
               vm => vm.ServiceStartDate,
               date => !string.IsNullOrWhiteSpace(date),
               "Service Start Date is required.");

            this.ValidationRule(
               vm => vm.ServiceStartDate,
               date => Regex.IsMatch(date, @"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$"),
               "Service Start Date must be in the format of MM/DD/YYYY.");

            this.ValidationRule(
               vm => vm.ServiceStartDate,
               date => DateTime.Parse(date).Date >= DateTime.Now.Date,
               "Service Start Date must not be in the past");

            this.ValidationRule(
               vm => vm.ServiceStartDate,
               date => DateTime.Parse(date).Date <= DateTime.Now.AddDays(30).Date,
               "It is too early to create an account");
        }
    }
}
