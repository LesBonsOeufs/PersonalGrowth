using System;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    public delegate void PodometerEventHandler(Podometer sender);
    public class Podometer : Singleton<Podometer>
    {
        [SerializeField] private HealthConnectAARCaller healthConnect = default;

        public int StepsCountSinceLast { get; private set; }
        public int TodayStepsCount { get; private set; }

        public event PodometerEventHandler OnStepsUpdate;

        protected override void Awake()
        {
            base.Awake();
            LocalDataSaver<LocalData>.InitPath();
        }

        private void Start()
        {
            healthConnect.GetTodayStepsCount(OnStepsCountReceived);
        }

        private void OnStepsCountReceived(int nSteps)
        {
            LocalData lLocalData = LocalDataSaver<LocalData>.CurrentData;

            if (lLocalData.lastUseDay < DateTime.Today)
            {
                lLocalData.lastUseDay = DateTime.Today;
                lLocalData.nLastTodaySteps = 0;
            }

            StepsCountSinceLast = nSteps - lLocalData.nLastTodaySteps;
            TodayStepsCount = nSteps;
            OnStepsUpdate?.Invoke(this);

            lLocalData.nLastTodaySteps = nSteps;
            LocalDataSaver<LocalData>.SaveCurrentData();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnStepsUpdate = null;
        }
    }
}