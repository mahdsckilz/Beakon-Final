using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using BeakonMvvm.Core.Interfaces;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.ViewModels
{
    public class NotificationViewModel : MvxViewModel
    {
        private IReqDB dbs;
        public ICommand SelectMessage { get; private set; }
        private readonly IDialogService dialog;
        private ICalendar calendar;
        private Perso sell = MyGlobals.SelPer;
      

        private ObservableCollection<Req> messages;
        public ObservableCollection<Req> Message
        {
            get { return messages; }
            set
            {
                    SetProperty(ref messages, value);
                RaisePropertyChanged("Message");

            }
        }

        public NotificationViewModel(IReqDB dbss, IDialogService dialog, ICalendar calendar, IToast toast, INetwork net)
        {
            dbs = dbss;       
            this.dialog = dialog;
            this.calendar = calendar;

            ReloadCommand.Execute(null);
           SelectMessage = new MvxCommand<Req>(async selectedItem =>
            {
                string ifloc = "Not Needed";
                string ifcal = "Not Needed";

                if (selectedItem.ReqLoc == true) { ifloc = "Needed"; }

                if (selectedItem.ReqCal == true) { ifcal = "Needed"; }

                string mes = selectedItem.ReqFrom + "\n" + "Calendar: " + ifcal + "\n" + "Location: " + ifloc + "\n" + "Other Info:" + selectedItem.ReqExtra; 

                List<string> Answer = await dialog.Show(mes, "Status Request",  "Send", "Dismiss");
                if (Answer[0] == "true")
                {

                    Message.Remove(selectedItem);
                    await DeleteReq(selectedItem.Id);
                    toast.Show("Status Response Sent");
                    string calend = ""; // Calander Events for Today
                    string wifi = ""; // Wifi Access point of person

                    // Don't send location or Calendar
                    if ((MyGlobals.SelPer.PLocCheck == false || ifloc == "Not Needed") && (MyGlobals.SelPer.PCalCheck == false || ifcal == "Not Needed"))
                    {
                         calend = ""; 
                         wifi = ""; 
                    }
                    // Don't send Calendar
                    else if (MyGlobals.SelPer.PCalCheck == false || ifcal == "Not Needed")
                    {
                         calend = ""; 
                         wifi = net.SSID(); 
                    }
                    // Don't send Location
                    else if (MyGlobals.SelPer.PLocCheck == false || ifloc == "Not Needed")
                    {
                         calend = calendar.returnEvents(); 
                         wifi = ""; 
                    }
                    // Send both.
                    else
                    {
                         calend = calendar.returnEvents(); 
                         wifi = net.SSID(); 
                    }
                   

                    MyGlobals.answer  = new Answ
                    {
                        AnsFrom = selectedItem.ReqTo,
                        AnsTo = selectedItem.ReqFrom,
                        AnsLoc = wifi,
                        AnsCal = calend,
                        AnsExtra = Answer[1]
                    };
                    ShowViewModel<RedirectViewModel>();

                }

                else if (Answer[0]=="false")
                {
                    Message.Remove(selectedItem);
                    await DeleteReq(selectedItem.Id);
                    toast.Show("Status Request Deleted");
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


        public MvxCommand NavAns
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<AnsViewModel>());
            }
        }
        public async Task LoadReq()
        {
            Message = new ObservableCollection<Req>();

            foreach (Req request in await dbs.GetReq(MyGlobals.SelPer.pFirstname))
            {

                Message.Add(request);

            }
        }

        public async Task DeleteReq(object id)
        {
            await dbs.DeleteReq(id);
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