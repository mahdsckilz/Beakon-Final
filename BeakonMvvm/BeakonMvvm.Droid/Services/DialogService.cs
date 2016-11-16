using System;
using Android.App;
using BeakonMvvm.Core.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using Android.Views;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;

namespace BeakonMvvm.Droid.Services
{
    public class DialogService : IDialogService
    {

        Dialog dialog = null;
        List<String> a = new List<string>();
      
        public async Task<List<string>> Show(string message, string title)
        {
            return await Show(message, title, "OK", "Cancel");
        }

        public async Task<List<string>> Show(string message, string title, string confirmButton, string cancelButton)
        {
            bool buttonPressed = false;

            a.Add("Nothing");
            a.Add("");
            Application.SynchronizationContext.Post(_ =>
            {
                var mvxTopActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(mvxTopActivity.Activity);
                //Inflate layout
                var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                View view = inflater.Inflate(Resource.Layout.CustomDialog, null);

                alertDialog.SetTitle(title);
                alertDialog.SetMessage(message);
                alertDialog.SetView(view);
                alertDialog.SetNegativeButton(cancelButton, (s, args) => 
                {
                    a.Clear();
                    a.Add("false");
                    a.Add("");
                });
                alertDialog.SetPositiveButton(confirmButton, (s, args) =>
                {
                    //Text From View
                    EditText edit = view.FindViewById<EditText>(Resource.Id.editTextDialogUserInput);
                    string text = edit.Text;
                    a.Clear();
                    a.Add("true");
                    a.Add(text.ToString());
                });

                dialog = alertDialog.Create();
                dialog.DismissEvent += (object sender, EventArgs e) =>
                {
                    buttonPressed = true;
                    dialog.Dismiss();
                };
                dialog.Show();
            }, null);
            while (!buttonPressed)
            {
                await Task.Delay(100);
            }
            return a;
        }
    }
}
