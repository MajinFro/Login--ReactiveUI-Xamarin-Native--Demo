using System.Reactive;
using Login.Models;
using Login.Services;
using ReactiveUI;
using Splat;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;

namespace Login.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService accountService;
        private readonly IUserDialogs userDialogs;

        [Reactive]
        public string Username { get; set; } = "";

        [Reactive]
        public string Password { get; set; } = "";

        public ReactiveCommand<Unit, Unit> SignIn { get; }

        public SignInViewModel(IAccountService accountService = null, IUserDialogs userDialogs = null)
        {
            this.accountService = accountService ?? Locator.Current.GetService<IAccountService>();
            this.userDialogs = userDialogs ?? Locator.Current.GetService<IUserDialogs>();
            SignIn = ReactiveCommand.Create(SignInImpl, this.IsValid());
            SetupValidation();
        }

        private void SignInImpl()
        {
            AccountStatus status = this.accountService.SignIn(Username, Password);
            userDialogs.ShowDialog(status);
        }

        private void SetupValidation()
        {
            this.ValidationRule(
                vm => vm.Username,
                name => !string.IsNullOrWhiteSpace(name),
                "Username is required.");

            this.ValidationRule(
                vm => vm.Password,
                password => !string.IsNullOrWhiteSpace(password),
                "Password is required.");
        }
    }
}
