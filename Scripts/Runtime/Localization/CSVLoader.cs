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
        private string[] lineSeparator = { "\";\"endline\"" };
        private string[] separators = { "\";\"" };
        #endregion

        #region Accessors
        #endregion

        #region Private Methods
        #endregion


        #region Public Methods
        public Dictionary<string, string> GetDictionaryValues(SystemLanguage language)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Languages/" + language.ToString());

            if (!csvFile)
                return null;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] lines = csvFile.text.Split(lineSeparator, StringSplitOptions.None);

            for (int i = 0; i < lines.Length - 1; ++i)
            {
                string line = lines[i];
                string[] fields = line.Split(separators, StringSplitOptions.None);
                dictionary.Add(fields[0].TrimStart('\r', '\n', '\"'), fields[1].TrimEnd('\r', '\n', '\"', ';'));
            }

            return dictionary;
        }

#if UNITY_EDITOR
        public void Add(string key, string value, SystemLanguage language)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("Languages/" + language.ToString());

            if (!csvFile)
                return;

            string path = AssetDatabase.GetAssetPath(csvFile);
            string appended = string.Format("\"{0}\";\"{1}\";\"endline\"", key, value);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(appended);
            }

            UnityEditor.AssetDatabase.Refresh();
        }

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

        public void Edit(string key, string value, SystemLanguage language)
        {
            Remove(key, language);
            Add(key, value, language);
        }
#endif
        #endregion


    }
}