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
            // If save does not exist, this is the first session
            if (!LocalDataSaver<LocalData>.SaveExists)
                MobileNotificationManager.Instance.MakeNotification("Good morning!", "A little walk for waking up?", DateTime.Today.AddDays(1).AddHours(reminderHour), TimeSpan.FromDays(1));
        }
    }
}