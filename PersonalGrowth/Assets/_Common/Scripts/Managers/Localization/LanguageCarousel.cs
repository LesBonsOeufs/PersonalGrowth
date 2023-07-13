///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 19/01/2023 16:28
///-----------------------------------------------------------------

using System;
using UnityEngine;
using TMPro;

namespace Com.WorldGame.ForeverInsurer.Localization
{
    public class LanguageCarousel : MonoBehaviour
    {
        [SerializeField] GameObject[] languagesSelectedHover = default;
        [SerializeField] TextMeshProUGUI[] languagesSelectedText = default;

        private void Awake()
        {
            if (languagesSelectedHover.Length > 0 && languagesSelectedText.Length > 0)
            {
                ResetCarousel();
            }
        }

        public void ToNext()
        {
            LanguageManager.SetLocalization(GetNextLanguage());
        }

        public void ToPrevious()
        {
            LanguageManager.SetLocalization(GetPreviousLanguage());
        }

        private ELanguage GetNextLanguage()
        {
            ELanguage[] lArray = (ELanguage[])Enum.GetValues(typeof(ELanguage));
            int lNextIndex = Array.IndexOf(lArray, LanguageManager.Localization) + 1;
            return (lArray.Length <= lNextIndex) ? lArray[0] : lArray[lNextIndex];
        }

        private ELanguage GetPreviousLanguage()
        {
            ELanguage[] lArray = (ELanguage[])Enum.GetValues(typeof(ELanguage));
            int lNextIndex = Array.IndexOf(lArray, LanguageManager.Localization) - 1;
            return (lNextIndex < 0) ? lArray[lArray.Length - 1] : lArray[lNextIndex];
        }

        public void ResetCarousel()
        {
            for (int i = 0; i < languagesSelectedHover.Length; i++)
            {
                languagesSelectedHover[i].SetActive(false);
            }
            for (int i = 0; i < languagesSelectedText.Length; i++)
            {
                languagesSelectedText[i].color = new Color(96f / 255f, 96f / 255f, 96f / 255f);
            }
        }
    }
}