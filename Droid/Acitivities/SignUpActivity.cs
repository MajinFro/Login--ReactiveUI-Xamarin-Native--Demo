
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Login.Models;
using Login.Services;
using Login.Validation;
using Login.ViewModels;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using Splat;

namespace Login.Droid.Activities
{
    [Activity(Label = "SignUpActivity", NoHistory = true, Theme = "@style/Theme.NoActionBar")]
    public class SignUpActivity : ReactiveActivity<SignUpViewModel>, IUserDialogs
    {
        public ConstraintLayout ConstraintLayout { get; set; }
        public TextInputLayout FirstNameField { get; set; }
        public TextInputLayout LastNameField { get; set; }
        public TextInputLayout UsernameField { get; set; }
        public TextInputLayout PasswordField { get; set; }
        public TextInputLayout PhoneNumberField { get; set; }
        public TextInputLayout ServiceStartDateField { get; set; }
        public TextInputEditText FirstName { get; set; }
        public TextInputEditText LastName { get; set; }
        public TextInputEditText Username { get; set; }
        public TextInputEditText Password { get; set; }
        public TextInputEditText PhoneNumber { get; set; }
        public TextInputEditText ServiceStartDate { get; set; }
        public Button SignUpButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SignUp);

            FirstNameField = FindViewById<TextInputLayout>(Resource.Id.FirstNameField);
            LastNameField = FindViewById<TextInputLayout>(Resource.Id.LastNameField);
            UsernameField = FindViewById<TextInputLayout>(Resource.Id.UsernameField);
            PasswordField = FindViewById<TextInputLayout>(Resource.Id.PasswordField);
            PhoneNumberField = FindViewById<TextInputLayout>(Resource.Id.PhoneNumberField);
            ServiceStartDateField = FindViewById<TextInputLayout>(Resource.Id.ServiceStartDateField);
            FirstName = FindViewById<TextInputEditText>(Resource.Id.FirstName);
            LastName = FindViewById<TextInputEditText>(Resource.Id.LastName);
            Username = FindViewById<TextInputEditText>(Resource.Id.Username);
            Password = FindViewById<TextInputEditText>(Resource.Id.Password);
            PhoneNumber = FindViewById<TextInputEditText>(Resource.Id.PhoneNumber);
            ServiceStartDate = FindViewById<TextInputEditText>(Resource.Id.ServiceStartDate);
            SignUpButton = FindViewById<Button>(Resource.Id.createAccountButton);
            ConstraintLayout = FindViewById<ConstraintLayout>(Resource.Id.main_layout);

            ConstraintLayout.Touch += (object sender, View.TouchEventArgs e) => HideKeyboard();

            Locator.CurrentMutable.Register(() => this, typeof(IUserDialogs));

            ViewModel = new SignUpViewModel();

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, x => x.FirstName.Value, x => x.FirstName.Text);
                this.Bind(ViewModel, x => x.LastName.Value, x => x.LastName.Text);
                this.Bind(ViewModel, x => x.Username.Value, x => x.Username.Text);
                this.Bind(ViewModel, x => x.Password.Value, x => x.Password.Text);
                this.Bind(ViewModel, x => x.Phone.Value, x => x.PhoneNumber.Text);
                this.Bind(ViewModel, x => x.ServiceStartDate.Value, x => x.ServiceStartDate.Text);

                this.BindCommand(ViewModel, x => x.SignUp, x => x.SignUpButton);
            });
        }

        public void ShowDialog(AccountStatus accountStatus)
        {
            if(accountStatus.Status == Status.success)
            {
                StartActivity(typeof(LandingActivity));
            }
            else if(accountStatus.Status == Status.error)
            {
                Snackbar.Make(SignUpButton, accountStatus.Message, 5000).Show();
                HideKeyboard();
            }
            else
            {
                SetErrors(FirstNameField, ViewModel.FirstName);
                SetErrors(LastNameField, ViewModel.LastName);
                SetErrors(UsernameField, ViewModel.Username);
                SetErrors(PasswordField, ViewModel.Password);
                SetErrors(PhoneNumberField, ViewModel.Phone);
                SetErrors(ServiceStartDateField, ViewModel.ServiceStartDate);
            }
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

        private void SetErrors(TextInputLayout layout, ValidatableObject<string> validatableObject)
        {
            bool hasErrors = validatableObject.Errors.Count > 0;
            layout.ErrorEnabled = hasErrors;
            layout.Error = hasErrors ? validatableObject.Errors[0] : string.Empty;
        }
    }
}
