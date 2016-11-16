using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using BeakonMvvm.Core.Interfaces;
using System.Windows.Input;
using BeakonMvvm.Core.Database;
using System.Threading.Tasks;

namespace BeakonMvvm.Core.ViewModels
{
    public class RedirectViewModel : MvxViewModel
    {
        IAnsDB answerDB;
       

        public RedirectViewModel(IAnsDB ansDB)
        {
            answerDB = ansDB;
            if (MyGlobals.answer != null)
            {
                Answ sel = MyGlobals.answer;
                insertAns(sel.AnsFrom, sel.AnsTo, sel.AnsCal, sel.AnsLoc, sel.AnsExtra);
                MyGlobals.answer = null;
            }
            ShowViewModel<NotificationViewModel>();

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
        public async void insertAns(string from, string to, string cal, string loc, string extra)
        {
            await answerDB.InsertAns(from,to, cal,loc,extra);
        }
    }
}

