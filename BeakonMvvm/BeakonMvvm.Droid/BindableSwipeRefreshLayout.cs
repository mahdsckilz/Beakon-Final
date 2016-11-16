using Android.Content;
using Android.Support.V4.Widget;
using System.Windows.Input;
using System;
using Android.Runtime;
using Android.Util;

namespace BeakonMvvm.Droid
{
    public class BindableSwipeRefreshLayout: SwipeRefreshLayout
    {
        protected BindableSwipeRefreshLayout(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer) { }

        public BindableSwipeRefreshLayout(Context context)
            : this (context, null) { }

        public BindableSwipeRefreshLayout(Context context, IAttributeSet attributes)
            : base (context, attributes) { }

        private ICommand _refreshCommand;
        private bool _refreshOverloaded;

        public ICommand RefreshCommand
        {
            get { return _refreshCommand; }
            set
            {
                _refreshCommand = value;
                if (_refreshCommand != null)
                    EnsureRefreshCommandOverloaded();
            }
        }

        private void EnsureRefreshCommandOverloaded()
        {
            if (_refreshOverloaded)
                return;

            _refreshOverloaded = true;
            Refresh += (sender, args) => ExecuteRefreshCommand(RefreshCommand);
        }

        protected virtual void ExecuteRefreshCommand(ICommand command)
        {
            if (command == null)
                return;

            if (!command.CanExecute(null))
                return;

            command.Execute(null);
        }
    }
}