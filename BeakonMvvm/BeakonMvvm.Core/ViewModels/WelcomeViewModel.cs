using MvvmCross.Core.ViewModels;
using BeakonMvvm.Core.Interfaces;
using BeakonMvvm.Core.Database;
using System.Windows.Input;

namespace BeakonMvvm.Core.ViewModels
{

    public class WelcomeViewModel : MvxViewModel
    {
        private IAPerson adb;
        private Perso Gur;
        private Perso Hari;

        public ICommand GurButton { get; private set; }
        public ICommand HarriButton { get; private set; }
        public WelcomeViewModel(IToast toast, IAPerson per)
        {
            adb = per;

            Gur = new Perso
            {
                pFirstname = "Gurpreet",
                pLastname = "Dhaliwal",
                PEmail = "hathur.gg@gmail.com",
                photo = "@drawable/gur",
                PCalCheck = true,
                PLocation = "Available",
                PLocCheck = true
            };

            Hari = new Perso
            {
                pFirstname = "Harri",
                pLastname = "Tuononen",
                PEmail = "harri@gmail.com",
                photo = "@drawable/harri",
                PCalCheck = true,
                PLocation = "Having Food",
                PLocCheck = true
            };

            //Perso Ian = new Perso
            //{
            //    pFirstname = "Ian",
            //    pLastname = "Maskell",
            //    PEmail = "Maskell@gmail.com",
            //    photo = "@drawable/iam",
            //    PCalCheck = true,
            //    PLocation = "Meeting in QUT",
            //    PLocCheck = true
            //};

            //getCount(Gur);
            //getCount(Hari);
            //getCount(Ian);

            GurButton = new MvxCommand(() =>
            {
                MyGlobals.SelPer = Gur;
                toast.Show("Logged in as " + MyGlobals.SelPer.pFirstname + "");
                ShowViewModel<SettingsViewModel>();

            });
            HarriButton = new MvxCommand(() =>
            {
                MyGlobals.SelPer = Hari;
                toast.Show("Logged in as " + MyGlobals.SelPer.pFirstname + "");
                ShowViewModel<SettingsViewModel>();

            });

        }
        public async void getCount(Perso person)
        {
            await adb.InsertPerson(person);
        }

    }
}

