using System;
using ReactiveUI;

namespace Login.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
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

        private bool _signInEnabled;
        public bool SignInEnabled
        {
            get { return _signInEnabled; }
            set { this.RaiseAndSetIfChanged(ref _signInEnabled, value); }
        }
    }
}
