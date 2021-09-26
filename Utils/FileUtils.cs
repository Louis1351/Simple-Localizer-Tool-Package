using System.IO;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Utils
{
    public class FileUtils
    {
        public static void CreateFolder(string _folderName)
        {
            string path = Application.dataPath + _folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void FindOrCreateSettingsFile(string _language, string _format)
        {
            string path = Application.dataPath + "/Resources/Languages/Settings.txt";

            CreateFolder("/Resources");
            CreateFolder("/Resources/Languages");

            File.WriteAllText(path, _language + " " + _format);

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }

        public static bool FindLanguageFile(string _fileName)
        {
            string path = Application.dataPath + "/Resources/Languages/" + _fileName + ".csv";
            return File.Exists(path);
        }

        public static bool ReadSettingsFile(out SystemLanguage _language, out string _format)
        {
            string path = Application.dataPath + "/Resources/Languages/Settings.txt";
            _language = SystemLanguage.English;
            _format = "CSV";

            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                string[] contents = content.Split(' ');
                _language = (SystemLanguage)int.Parse(contents[0]);
                _format = contents[1];
                return true;
            }
            return false;
        }

        public static void FindOrCreateLanguageFile(string _fileName)
        {
            string path = Application.dataPath + "/Resources/Languages/" + _fileName + ".csv";

            CreateFolder("/Resources");
            CreateFolder("/Resources/Languages");

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
#if UNITY_EDITOR
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
#endif
            }
        }

        public static void ClearContentLanguageFile(string _fileName)
        {
            string path = Application.dataPath + "/Resources/Languages/" + _fileName + ".csv";
            if (File.Exists(path))
            {
                File.WriteAllText(path, "");
#if UNITY_EDITOR
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
#endif
            }
        }

        public static void RemoveContentLanguageFile(string _fileName)
        {
            string path = Application.dataPath + "/Resources/Languages/" + _fileName + ".csv";
            string pathMeta = Application.dataPath + "/Resources/Languages/" + _fileName + ".csv.meta";
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Delete(pathMeta);
#if UNITY_EDITOR
                AssetDatabase.Refresh();
#endif
            }
        }
    }
}