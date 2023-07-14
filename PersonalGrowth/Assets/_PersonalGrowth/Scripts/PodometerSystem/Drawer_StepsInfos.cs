using TMPro;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.PodometerSystem {
    public class Drawer_StepsInfos : MonoBehaviour
    {
        private const int STEPS_PER_KILOMETER = 1350;
        private const int KCALORIES_PER_KILOMETER = 54;

        [Header("TMPs")]
        [SerializeField] private TextMeshProUGUI stepsTmp = default;
        [SerializeField] private TextMeshProUGUI kilometersTmp = default;
        [SerializeField] private TextMeshProUGUI kcaloriesTmp = default;

        [Header("Prefixes")]
        [SerializeField] private string stepsPrefix = "Steps: ";
        [SerializeField] private string kilometersPrefix = "Kilometers: ";
        [SerializeField] private string kcaloriesPrefix = "Kcalories: ";

        private void Awake()
        {
            Podometer.Instance.OnStepsUpdate += Podometer_OnStepsUpdate;
        }

        private void Podometer_OnStepsUpdate(Podometer sender)
        {
            SetSteps(sender.TodayStepsCount);
        }

        public void SetSteps(int nSteps)
        {
            float lNKilometers = (float)nSteps / STEPS_PER_KILOMETER;
            float lNKCalories = lNKilometers * KCALORIES_PER_KILOMETER;

            stepsTmp.text = stepsPrefix + nSteps;
            kilometersTmp.text = kilometersPrefix + lNKilometers;
            kcaloriesTmp.text = kcaloriesPrefix + lNKCalories;
        }

        private void OnDestroy()
        {
            if (Podometer.Instance != null)
                Podometer.Instance.OnStepsUpdate -= Podometer_OnStepsUpdate;
        }
    }
}