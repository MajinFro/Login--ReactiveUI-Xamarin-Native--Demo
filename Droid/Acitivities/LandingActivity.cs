
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Login.Droid.Activities
{
    [Activity(Label = "LandingActivity", NoHistory = true, Theme = "@style/Theme.NoActionBar")]
    public class LandingActivity : Activity
    { 
        public Button SignInButton { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            JoanZapata.XamarinIconify.Iconify.with(new JoanZapata.XamarinIconify.Fonts.FontAwesomeModule());
            SetContentView(Resource.Layout.Landing);

            SignInButton = FindViewById<Button>(Resource.Id.signInButton);
            SignInButton.Click += SignInButton_Click;
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SignInActivity)).SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
