using Com.GabrielBernabeu.PersonalGrowth.PodometerSystem;
using MoreMountains.Feedbacks;
using System;
using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu {
    public class StepCoinsManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stepCoinsTmp = default;
        [SerializeField] private MMF_Player onNewStepsFeedbacks = default;

        public int Count { get; private set; }

        private void Awake()
        {
            Podometer.Instance.OnStepsUpdate += Podometer_OnStepsUpdate;
        }

        public void Consume(int nCoins)
        {
            Count -= nCoins;
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

        private void NewCoinsAnim(int lastCoinsCount, int newStepsCount)
        {
            MMF_FloatingText lFloatingText = onNewStepsFeedbacks.GetFeedbackOfType<MMF_FloatingText>();
            MMF_TMPCountTo lTMPCountTo = onNewStepsFeedbacks.GetFeedbackOfType<MMF_TMPCountTo>();
            lFloatingText.Value = $"+{newStepsCount}";
            lTMPCountTo.CountFrom = lastCoinsCount;
            lTMPCountTo.CountTo = lastCoinsCount + newStepsCount;

            onNewStepsFeedbacks.PlayFeedbacks();
        }

        private void OnDestroy()
        {
            if (Podometer.Instance != null)
                Podometer.Instance.OnStepsUpdate -= Podometer_OnStepsUpdate;
        }
    }
}
