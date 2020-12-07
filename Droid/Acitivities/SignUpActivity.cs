
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Login.ViewModels;
using ReactiveUI;

namespace Login.Droid.Activities
{
    [Activity(Label = "SignUpActivity", Theme = "@style/Theme.NoActionBar")]
    public class SignUpActivity : ReactiveActivity<SignUpViewModel>
    {

        //public TextInputEditText Username {get; set; }
        //public TextInputEditText Password { get; set; }
        //public TextInputEditText FirstName { get; set; }
        //public TextInputEditText LastName { get; set; }
        //public TextInputEditText Phone { get; set; }
        //public TextInputEditText ServiceStartDate { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SignUp);

            //ViewModel = new SignUpViewModel();

            //this.WireUpControls();
            //this.WhenActivated(disposables =>
            //{
            //    this.Bind(ViewModel, x => x.Username, x => x.Username.Text).DisposeWith(disposables);
            //});
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
