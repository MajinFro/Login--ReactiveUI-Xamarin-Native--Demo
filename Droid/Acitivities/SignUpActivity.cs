
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
                this.Bind(ViewModel, x => x.FirstName, x => x.FirstName.Text);
                this.Bind(ViewModel, x => x.LastName, x => x.LastName.Text);
                this.Bind(ViewModel, x => x.Username, x => x.Username.Text);
                this.Bind(ViewModel, x => x.Password, x => x.Password.Text);
                this.Bind(ViewModel, x => x.Phone, x => x.PhoneNumber.Text);
                this.Bind(ViewModel, x => x.ServiceStartDate, x => x.ServiceStartDate.Text);

                this.BindCommand(ViewModel, x => x.SignUp, x => x.SignUpButton);

                this.BindValidation(ViewModel, x => x.FirstName, FirstNameField);
                this.BindValidation(ViewModel, x => x.LastName, LastNameField);
                this.BindValidation(ViewModel, x => x.Username, UsernameField);
                this.BindValidation(ViewModel, x => x.Password, PasswordField);
                this.BindValidation(ViewModel, x => x.Phone, PhoneNumberField);
                this.BindValidation(ViewModel, x => x.ServiceStartDate, ServiceStartDateField);
            });
        }

        public void ShowDialog(AccountStatus accountStatus)
        {
            if(accountStatus.Status == Status.success)
            {
                StartActivity(typeof(LandingActivity));
            }
            else
            {
                Snackbar.Make(SignUpButton, accountStatus.Message, 5000).Show();
                HideKeyboard();
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
    }
}
