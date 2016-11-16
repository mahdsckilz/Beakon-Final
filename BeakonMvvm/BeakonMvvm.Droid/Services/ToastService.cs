using BeakonMvvm.Core.Interfaces;
using Android.App;
using Android.Widget;

namespace BeakonMvvm.Droid.Services
{
    public class ToastService : IToast
    {
        public void Show(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }

}