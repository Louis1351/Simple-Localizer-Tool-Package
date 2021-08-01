using UnityEngine;
using System.Collections.Generic;
using LS.Localiser.Utils;
using LS.Localiser.Runtime;

namespace LS.Localiser.CSV
{
    public class LocalizationSystem
    {

        #region Public Fields

        public static SystemLanguage useLanguage = SystemLanguage.French;
        public static CSVLoader csvLoader;
        #endregion


        #region Private Fields
        private static Dictionary<SystemLanguage, Dictionary<string, string>> dictionaries;
        private static bool alreadyLoaded = false;
        #endregion


        #region Accessors
        #endregion


        #region MonoBehaviour Methods
        #endregion


        #region Private Methods
        #endregion


        #region Public Methods
        public static void ChangeLanguage(SystemLanguage language)
        {
            Dictionary<string, string> tmpDictionnary = null;

            useLanguage = language;
            tmpDictionnary = GetDictionary(useLanguage);

            if (tmpDictionnary == null || tmpDictionnary.Count == 0)
                return;

            TextLocalizerUI[] components = Resources.FindObjectsOfTypeAll<TextLocalizerUI>();
            for (int i = 0; i < components.Length; ++i)
            {
                components[i].Refresh();
            }

            PlayerPrefs.SetInt("language", (int)useLanguage);
        }

        public static void ChangeToNextLanguage()
        {
            Dictionary<string, string> tmpDictionnary = null;
            int nbTries = 0;

            do
            {
                useLanguage = (SystemLanguage)(((int)useLanguage + 1) % ((int)SystemLanguage.Unknown));
                tmpDictionnary = GetDictionary(useLanguage);
                nbTries++;

                if (nbTries >= 42)
                    return;
            }
            while (tmpDictionnary == null || tmpDictionnary.Count == 0);

            TextLocalizerUI[] components = Resources.FindObjectsOfTypeAll<TextLocalizerUI>();
            for (int i = 0; i < components.Length; ++i)
            {
                components[i].Refresh();
            }

            PlayerPrefs.SetInt("language", (int)useLanguage);
        }

        public static void ChangeToPreviousLanguage()
        {
            Dictionary<string, string> tmpDictionnary = null;
            int nbTries = 0;

            do
            {
                useLanguage = (SystemLanguage)(((int)useLanguage - 1));
                if ((int)useLanguage < 0)
                {
                    useLanguage = SystemLanguage.ChineseTraditional;
                }

                tmpDictionnary = GetDictionary(useLanguage);
                nbTries++;

                if (nbTries >= 42)
                    return;
            }
            while (tmpDictionnary == null || tmpDictionnary.Count == 0);

            TextLocalizerUI[] components = Resources.FindObjectsOfTypeAll<TextLocalizerUI>();
            for (int i = 0; i < components.Length; ++i)
            {
                components[i].Refresh();
            }

            PlayerPrefs.SetInt("language", (int)useLanguage);
        }

        public static void InitializeCSV(SystemLanguage _language)
        {
            if (csvLoader == null)
                csvLoader = new CSVLoader();

            if (dictionaries == null)
                dictionaries = new Dictionary<SystemLanguage, Dictionary<string, string>>();

            if (!dictionaries.ContainsKey(_language))
            {
                dictionaries.Add(_language, csvLoader.GetDictionaryValues(_language));
            }
            //      UpdateDictionary(_language);
        }

        public static void AutoSelectLanguage()
        {
            useLanguage = Application.systemLanguage;

            if (PlayerPrefs.HasKey("language"))
                useLanguage = (SystemLanguage)PlayerPrefs.GetInt("language");
        }

        private static void UpdateDictionary(SystemLanguage language)
        {
            dictionaries[language] = csvLoader.GetDictionaryValues(language);

            if (dictionaries[language] == null || dictionaries[language].Count == 0)
            {
                dictionaries[language] = null;
                FileUtils.RemoveContentLanguageFile(language.ToString());
            }
        }

        public static string GetLocalisedValue(string key, SystemLanguage language)
        {
            string value = "";

            InitializeCSV(language);

            if (dictionaries != null && dictionaries[language] != null)
            {
                dictionaries[language].TryGetValue(key, out value);
            }
            return value;
        }

        public static string GetLocalisedValue(string key)
        {
            string value = key;

            if (!alreadyLoaded)
            {
                if (PlayerPrefs.HasKey("language"))
                {
                    useLanguage = (SystemLanguage)PlayerPrefs.GetInt("language");
                }
                else
                {
                    FileUtils.ReadSettingsFile(out SystemLanguage language, out string format);
                    useLanguage = language;

                    PlayerPrefs.SetInt("language", (int)language);
                }

                alreadyLoaded = true;
            }

            InitializeCSV(useLanguage);

            if (dictionaries != null && dictionaries[useLanguage] != null)
            {
                if (!dictionaries[useLanguage].TryGetValue(key, out value))
                {
                    Debug.LogWarning("Any text found with the key \"" + key + "\" and the " + useLanguage.ToString() + " language.");
                }
            }
            return value;
        }
        public static void Add(string key, string value, SystemLanguage language)
        {
            if (key == "")
                return;

            if (value != null && value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            if (csvLoader == null)
            {
                csvLoader = new CSVLoader();
            }

#if UNITY_EDITOR
            FileUtils.FindOrCreateLanguageFile(language.ToString());
            csvLoader.Add(key, value, language);
#endif
            UpdateDictionary(language);
        }

        public static void Replace(string key, string value, SystemLanguage language)
        {
            if (key == "")
                return;

            if (value != null && value.Contains("\""))
            {
                value.Replace('"', '\"');
            }

            if (csvLoader == null)
            {
                csvLoader = new CSVLoader();
            }

#if UNITY_EDITOR
            FileUtils.FindOrCreateLanguageFile(language.ToString());
            csvLoader.Edit(key, value, language);
#endif

            UpdateDictionary(language);
        }

        public static void Remove(string key, SystemLanguage language)
        {
            if (key == "")
                return;

            if (csvLoader == null)
            {
                csvLoader = new CSVLoader();
            }

#if UNITY_EDITOR
            FileUtils.FindOrCreateLanguageFile(language.ToString());
            csvLoader.Remove(key, language);
#endif

            UpdateDictionary(language);
        }

        public static Dictionary<string, string> GetDictionary(SystemLanguage language)
        {
            InitializeCSV(language);

            if (dictionaries == null)
                return null;
            else
                return dictionaries[language];
        }

        public static Dictionary<string, string> GetDictionary()
        {
            InitializeCSV(useLanguage);

            if (dictionaries == null)
                return null;
            else
                return dictionaries[useLanguage];
        }
        #endregion


    }
}