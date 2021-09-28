using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEditor;

namespace LS.Localiser.CSV
{
    public class CSVLoader
    {

        #region Public Fields
        #endregion

        #region Private Fields
        private string[] lineSeparator = { Environment.NewLine };
        private string[] separators = { "\";\"" };
        #endregion

        #region Accessors
        #endregion

        #region Private Methods
        #endregion


        #region Public Methods
        /// <summary>
        /// Returns a dictionary with the corresponding language.
        /// </summary>
        /// <param name="language">The dictionary's language.</param>
        /// <returns>Returns a dictionnary of LocalizationItem. (text, Sprite Path, AudioCilp Path)</returns>
        public Dictionary<string, LocalizationSystem.LocalizationItem> GetDictionaryValues(SystemLanguage language)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Languages/" + language.ToString());

            if (!csvFile)
                return null;

            Dictionary<string, LocalizationSystem.LocalizationItem> dictionary = new Dictionary<string, LocalizationSystem.LocalizationItem>();
            string[] lines = csvFile.text.Split(lineSeparator, StringSplitOptions.None);

            for (int i = 0; i < lines.Length - 1; ++i)
            {
                string line = lines[i];

                if (line != "")
                {
                    string[] fields = line.Split(separators, StringSplitOptions.None);
                    if (fields.Length == 4)
                    {
                        string key = fields[0].TrimStart('\r', '\n', '\"');
                        string text = fields[1].TrimEnd('\r', '\n', '\"', ';');
                        string spritepath = fields[2].TrimEnd('\r', '\n', '\"', ';');
                        string audiopath = fields[3].TrimEnd('\r', '\n', '\"', ';');

                        dictionary.Add(key, new LocalizationSystem.LocalizationItem(text, spritepath, audiopath));
                    }
                }
            }

            return dictionary;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Add new CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="value">Text value to pass.</param>
        /// <param name="spritePath">Sprite Path to pass.</param>
        /// <param name="audioPath">AudioClip Path to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        public void Add(string key, string value, string spritePath, string audioPath, SystemLanguage language)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Languages/" + language.ToString());

            if (!csvFile)
                return;

            string path = AssetDatabase.GetAssetPath(csvFile);
            string appended = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";", key, value, spritePath, audioPath);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(appended);
            }

            UnityEditor.AssetDatabase.Refresh();
        }
        /// <summary>
        /// Remove CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        public void Remove(string key, SystemLanguage language)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Languages/" + language.ToString());

            if (!csvFile)
                return;

            string path = AssetDatabase.GetAssetPath(csvFile);
            string[] lines = csvFile.text.Split(lineSeparator, StringSplitOptions.None);
            string[] keys = new string[lines.Length - 1];

            for (int i = 0; i < lines.Length - 1; i++)
            {
                string line = lines[i];
                keys[i] = line.Split(separators, System.StringSplitOptions.None)[0].TrimStart('\r', '\n', '\"');
            }

            int index = -1;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Contains(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
            {
                string[] newLines;
                newLines = lines.Where(w => w != lines[index]).ToArray();
                string replaced = string.Join(lineSeparator[0].ToString(), newLines);
                File.WriteAllText(path, replaced);
                UnityEditor.AssetDatabase.Refresh();
            }
        }
        /// <summary>
        /// Edit CSV Key in the Language file. 
        /// </summary>
        /// <param name="key">CSV Key to pass.</param>
        /// <param name="value">Text value to pass.</param>
        /// <param name="spritePath">Sprite Path to pass.</param>
        /// <param name="audioPath">AudioClip Path to pass.</param>
        /// <param name="language">SystemLanguage to pass.</param>
        public void Edit(string key, string value, string spritePath, string audioPath, SystemLanguage language)
        {
            Remove(key, language);
            Add(key, value, spritePath, audioPath, language);
        }
#endif
        #endregion


    }
}