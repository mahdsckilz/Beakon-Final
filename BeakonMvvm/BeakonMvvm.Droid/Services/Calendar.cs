using System;
using System.Collections.Generic;

using BeakonMvvm.Core.Interfaces;
using Android.App;
using Android.Provider;
using Android.Database;

namespace BeakonMvvm.Droid.Services
{

    public class Calendar : ICalendar
    {
        List<string> eventlist = new List<string>();
        string returnevents;
        public string returnEvents()
        {

            // Get events
            var calendarsUri = CalendarContract.Calendars.ContentUri;

            string[] calendarsProjection = {
               CalendarContract.Calendars.InterfaceConsts.Id,
               CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
               CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            var cursor = Application.Context.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);

                cursor.MoveToFirst();
                int calendarCount = cursor.Count;

            if (calendarCount==0)
            {
   
                    return "No Events";
                }
            else
            {


                int calId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));


                var eventsUri = CalendarContract.Events.ContentUri;

                string[] eventsProjection = {
                CalendarContract.Events.InterfaceConsts.Id,
                CalendarContract.Events.InterfaceConsts.Title,
                CalendarContract.Events.InterfaceConsts.Dtstart
             };

                var events = Application.Context.ContentResolver.Query(eventsUri, eventsProjection,
                 String.Format("calendar_id={0}", calId), null, "dtstart ASC");




                // var events = eventList(calId);
                int testi = events.Count;

                events.MoveToFirst();
                long eventTimeLong = events.GetLong(events.GetColumnIndex(eventsProjection[2]));
                DateTime eventTimeDate = new DateTime(1970, 1, 1, 0, 0, 0,
                    DateTimeKind.Utc).AddMilliseconds(eventTimeLong).ToLocalTime();

                DateTime now = DateTime.Now.ToLocalTime();

                while (true)
                {
                    eventTimeLong = events.GetLong(2);
                    eventTimeDate = new DateTime(1970, 1, 1, 0, 0, 0,
                       DateTimeKind.Utc).AddMilliseconds(eventTimeLong).ToLocalTime();

                    if (eventTimeDate.DayOfYear.Equals(now.DayOfYear))

                    {

                        eventTimeDate.ToShortDateString();


                        eventlist.Add(events.GetString(events.GetColumnIndex(eventsProjection[0])) + " " + events.GetString(events.GetColumnIndex(eventsProjection[1])) +
                            " " + eventTimeDate.ToShortTimeString());

                        returnevents = returnevents + events.GetString(events.GetColumnIndex(eventsProjection[1])) +
                            " " + eventTimeDate.ToShortTimeString() + "\n";

                        if (events.IsLast == true)
                        {
                            break;
                        }
                        else
                        {
                            events.MoveToNext();
                        }

                    }
                    else
                    {
                        if (events.IsLast == true)
                        {
                            break;
                        }
                        else
                        {
                            events.MoveToNext();
                        }
                    }
                }

                return returnevents;

            }




        }

        public ICursor eventList(int _calId)
        {
            var eventsUri = CalendarContract.Events.ContentUri;

            string[] eventsProjection = {
                CalendarContract.Events.InterfaceConsts.Id,
                CalendarContract.Events.InterfaceConsts.Title,
                CalendarContract.Events.InterfaceConsts.Dtstart
             };

            var cursor = Application.Context.ContentResolver.Query(eventsUri, eventsProjection,
             String.Format("calendar_id={0}", _calId), null, "dtstart ASC");

            return cursor;
        } 

    }
}