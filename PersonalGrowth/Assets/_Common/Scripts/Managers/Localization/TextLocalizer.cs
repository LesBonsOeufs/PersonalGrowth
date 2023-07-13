///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 18/01/2023 11:15
///-----------------------------------------------------------------

using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using UnityEngine;

namespace Com.WorldGame.ForeverInsurer.Localization 
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizer : MonoBehaviour
    {
        [SerializedDictionary, SerializeField] 
        private SerializedDictionary<ELanguage, string> translations = default;

        private void Awake()
        {
            LanguageManager.OnUpdate += LanguageManager_OnUpdate;
            LanguageManager_OnUpdate();
        }

        private void LanguageManager_OnUpdate()
        {
            GetComponent<TextMeshProUGUI>().text = translations[LanguageManager.Localization];
        }

        private void OnDestroy()
        {
            LanguageManager.OnUpdate -= LanguageManager_OnUpdate;
        }
    }
}