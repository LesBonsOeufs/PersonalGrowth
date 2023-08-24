using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth {
    public class StepCoinsManager : Singleton<StepCoinsManager>
    {
        [SerializeField] private TextMeshProUGUI stepCoinsTmp = default;
        [SerializeField] private MMF_Player onNewStepsFeedbacks = default;

        [Header("Maximums")]
        [SerializeField] private int maxDailyCoins = 1000;
        [SerializeField] private int maxTotalCoins = 10000;

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
            int lCoinsGain;

            if (sender.TodayStepsCount > maxDailyCoins)
            {
                int lLastSessionStepsCount = sender.TodayStepsCount - sender.StepsCountSinceLast;
                // All coins have been retrieved, as the maxDailyCoins was already exceeded on last session
                if (lLastSessionStepsCount > maxDailyCoins)
                {
                    lCoinsGain = 0;
                    GeneralTextFeedback.Instance.MakeText($"Already retrieved today's coins ({maxDailyCoins} per day max)!");
                }
                else
                {
                    // This session is the first with an exceeded maxDailyCoins today
                    lCoinsGain = maxDailyCoins - lLastSessionStepsCount;
                }
            }
            else
                lCoinsGain = sender.StepsCountSinceLast;

            if (lCoinsGain > 0)
            {
                int lNewCoinsCount = lData.stepCoinsCount + sender.StepsCountSinceLast;

                if (lNewCoinsCount > maxTotalCoins)
                {
                    lNewCoinsCount = maxTotalCoins;
                    GeneralTextFeedback.Instance.MakeText($"You can't have more than {maxTotalCoins} coins in total!");
                }

                NewCoinsAnim(lLastCoinsCount, lNewCoinsCount - lLastCoinsCount);
                lData.stepCoinsCount = lNewCoinsCount;
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