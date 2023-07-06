using Com.GabrielBernabeu.Common.DataManagement;
using UnityEngine;

namespace Com.GabrielBernabeu.Placeholder 
{
    public delegate void PodometerEventHandler(Podometer sender);
    public class Podometer : Singleton<Podometer>
    {
        [SerializeField] private HealthConnectAARCaller healthConnect = default;

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
            LocalDataSaving.LoadData();
        }

        private void Start()
        {
            healthConnect.GetTodayStepsCount(OnStepsCountReceived);
        }

        private void OnStepsCountReceived(int nSteps)
        {
            TodayStepsCount = nSteps;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnStepsUpdate = null;
        }
    }
}