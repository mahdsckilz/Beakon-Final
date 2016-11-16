using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace BeakonMvvm.Droid
{
    [Activity(
        Label = "Beakon"
        , MainLauncher = true
        , Theme = "@style/Theme.Splash"
        , Icon = "@drawable/Beakon_Icon"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
