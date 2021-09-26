using System;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Editor
{
    public static class UtilsArray
    {
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
    }

    public class TextLocalizerEditWindow : EditorWindow
    {

        #region Public Fields
        #endregion


        #region Private Fields
        public enum Tab
        {
            spreadsheet,
            modification,
            translation,
            settings
        }

        [SerializeField]
        private string key = "";
        [SerializeField]
        private Vector2 minSizeW = new Vector2(500, 500);
        [SerializeField]
        private Vector2 maxSizeW = new Vector2(1920, 1080);
        [SerializeField]
        private Tab tab = Tab.spreadsheet;
        [SerializeField]
        private string[] languageTabNames = null;
        [SerializeField]
        private SpreadsheetTab spreadsheetTab = null;
        [SerializeField]
        private ModificationTab modificationTab = null;
        [SerializeField]
        private TranslationTab translationTab = null;
        [SerializeField]
        private SettingsTab settingsTab = null;
        #endregion


        #region Accessors
        public string Key
        {
            get => key;
            set
            {
                key = value;
                modificationTab.AddText(key);
            }
        }

        public string[] LanguageTabNames { get => languageTabNames; set => languageTabNames = value; }
        public SpreadsheetTab SpreadsheetTab { get => spreadsheetTab; set => spreadsheetTab = value; }
        public ModificationTab ModificationTab { get => modificationTab; set => modificationTab = value; }
        public TranslationTab TranslationTab { get => translationTab; set => translationTab = value; }
        public SettingsTab SettingsTab { get => settingsTab; set => settingsTab = value; }
        public Tab TabEnum { get => tab; set => tab = value; }
        #endregion


        #region EditorWindow Methods
        public void Init()
        {
            TextLocalizerEditWindow window = EditorWindow.GetWindow<TextLocalizerEditWindow>();
            titleContent = new GUIContent("Simple Localization");

            InitializeSelectionLanguages();
            minSize = minSizeW;
            maxSize = maxSizeW;

            spreadsheetTab = new SpreadsheetTab(window);
            modificationTab = new ModificationTab(window);
            translationTab = new TranslationTab(window);
            settingsTab = new SettingsTab(window);
            Show();
        }

        private void OnGUI()
        {
            bool change = false;

            EditorGUI.BeginChangeCheck();
            tab = (Tab)GUILayout.Toolbar((int)tab, new string[] { "Spreadsheet", "Modification", "Translation", "Settings" }, GUILayout.Height(30));


            if (EditorGUI.EndChangeCheck())
            {
                change = true;
                GUI.FocusControl(null);
            }

            switch (tab)
            {
                case Tab.spreadsheet:
                    if (change)
                    {
                        spreadsheetTab.Refresh();
                    }
                    spreadsheetTab.OnGUI();
                    break;
                case Tab.modification:
                    modificationTab.OnGUI();
                    break;
                case Tab.translation:
                    translationTab.OnGUI();
                    break;
                case Tab.settings:
                    if (change)
                        settingsTab.Start();

                    settingsTab.OnGUI();
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void InitializeSelectionLanguages()
        {
            if (languageTabNames == null)
            {
                languageTabNames = System.Enum.GetNames(typeof(SystemLanguage));
                languageTabNames = languageTabNames.RemoveAt(19);
            }
        }
        #endregion


        #region Public Methods
        [MenuItem("Localization/Edit")]
        public static void Open()
        {
            TextLocalizerEditWindow window = ScriptableObject.CreateInstance<TextLocalizerEditWindow>();
            window.Init();
        }
        #endregion
    }
}