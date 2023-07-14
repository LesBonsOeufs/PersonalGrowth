using Com.GabrielBernabeu.Common;
using System;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    public class HealthConnectAARCaller : AndroidAARCaller
    {
        private Action<int> todayStepsReceivedCallback;

        /// <summary>
        /// Do not change name or signature: used by .AAR
        /// </summary>
        /// <param name="stepsCount"></param>
        private void ReceiveTodayStepsCount(string stepsCount)
        {
            todayStepsReceivedCallback?.Invoke(int.Parse(stepsCount));
            todayStepsReceivedCallback = null;
        }

        public void GetTodayStepsCount(Action<int> callback)
        {
            todayStepsReceivedCallback = callback;
            Call("getTodayStepsCount");
        }
    }
}