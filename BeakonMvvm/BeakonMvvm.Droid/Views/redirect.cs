using Android.App;
using Android.OS;
using BeakonMvvm.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace BeakonMvvm.Droid.Views
{
    [MvxViewFor(typeof(RedirectViewModel))]
    [Activity(Label = "Answers Page")]
    public class redirect : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
           // SetContentView(Resource.Layout.Notification);
        }
    }
}