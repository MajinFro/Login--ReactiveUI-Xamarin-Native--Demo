
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Login.Models;
using Login.Services;
using Login.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using Splat;

namespace Login.Droid.Activities
{
    [Activity(Label = "SignInActivity", Theme = "@style/Theme.NoActionBar")]
    public class SignInActivity : ReactiveActivity<SignInViewModel>, IUserDialogs
    {
        public ConstraintLayout ConstraintLayout { get; set; }
        public TextInputLayout UsernameField { get; set; }
        public TextInputLayout PasswordField { get; set; }
        public TextInputEditText Username { get; set; }
        public TextInputEditText Password { get; set; }
        public Button SignInButton { get; set; }
        public Button CreateAccountButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SignIn);

            UsernameField = FindViewById<TextInputLayout>(Resource.Id.UsernameField);
            PasswordField = FindViewById<TextInputLayout>(Resource.Id.PasswordField);
            Username = FindViewById<TextInputEditText>(Resource.Id.Username);
            Password = FindViewById<TextInputEditText>(Resource.Id.Password);
            SignInButton = FindViewById<Button>(Resource.Id.signInButton);
            CreateAccountButton = FindViewById<Button>(Resource.Id.createAccountButton);
            ConstraintLayout = FindViewById<ConstraintLayout>(Resource.Id.main_layout);

            ConstraintLayout.Touch += (object sender, View.TouchEventArgs e) => HideKeyboard();
            CreateAccountButton.Click += CreateAccountButton_Click;

            Locator.CurrentMutable.Register(() => this, typeof(IUserDialogs));

            ViewModel = new SignInViewModel();

            this.WhenActivated(disposibles =>
            {
                this.Bind(ViewModel, x => x.Username.Value, x => x.Username.Text);
                this.Bind(ViewModel, x => x.Password.Value, x => x.Password.Text);

                this.BindCommand(ViewModel, x => x.SignIn, x => x.SignInButton);
            });
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }

        public void ShowDialog(AccountStatus accountStatus)
        {
            string message = accountStatus.Status == Status.success ? "You have sucessfully signed in." : accountStatus.Message;
            string color = accountStatus.Status == Status.error ? "#ED4337" : "#009966";
            GetSnackbar(color, message).Show();
            HideKeyboard();
        }

        public void HideKeyboard()
        {
            var inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager?.HideSoftInputFromWindow(CurrentFocus?.WindowToken, 0);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private Snackbar GetSnackbar(string bgColor, string message)
        {
            var snackbar = Snackbar.Make(SignInButton, message, 5000);
            snackbar.View.SetBackgroundColor(Color.ParseColor(bgColor));
            snackbar.View.FindViewById<TextView>(Resource.Id.snackbar_text)?.SetTextColor(Color.White);

            return snackbar;
        }
    }
}
