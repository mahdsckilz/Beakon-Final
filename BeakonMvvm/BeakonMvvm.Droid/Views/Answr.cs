using Android.App;
using Android.OS;
using BeakonMvvm.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace BeakonMvvm.Droid.Views
{
    [MvxViewFor(typeof(AnsViewModel))]
    [Activity(Label = "Requests")]
    public class Answr : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.answ);
        }
    }
}

