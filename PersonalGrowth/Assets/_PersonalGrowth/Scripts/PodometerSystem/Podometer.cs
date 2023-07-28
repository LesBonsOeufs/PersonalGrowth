using Com.GabrielBernabeu.Common.DataManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    public delegate void PodometerEventHandler(Podometer sender);
    public class Podometer : Singleton<Podometer>
    {
        [SerializeField] private HealthConnectAARCaller healthConnect = default;

        public int NewStepsCount { get; private set; }

        public int TodayStepsCount
        {
            get
            {
                return _todayStepsCount;
            }

            private set
            {
                _todayStepsCount = value;
                OnStepsUpdate?.Invoke(this);
            }
        }
        private int _todayStepsCount;


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

            if (lLocalData.nLastTodaySteps > nSteps)
                lLocalData.nLastTodaySteps = 0;

            TodayStepsCount = nSteps;
            NewStepsCount = nSteps - lLocalData.nLastTodaySteps;

            lLocalData.nLastTodaySteps = nSteps;
            lLocalData.nCurrentSteps += NewStepsCount;
            LocalDataSaver<LocalData>.SaveCurrentData();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnStepsUpdate = null;
        }
    }
}