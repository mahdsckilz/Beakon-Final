using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using BeakonMvvm.Core.Interfaces;
using BeakonMvvm.Core.Database;
using System.Windows.Input;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.ViewModels
{
    public static class MyGlobals
    {
        public static Perso perr { get; set; } // Holds the Selected Person
        public static Perso SelPer { get; set; } // Holds the Main person
        public static Answ answer { get; set; } // Holds the answer for Req
    }

    public class RequestsViewModel : MvxViewModel
    { 
       private ObservableCollection<Perso> _persons;
       private IAPerson dbs;
       List<Perso> dbPersons = new List<Perso>();

       public ICommand SelectPer { get; private set; }

        // List that holds the persons
        public ObservableCollection<Perso> Persons
        {
            get { return _persons; }
            set
            {
                SetProperty(ref _persons, value);
                RaisePropertyChanged("Persons");
            }
        }

        // Main View Model Function
        public RequestsViewModel(IAPerson dbs, IToast toast)
        {
            this.dbs = dbs;
            ReloadCommand.Execute(null);
            SelectPer = new MvxCommand<Perso>( selectedPer =>
            {
                MyGlobals.perr = null;
                MyGlobals.perr = selectedPer;
                ShowViewModel<AnswerViewModel>();
            });
        }

        // Navigation 
        public MvxCommand NavNotCmd
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<NotificationViewModel>());
            }
        }

        public MvxCommand NavSetCmd
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<SettingsViewModel>());
            }
        }


        public async Task LoadReq()
        {
            Persons = new ObservableCollection<Perso>();
            foreach (Perso person in await dbs.GetPersons())
            {
                Persons.Add(person);
            }
        }
        // experimental stuff

        private bool _isRefreshing;

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {

                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () => {
                    IsRefreshing = true;
                    await LoadReq();
                    IsRefreshing = false;
                });
            }
        }

        public virtual async Task ReloadData()
        {
            // By default return a completed Task
            await Task.Delay(5000);
        }

    }
}