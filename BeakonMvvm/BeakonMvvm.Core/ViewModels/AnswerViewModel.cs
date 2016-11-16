using MvvmCross.Core.ViewModels;
using System.Windows.Input;
using BeakonMvvm.Core.Interfaces;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.ViewModels
{
    public class AnswerViewModel : MvxViewModel

    {
        private IReqDB dbss;
        private Req Hari;

        private string _PerInfo = "" + MyGlobals.perr.pFirstname + " " + MyGlobals.perr.pLastname + "\n\n" + MyGlobals.perr.PLocation + "";
        public string PerInfo
        {
            get { return _PerInfo; }
        }

        private string _PersonPhto = MyGlobals.perr.photo;
        public string PersonPhto
        {
            get { return _PersonPhto; }
        }

        private string _ExtraInfo;
        public string ExtraInfo
        {
            get { return _ExtraInfo; }
            set
            {
                if (value != null && value != _ExtraInfo)
                {
                    _ExtraInfo = value;
                    RaisePropertyChanged(() => ExtraInfo);
                }
            }
        }

        private bool _isCheckedCal = MyGlobals.perr.PCalCheck;
        public bool IsCheckedCal
        {
            get { return _isCheckedCal; }
            set
            {

                _isCheckedCal = value;
                RaisePropertyChanged(() => IsCheckedCal);

            }
        }

        private bool _isCheckedLoc = MyGlobals.perr.PLocCheck;
        public bool IsCheckedLoc
        {
            get { return _isCheckedLoc; }
            set
            {

                _isCheckedLoc = value;
                RaisePropertyChanged(() => IsCheckedLoc);

            }
        }

        public ICommand SendButton { get; private set; }
        public ICommand CancelButton { get; private set; }

        public AnswerViewModel(IToast toast, IReqDB reqq)
        {
            dbss = reqq;
            SendButton = new MvxCommand(() =>
                {
                    Hari = new Req
                    {
                        ReqFrom = MyGlobals.SelPer.pFirstname,
                        ReqTo = MyGlobals.perr.pFirstname,
                        ReqCal = IsCheckedCal,
                        ReqLoc = IsCheckedLoc,
                        ReqExtra = ExtraInfo
                    };
                    InsertReq(Hari);

                    toast.Show("Status Request Sent");

                    ShowViewModel<NotificationViewModel>();

                });

            CancelButton = new MvxCommand(() =>
            {
                ShowViewModel<RequestsViewModel>();

            });

        }

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

        public void InsertReq(Req an)
        {
            Task<int> aa = dbss.InsertReq(an);

        }

    }
}
