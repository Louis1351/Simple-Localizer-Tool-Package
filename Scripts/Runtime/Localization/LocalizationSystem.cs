using UnityEngine;
using System.Collections.Generic;
using LS.Localiser.Utils;
using System;
using LS.Localiser.Runtime.Components;

namespace LS.Localiser.CSV
{
    public class LocalizationSystem
    {
        public struct LocalizationItem
        {
            public string text;
            public string spritePath;
            public string clipPath;

            public LocalizationItem(string text, string spritePath, string clipPath)
            {
                this.text = text;
                this.spritePath = spritePath;
                this.clipPath = clipPath;
            }
        }

        #region Public Fields

        public static SystemLanguage useLanguage = SystemLanguage.French;
        public static CSVLoader csvLoader;
        #endregion


        #region Private Fields
        private static Dictionary<SystemLanguage, Dictionary<string, LocalizationItem>> dictionaries;
        private static bool alreadyLoaded = false;
        #endregion


        #region Accessors
        #endregion


        #region MonoBehaviour Methods
        #endregion


        #region Private Methods
        #endregion


        #region Public Methods
        /// <summary>
        /// Change the language in game. 
        /// </summary>
        /// <param name="language">SystemLanguage to pass.</param>
        /// <param name="callback">Callback to execute if the dictionary has been loaded.</param>
        public static void ChangeLanguage(SystemLanguage language, Action callback = null)
        {
            Dictionary<string, LocalizationItem> tmpDictionnary = null;

            useLanguage = language;
            tmpDictionnary = GetDictionary(useLanguage);

            if (tmpDictionnary == null || tmpDictionnary.Count == 0)
                return;

            RefreshSceneTexts();
            RefreshSceneImages();

            PlayerPrefs.SetInt("language", (int)useLanguage);

            if (callback != null)
                callback.Invoke();
        }
        /// <summary>
        /// Change the next language in game.(Order by the enum SystemLanguage ) 
        /// </summary>
        /// <param name="callback">Callback to execute if the dictionary has been loaded.</param>
        public static void ChangeToNextLanguage(Action callback = null)
        {
            Dictionary<string, LocalizationItem> tmpDictionnary = null;
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

            RefreshSceneTexts();
            RefreshSceneImages();

            PlayerPrefs.SetInt("language", (int)useLanguage);

            if (callback != null)
                callback.Invoke();
        }
        /// <summary>
        /// Change the previous language in game.(Order by the enum SystemLanguage )
        /// </summary>
        /// <param name="callback">Callback to execute if the dictionary has been loaded.</param>
        public static void ChangeToPreviousLanguage(Action callback = null)
        {
            Dictionary<string, LocalizationItem> tmpDictionnary = null;
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

            RefreshSceneTexts();
            RefreshSceneImages();

            PlayerPrefs.SetInt("language", (int)useLanguage);

            if (callback != null)
                callback.Invoke();
        }
        /// <summary>
        /// Initialize the dictionary if it hasnt already been load from the CSV file. 
        /// </summary>
        /// <param name="language">SystemLanguage to pass.</param>
        public static void InitializeCSV(SystemLanguage _language)
        {
            if (csvLoader == null)
                csvLoader = new CSVLoader();

            if (dictionaries == null)
                dictionaries = new Dictionary<SystemLanguage, Dictionary<string, LocalizationItem>>();

            if (!dictionaries.ContainsKey(_language))
            {
                dictionaries.Add(_language, csvLoader.GetDictionaryValues(_language));
            }
            //      UpdateDictionary(_language);
        }
        /// <summary>
        /// Auto Selection language thanks to Application.systemLanguage. 
        /// </summary>
        public static void AutoSelectLanguage()
        {
            useLanguage = Application.systemLanguage;

            if (PlayerPrefs.HasKey("language"))
            {
                useLanguage = (SystemLanguage)PlayerPrefs.GetInt("language");
            }

            RefreshSceneTexts();
            RefreshSceneImages();
        }
        /// <summary>
        /// Override the dictionary with the CSV file. 
        /// </summary>
        /// <param name="language">SystemLanguage to pass.</param>
        private static void UpdateDictionary(SystemLanguage language)
        {
            dictionaries[language] = csvLoader.GetDictionaryValues(language);

            if (dictionaries[language] == null || dictionaries[language].Count == 0)
            {
                dictionaries[language] = null;
                FileUtils.RemoveContentLanguageFile(language.ToString());
            }
        }
        /// <summary>
        /// Get the informations about the key.
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        /// <returns>Returns LocalizationItem. (text, Sprite Path, AudioCilp Path)</returns>
        public static LocalizationItem GetLocalisedValue(string key, SystemLanguage language)
        {
            LocalizationItem value = new LocalizationItem();

            InitializeCSV(language);

            if (dictionaries != null && dictionaries[language] != null)
            {
                if (key != null)
                    dictionaries[language].TryGetValue(key, out value);
            }
            return value;
        }
        /// <summary>
        /// Checks if the key exists in the corresponding language.
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        /// <returns>Returns true if the key exists in the dictionary</returns>
        public static bool ContainKey(string key, SystemLanguage language)
        {
            if (dictionaries != null && dictionaries[language] != null)
            {
                return dictionaries[language].ContainsKey(key);
            }
            return false;
        }
        /// <summary>
        /// Get the informations about the key.
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <returns>Returns LocalizationItem. (text, Sprite Path, AudioCilp Path)</returns>
        public static LocalizationItem GetLocalisedValue(string key)
        {
            LocalizationItem value = new LocalizationItem();

            if (!alreadyLoaded)
            {
                if (PlayerPrefs.HasKey("language"))
                {
                    useLanguage = (SystemLanguage)PlayerPrefs.GetInt("language");
                }
                else
                {
                    if (!FileUtils.FindLanguageFile(useLanguage.ToString()))
                    {
                        FileUtils.ReadSettingsFile(out SystemLanguage language, out string format);
                        useLanguage = language;
                    }

                    PlayerPrefs.SetInt("language", (int)useLanguage);
                }

                alreadyLoaded = true;
            }

            InitializeCSV(useLanguage);

            if (dictionaries != null && dictionaries[useLanguage] != null)
            {
                if (key != null && !dictionaries[useLanguage].TryGetValue(key, out value))
                {
                    Debug.LogWarning("Any text found with the key \"" + key + "\" and the " + useLanguage.ToString() + " language.");
                }
            }
            return value;
        }
        /// <summary>
        /// Add new CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="value">Text value to pass.</param>
        /// <param name="spritePath">Sprite Path to pass.</param>
        /// <param name="audioPath">AudioClip Path to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        public static void Add(string key, string value, string spritePath, string audioPath, SystemLanguage language)
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
            csvLoader.Add(key, value, spritePath, audioPath, language);
#endif
            UpdateDictionary(language);
        }
        /// <summary>
        /// Override CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="value">Text value to pass.</param>
        /// <param name="spritePath">Sprite Path to pass.</param>
        /// <param name="audioPath">AudioClip Path to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        public static void Replace(string key, string value, string spritePath, string audioPath, SystemLanguage language)
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
            csvLoader.Edit(key, value, spritePath, audioPath, language);
#endif

            UpdateDictionary(language);
        }
        /// <summary>
        /// Remove CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
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
        /// <summary>
        /// Get the dictionary corresponding to the language.
        /// </summary>
        /// <param name="language">SystemLanguage to pass.</param>
        /// <returns>Returns a dictionnary of LocalizationItem. (text, Sprite Path, AudioCilp Path)</returns>
        public static Dictionary<string, LocalizationItem> GetDictionary(SystemLanguage language)
        {
            InitializeCSV(language);

            if (dictionaries == null)
                return null;
            else
                return dictionaries[language];
        }
        /// <summary>
        /// Get the dictionary corresponding to the language.
        /// </summary>
        /// <returns>Returns a dictionnary of LocalizationItem. (text, Sprite Path, AudioCilp Path)</returns>
        public static Dictionary<string, LocalizationItem> GetDictionary()
        {
            InitializeCSV(useLanguage);

            if (dictionaries == null)
                return null;
            else
                return dictionaries[useLanguage];
        }
        #endregion
        /// <summary>
        /// Refreshs all the texts in the scene. 
        /// </summary>
        private static void RefreshSceneTexts()
        {
            TextLocalizerUI[] components = Resources.FindObjectsOfTypeAll<TextLocalizerUI>();
            for (int i = 0; i < components.Length; ++i)
            {
                components[i].Refresh();
            }
        }
        /// <summary>
        /// Refreshs all the sprites/images in the scene. 
        /// </summary>
        private static void RefreshSceneImages()
        {
            ImageLocalizerUI[] components = Resources.FindObjectsOfTypeAll<ImageLocalizerUI>();
            for (int i = 0; i < components.Length; ++i)
            {
                components[i].Refresh();
            }
        }

    }
}