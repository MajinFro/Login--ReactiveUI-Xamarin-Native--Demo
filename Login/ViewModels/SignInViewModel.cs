using System.Reactive;
using Login.Models;
using Login.Services;
using ReactiveUI;
using Splat;
using ReactiveUI.Fody.Helpers;
using Login.Validation;
using Login.Validation.Rules;

namespace Login.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly IAccountService accountService;
        private readonly IUserDialogs userDialogs;

        [Reactive]
        public ValidatableObject<string> Username { get; set; } = new ValidatableObject<string>();

        [Reactive]
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();

        public ReactiveCommand<Unit, Unit> SignIn { get; }

        public SignInViewModel(IAccountService accountService = null, IUserDialogs userDialogs = null)
        {
            this.accountService = accountService ?? Locator.Current.GetService<IAccountService>();
            this.userDialogs = userDialogs ?? Locator.Current.GetService<IUserDialogs>();
            SetupValidation();

            var canSignInObservable = this.WhenAnyValue(
                vm => vm.Username.Value,
                vm => vm.Password.Value,
                (username, password) => {
                    return Username.TryValidate(username) && Password.TryValidate(password);
                }
            );

            SignIn = ReactiveCommand.Create(SignInImpl, canSignInObservable);
        }

        private void SignInImpl()
        {
            AccountStatus status = this.accountService.SignIn(Username.Value, Password.Value);
            userDialogs.ShowDialog(status);
        }

        private void SetupValidation()
        {
            Username.Validations.Add(new RequiredRule("Username is required."));
            Password.Validations.Add(new RequiredRule("Password is required."));
        }
    }
}
