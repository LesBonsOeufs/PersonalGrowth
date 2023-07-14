using System;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    [Serializable]
    public class LocalData
    {
        public int nTodayValidatedSteps;
        public DateTime lastValidationDate;
    }
}