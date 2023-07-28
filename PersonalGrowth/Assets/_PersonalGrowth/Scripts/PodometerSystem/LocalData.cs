using System;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    [Serializable]
    public class LocalData
    {
        public DateTime lastUseDay = DateTime.MinValue;
        public int nLastTodaySteps = 0;
        public int stepCoinsCount = 0;
    }
}