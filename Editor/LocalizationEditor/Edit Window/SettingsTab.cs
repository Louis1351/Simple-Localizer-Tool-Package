using LS.Localiser.Utils;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Editor
{
    [System.Serializable]
    public class SettingsTab
    {
        #region Private Fields
        public enum Format
        {
            CSV,
            GoogleSheet
        }

        [SerializeField]
        private TextLocalizerEditWindow window = null;
        [SerializeField]
        private int DefaultLanguage = 10;//English
        [SerializeField]
        private Format format = Format.CSV;
        #endregion
        public SettingsTab(TextLocalizerEditWindow _window)
        {
            window = _window;
        }

        #region Public Methods
        public void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Default Language", GUILayout.ExpandWidth(false));
            DefaultLanguage = EditorGUILayout.Popup(DefaultLanguage, window.LanguageTabNames, GUILayout.Width(150), GUILayout.Height(30));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Files Format", GUILayout.ExpandWidth(false));
            format = (Format)EditorGUILayout.Popup((int)format, new string[] { "CSV", "Google Sheet" }, GUILayout.Width(150), GUILayout.Height(30));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            if (EditorGUI.EndChangeCheck())
            {
                FileUtils.FindOrCreateSettingsFile(DefaultLanguage.ToString(), format.ToString());
            }
        }
        #endregion
    }
}
