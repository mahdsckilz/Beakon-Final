using System;
using Android.App;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using System.Threading.Tasks;
using System.Collections.Generic;
using BeakonMvvm.Core.Interfaces;

namespace BeakonMvvm.Droid.Services
{
    public class DialogServiceP : IDialogServiceP
    {
        Dialog dialog = null;
        List<String> a = new List<string>();


        public async Task<List<string>> Show(string message, string title)
        {
            return await Show(message, title, "OK", "Cancel");
        }

        public async Task<List<string>> Show(string message, string title, string confirmButton, string cancelButton)
        {
          
            a.Add("nothing");
            bool buttonPressed = false;
            Application.SynchronizationContext.Post(_ =>
            {
                var mvxTopActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
                AlertDialog.Builder alertDialog = new AlertDialog.Builder(mvxTopActivity.Activity);
                alertDialog.SetTitle(title);
                alertDialog.SetMessage(message);
                alertDialog.SetNegativeButton(cancelButton, (s, args) =>
                {
                    a.Clear();
                    a.Add("false");
                });
                alertDialog.SetPositiveButton(confirmButton, (s, args) =>
                {
                    a.Clear();
                    a.Add("true");
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
