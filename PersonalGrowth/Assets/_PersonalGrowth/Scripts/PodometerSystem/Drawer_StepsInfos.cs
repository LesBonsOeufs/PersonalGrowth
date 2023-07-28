using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    public class Drawer_StepsInfos : MonoBehaviour
    {
        private const int STEPS_PER_KILOMETER = 1350;
        private const int KCALORIES_PER_KILOMETER = 54;

        [Header("TMPs")]
        [SerializeField] private TextMeshProUGUI newStepsTmp = default;
        [SerializeField] private TextMeshProUGUI todayStepsTmp = default;
        [SerializeField] private TextMeshProUGUI kilometersTmp = default;
        [SerializeField] private TextMeshProUGUI kcaloriesTmp = default;

        private void Awake()
        {
            Podometer.Instance.OnStepsUpdate += Podometer_OnStepsUpdate;
        }

        private void Podometer_OnStepsUpdate(Podometer sender)
        {
            SetSteps(sender);
        }

        public void SetSteps(Podometer podometer)
        {
            int lNewStepsCount = podometer.NewStepsCount;
            int lTodayStepsCount = podometer.TodayStepsCount;

            float lNKilometers = (float)lTodayStepsCount / STEPS_PER_KILOMETER;
            float lNKCalories = lNKilometers * KCALORIES_PER_KILOMETER;

            newStepsTmp.text = lNewStepsCount.ToString();
            kilometersTmp.text = lNKilometers.ToString("F2");
            kcaloriesTmp.text = lNKCalories.ToString("F2");
            todayStepsTmp.text = lTodayStepsCount.ToString();
        }

        private void OnDestroy()
        {
            if (Podometer.Instance != null)
                Podometer.Instance.OnStepsUpdate -= Podometer_OnStepsUpdate;
        }
    }
}