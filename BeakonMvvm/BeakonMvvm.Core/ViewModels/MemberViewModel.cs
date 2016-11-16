using MvvmCross.Core.ViewModels;
using System.Windows.Input;

namespace BeakonMvvm.Core.ViewModels
{
    public class MemberViewModel : MvxViewModel
    {

        private string _ExtraInfo = "Hello MvvmCross";
        public string ExtraInfo
        {
            get { return _ExtraInfo; }
            set
            {
                if (value != null)

                    SetProperty(ref _ExtraInfo, value);
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            { 

                    SetProperty(ref _isChecked, value);
            }
        }

        public ICommand SendButton { get; private set; }

        public MemberViewModel()
        {


            SendButton = new MvxCommand(() =>
            {
                _isChecked = true;
                _ExtraInfo = "fuck off";
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

    }

}