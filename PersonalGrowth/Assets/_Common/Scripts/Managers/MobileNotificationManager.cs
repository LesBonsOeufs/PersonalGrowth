///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 22/11/2022 23:30
///-----------------------------------------------------------------

using System;
using UnityEngine;

#if UNITY_ANDROID
    using Unity.Notifications.Android;
#endif

namespace Com.GabrielBernabeu.Common 
{
    public class MobileNotificationManager : Singleton<MobileNotificationManager>
    {
#if UNITY_ANDROID

        private static int id = 0;

        private AndroidNotificationChannel defaultChannel;

#endif

        private void Start()
        {
#if UNITY_ANDROID

            AndroidNotificationCenter.NotificationReceivedCallback lReceivedNotificationHandler =
                delegate (AndroidNotificationIntentData data)
                {
                    var msg = "Notification received: " + data.Id + "\n";
                    msg += "\n Notification received: ";
                    msg += "\n .Title: " + data.Notification.Title;
                    msg += "\n .Body: " + data.Notification.Text;
                    msg += "\n .Channel: " + data.Channel;
                    Debug.Log(msg);
                };

            AndroidNotificationCenter.OnNotificationReceived += lReceivedNotificationHandler;

            var lNotificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (lNotificationIntentData != null)
            {
                Debug.Log("App was opened with a notification!");
            }

#endif
        }

        public void ResetNotifications()
        {
#if UNITY_ANDROID

            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.DeleteNotificationChannel(defaultChannel.Id);
#endif
        }

        public void MakeNotification(string title, string text, DateTime fireTime, TimeSpan repeatInterval = default)
        {
#if UNITY_ANDROID

            AndroidNotification lNotification = new AndroidNotification()
            {
                Title = title,
                Text = text,
                SmallIcon = "icon_small",
                LargeIcon = "icon_large",
                FireTime = fireTime,
                RepeatInterval = repeatInterval,
                ShouldAutoCancel = true
            };

            Debug.Log(fireTime);
            AndroidNotificationCenter.SendNotificationWithExplicitID(lNotification, defaultChannel.Id, id);

#endif
        }

        public bool IsScheduled(int id)
        {
            NotificationStatus lNotificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(id);
            return lNotificationStatus == NotificationStatus.Scheduled;
        }

        private DateTime GetNextDayOfWeekDateAtHour(DayOfWeek dayOfWeek, int hour = 8)
        {
            if (hour < 0 || hour > 24)
            {
                Debug.LogError("Hour must be between 0 and 24!");
                return new DateTime();
            }

            DateTime lToday = DateTime.Today;
            int lDaysUntilDayOfWeek = ((int)dayOfWeek - (int)lToday.DayOfWeek + 7) % 7;
            DateTime lReturnedValue = lToday.AddDays(lDaysUntilDayOfWeek);
            int lDeltaHours = hour - lReturnedValue.Hour;
            lReturnedValue = lReturnedValue.AddHours(lDeltaHours);
            lReturnedValue.AddMinutes(-lReturnedValue.Minute);
            lReturnedValue.AddSeconds(-lReturnedValue.Second);

            return lReturnedValue;
        }
    }
}