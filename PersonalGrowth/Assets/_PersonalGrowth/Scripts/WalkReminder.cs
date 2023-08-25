using Com.GabrielBernabeu.Common;
using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using System;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth {
    public class WalkReminder : MonoBehaviour
    {
        [SerializeField, Range(0, 24)] private int reminderHour = 8;

        private void Awake()
        {
            // If there is no scheduled notification from this app with id 0, make one.
            if (MobileNotificationManager.Instance.IsScheduled(0))
                MobileNotificationManager.Instance.MakeNotification("Good morning!", "A little walk for waking up?", DateTime.Today.AddDays(1).AddHours(reminderHour), TimeSpan.FromDays(1));
        }
    }
}