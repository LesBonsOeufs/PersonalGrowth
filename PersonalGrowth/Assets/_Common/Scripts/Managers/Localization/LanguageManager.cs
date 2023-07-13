
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
///-----------------------------------------------------------------
///   Author : Gabriel Bernabeu                    
///   Date   : 18/01/2023 10:41
///-----------------------------------------------------------------
namespace Com.WorldGame.ForeverInsurer.Localization 
{
    public delegate void LanguageManagerEventHandler();
    public static class LanguageManager
    {
        private const string FILE_PATH = "Localization/";
        private const string DEFAULT_KEY = "DEFAULT";

        private static Dictionary<ELanguage, Dictionary<string, string>> locaDictionary;

        public static ELanguage Localization { get; private set; } = ELanguage.FRENCH;

        public static event LanguageManagerEventHandler OnUpdate;

        public static void InitLocaDictionary()
        {
            locaDictionary = new Dictionary<ELanguage, Dictionary<string, string>>();

            foreach (ELanguage language in (ELanguage[])Enum.GetValues(typeof(ELanguage)))
                locaDictionary[language] = LoadJsonLocalizationFile(FILE_PATH, language);
        }

        private static Dictionary<string, string> LoadJsonLocalizationFile(string path, ELanguage lang)
		{
            Dictionary<string, string> lResult = new Dictionary<string, string>();
			string lFilePath = $"{path}{lang}";
			TextAsset lJsonFile = Resources.Load<TextAsset>(lFilePath);

			if (lJsonFile == null)
            {
				Debug.Log($"No JSON Found. path:{lFilePath}");
                return null;
            }
			else
				JsonConvert.PopulateObject(lJsonFile.text, lResult);

            return lResult;
		}

        public static void SetLocalization(ELanguage localization)
        {
            Localization = localization;
            OnUpdate?.Invoke();
        }

        public static string GetLocalizedText(string key) 
		{
            Dictionary<string, string> lDictionary = locaDictionary[Localization];

            if (key != null && lDictionary.ContainsKey(key))
                return lDictionary[key];
            else
                return lDictionary[DEFAULT_KEY];
		}
    }
}