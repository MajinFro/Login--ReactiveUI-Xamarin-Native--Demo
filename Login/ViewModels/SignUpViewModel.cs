using System;
using ReactiveUI;
using Login.Models;

namespace Login.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { this.RaiseAndSetIfChanged(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { this.RaiseAndSetIfChanged(ref _phone, value); }
        }

        private DateTime _serviceStartDate;
        public DateTime ServiceStartDate
        {
            get { return _serviceStartDate; }
            set { this.RaiseAndSetIfChanged(ref _serviceStartDate, value); }
        }

        private bool _signUpEnabled;
        public bool SignUpEnabled
        {
            get { return _signUpEnabled; }
            set { this.RaiseAndSetIfChanged(ref _signUpEnabled, value); }
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
    }
}
