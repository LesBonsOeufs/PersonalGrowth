///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 18/01/2023 11:06
///-----------------------------------------------------------------

using TMPro;
using UnityEngine;

namespace Com.WorldGame.ForeverInsurer.Localization
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private ELanguage language = default;
        [SerializeField] GameObject[] selectedHovers = default;
        [SerializeField] TextMeshProUGUI text = default;

        private void Start()
        {
            if (LanguageManager.Localization == language)
                SetHover();
        }

        public void Select()
        {
            LanguageManager.SetLocalization(language);
        }

        public void SetHover()
        {
            for (int i = 0; i < selectedHovers.Length; i++)
            {
                selectedHovers[i].SetActive(true);
            }

            text.color = new Color(34 / 255, 34 / 255, 34 / 255);
        }
    }
}