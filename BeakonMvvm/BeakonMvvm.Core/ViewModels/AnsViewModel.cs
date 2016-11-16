using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using BeakonMvvm.Core.Interfaces;
using System.Windows.Input;
using BeakonMvvm.Core.Database;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.ViewModels
{
    public class AnsViewModel : MvxViewModel
    {
        public ICommand SelectMessage { get; private set; }
        private readonly IDialogServiceP dialog;
        private IAnsDB answerDB;
        private ObservableCollection<Answ> messages;
        public ObservableCollection<Answ> Message
        {
            get { return messages; }
            set
            {
                SetProperty(ref messages, value);
            }
        }

        public AnsViewModel(IDialogServiceP dialog, IToast toast, IAnsDB ansDB)
        {
            answerDB = ansDB;
            ReloadCommand.Execute(null);
            this.dialog = dialog;

            SelectMessage = new MvxCommand<Answ>(async selectedItem =>
            {
                string mes = "from " + selectedItem.AnsFrom + "\n" + "Calendar: " + selectedItem.AnsCal + "\n" + "Location: "+ selectedItem.AnsLoc + "\nOther Info:" + selectedItem.AnsExtra;
                List<string> Answer = await dialog.Show(mes, "Status Response", "Ok", "Delete");

                if  (Answer[0] == "false")
                {
                    Message.Remove(selectedItem);
                    DeleteAns(selectedItem.Id);
                    toast.Show("Status Response Deleted");
                }
            });

        }

        public MvxCommand NavReqCmd
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<RequestsViewModel>());
            }
        }

        public MvxCommand NavSetCmd
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<SettingsViewModel>());
            }
        }

        public MvxCommand NavNoti
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<NotificationViewModel>());
            }
        }
        public void DeleteAns(object id)
        {
            Task<int> aa = answerDB.DeleteAns(id);
        }

      
        public async Task LoadReq()
        {
            Message = new ObservableCollection<Answ>();

            foreach (Answ ans in await answerDB.GetAns(MyGlobals.SelPer.pFirstname))
            {

                Message.Add(ans);

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