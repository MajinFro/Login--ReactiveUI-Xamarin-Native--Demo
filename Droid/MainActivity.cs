using Android.App;
using Android.OS;
using Android.Content;
using Login.Droid.Activities;
using System.Threading;
using Splat;
using Login.Services;

namespace Login.Droid
{
    [Activity(Label = "loginDemo", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Locator.CurrentMutable.Register(() => new AccountService(), typeof(IAccountService));
            Thread.Sleep(2000);
            StartActivity(typeof(SignInActivity));
        }
    }
}
