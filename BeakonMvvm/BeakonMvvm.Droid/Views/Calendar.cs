using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BeakonMvvm.Droid.Views
{
    [Activity(Label = "Calendar")]
    public class Calendar : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var calendarsUri = CalendarContract.Calendars.ContentUri;

            string[] calendarsProjection = {
               CalendarContract.Calendars.InterfaceConsts.Id,
               CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
               CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            var cursor = ManagedQuery(calendarsUri, calendarsProjection, null, null, null);

            string[] sourceColumns = {CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                CalendarContract.Calendars.InterfaceConsts.AccountName};

            int[] targetResources = { Resource.Id.calDisplayName, Resource.Id.calAccountName };

            SimpleCursorAdapter adapter = new SimpleCursorAdapter(this, Resource.Layout.CalListItem,
                cursor, sourceColumns, targetResources);
            
        }
    }
}