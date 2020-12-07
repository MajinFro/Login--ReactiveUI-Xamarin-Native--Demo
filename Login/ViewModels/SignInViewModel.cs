using System.Reactive;
using Login.Models;
using Login.Services;
using ReactiveUI;
using Splat;
using ReactiveUI.Fody.Helpers;

namespace Login.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;

        [Reactive]
        public string Username { get; set; }

        [Reactive]
        public string Password { get; set; }

        [Reactive]
        public bool SignInEnabled { get; set; }

        public ReactiveCommand<Unit, AccountStatus> SignInCommand { get; }

        public SignInViewModel(IAccountService accountService = null)
        {
            _accountService = accountService ?? Locator.Current.GetService<IAccountService>();
            //SignInCommand = ReactiveCommand.Create(SignIn);
        }

        //private AccountStatus SignIn()
        //{

        //}
    }
}
