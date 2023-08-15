using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu {
    public class StepCoinsManager : Singleton<StepCoinsManager>
    {
        [SerializeField] private TextMeshProUGUI stepCoinsTmp = default;
        [SerializeField] private MMF_Player onNewStepsFeedbacks = default;

        public int Count { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Podometer.Instance.OnStepsUpdate += Podometer_OnStepsUpdate;
        }

        public void Consume(int nCoins)
        {
            int lLastCoinsCount = Count;
            Count -= nCoins;

            NewCoinsAnim(lLastCoinsCount, -nCoins);
            LocalDataSaver<LocalData>.CurrentData.stepCoinsCount = Count;
            LocalDataSaver<LocalData>.SaveCurrentData();
        }

        private void Podometer_OnStepsUpdate(Podometer sender)
        {
            LocalData lData = LocalDataSaver<LocalData>.CurrentData;
            int lLastCoinsCount = lData.stepCoinsCount;
            stepCoinsTmp.text = lLastCoinsCount.ToString();

            if (sender.NewStepsCount > 0)
            {
                lData.stepCoinsCount += sender.NewStepsCount;
                NewCoinsAnim(lLastCoinsCount, sender.NewStepsCount);
            }

            Count = lData.stepCoinsCount;
        }

        private void NewCoinsAnim(int startCoins, int deltaCoins)
        {
            MMF_FloatingText lFloatingText = onNewStepsFeedbacks.GetFeedbackOfType<MMF_FloatingText>();
            MMF_TMPCountTo lTMPCountTo = onNewStepsFeedbacks.GetFeedbackOfType<MMF_TMPCountTo>();
            string lPrefix = Mathf.Sign(deltaCoins) == 1 ? "+" : "-";

            lFloatingText.Value = $"{lPrefix}{Mathf.Abs(deltaCoins)}";
            lTMPCountTo.CountFrom = startCoins;
            lTMPCountTo.CountTo = startCoins + deltaCoins;

            onNewStepsFeedbacks.PlayFeedbacks();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (Podometer.Instance != null)
                Podometer.Instance.OnStepsUpdate -= Podometer_OnStepsUpdate;
        }
    }
}