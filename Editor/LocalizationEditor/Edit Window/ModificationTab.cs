using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LS.Localiser.CSV;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Editor
{
    [System.Serializable]
    public class ModificationTab
    {
        #region Private Fields
        [SerializeField]
        private TextLocalizerEditWindow window = null;
        private int popup = 0;
        private int lastPopup = 0;
        [SerializeField]
        private string key = "";
        [SerializeField]
        private string textArea = "";
        [SerializeField]
        private UnityEngine.Object audioClip = null;
        [SerializeField]
        private UnityEngine.Object sprite = null;
        private Vector2 scrollPos1 = Vector2.zero;
        private Vector2 scrollPos2 = Vector2.zero;
        private Dictionary<string, LocalizationSystem.LocalizationItem> dictionary;
        private LocalizationSystem.LocalizationItem currentItem;
        #endregion

        #region Accessors
        public void ChangePopup(int _language)
        {
            popup = _language;
            lastPopup = _language;
        }
        #endregion

        public ModificationTab(TextLocalizerEditWindow _window)
        {
            window = _window;
        }
        #region Public Methods

        public void AddText(string _key)
        {

            key = _key;
            currentItem = LocalizationSystem.GetLocalisedValue(_key, (SystemLanguage)popup);
            textArea = currentItem.text;
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>(currentItem.spritePath);
            audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(currentItem.clipPath);
        }

        public void OnGUI()
        {
            if (key == null)
                key = "";

            GUIContent guiCtAdd = new GUIContent("Add", "tooltp");
            GUIContent guiCtRemove = new GUIContent("Remove", "tooltp");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Language", GUILayout.Width(150));
            popup = EditorGUILayout.Popup(popup, window.LanguageTabNames, GUILayout.Width(150));
            EditorGUILayout.EndHorizontal();

            SystemLanguage language = (SystemLanguage)popup;

            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));

            GUI.SetNextControlName("key");
            key = TextField(key, "Search key", GUI.skin.FindStyle("ToolbarSeachTextField"));

            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                key = "";
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();


            bool findKey = false;
            if (key != "" && GUI.GetNameOfFocusedControl() == "key")
            {

                if ((Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
                {
                    findKey = false;

                    string tmpArea = LocalizationSystem.GetLocalisedValue(key, language).text;

                    if (tmpArea != null)
                        textArea = tmpArea;

                    GUI.FocusControl(null);
                    window.Repaint();
                }

                GUIStyle buttonStyle = new GUIStyle("ToolbarButton");
                buttonStyle.alignment = TextAnchor.MiddleLeft;
                EditorGUILayout.BeginVertical();

                dictionary = LocalizationSystem.GetDictionary(language);

                if (dictionary != null)
                {

                    int nbKey = 0;
                    foreach (KeyValuePair<string, LocalizationSystem.LocalizationItem> element in dictionary)
                    {
                        if (element.Key.ToLower().Contains(key.ToLower())
                        || element.Value.text.ToLower().Contains(key.ToLower()))
                        {
                            if (!findKey)
                                scrollPos1 = EditorGUILayout.BeginScrollView(scrollPos1);

                            findKey = true;
                            EditorGUILayout.BeginHorizontal();
                            string value = Regex.Replace(element.Value.text, @"\t|\n|\r", "").Substring(0, Mathf.Min(element.Value.text.Length, 80));

                            if (GUILayout.Button(element.Key + "\t\t" + value, buttonStyle, GUILayout.Height(25)))
                            {
                                findKey = false;
                                key = element.Key;

                                string tmpArea = element.Value.text;
                                if (tmpArea != null)
                                    textArea = tmpArea;


                                sprite = AssetDatabase.LoadAssetAtPath<Sprite>(element.Value.spritePath);
                                audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(element.Value.clipPath);
                                currentItem = element.Value;
                                GUI.FocusControl(null);
                                break;
                            }
                            EditorGUILayout.EndHorizontal();

                            nbKey++;
                        }
                        if (nbKey > 20)
                            break;
                    }
                    if (findKey)
                        EditorGUILayout.EndScrollView();
                }

                EditorGUILayout.EndVertical();

            }

            sprite = EditorGUILayout.ObjectField(sprite, typeof(Sprite), true);
            if (LocalizationSystem.ContainKey(key, language) && currentItem.spritePath != "" && sprite == null)
                EditorGUILayout.HelpBox("Sprite Reference lost " + currentItem.spritePath, MessageType.Warning);

            audioClip = EditorGUILayout.ObjectField(audioClip, typeof(AudioClip), true);
            if (LocalizationSystem.ContainKey(key, language) && currentItem.clipPath != "" && audioClip == null)
                EditorGUILayout.HelpBox("Audio Clip Reference lost" + currentItem.clipPath, MessageType.Warning);

            if (!findKey)
            {
                EditorGUILayout.BeginVertical();
                scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
                EditorStyles.textArea.wordWrap = true;
                EditorStyles.textArea.fixedHeight = window.position.size.y;

                if (popup != lastPopup)
                {
                    currentItem = LocalizationSystem.GetLocalisedValue(key, language);
                    GUI.FocusControl(null);
                    lastPopup = popup;

                    textArea = currentItem.text;
                    sprite = AssetDatabase.LoadAssetAtPath<Sprite>(currentItem.spritePath);
                    audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(currentItem.clipPath);
                }

                textArea = TextArea(textArea, "Text", EditorStyles.textArea);
                EditorGUILayout.EndScrollView();

                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add", GUILayout.Height(50), GUILayout.Width(70)))
                {
                    var dictionnary = LocalizationSystem.GetDictionary(language);

                    string spritePath = AssetDatabase.GetAssetPath(sprite);
                    string audioPath = AssetDatabase.GetAssetPath(audioClip);

                    if (dictionnary != null && dictionnary.ContainsKey(key))
                    {
                        if (EditorUtility.DisplayDialog("Overwrite Key " + key + "?", "This will overwrite the element from localization, are you sure?", "Ok", "Cancel"))
                        {
                            LocalizationSystem.Replace(key, textArea, spritePath, audioPath, language);
                            ClearTexts();
                        }
                    }
                    else
                    {
                        LocalizationSystem.Add(key, textArea, spritePath, audioPath, language);
                        ClearTexts();
                    }
                }

                if (GUILayout.Button("Remove", GUILayout.Height(50), GUILayout.Width(70)))
                {
                    LocalizationSystem.Remove(key, language);
                    ClearTexts();
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                EditorStyles.textArea.fixedHeight = 0;
            }
        }
        #endregion
        #region Private Methods
        private string TextField(string text, string placeholder, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUI.BeginChangeCheck();
            var newText = EditorGUILayout.TextField(text, style, options);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(window, "Changed Localization Window 0");
            }

            if (String.IsNullOrEmpty(text.Trim()))
            {
                const int textMargin = 15;
                var guiColor = GUI.color;
                GUI.color = Color.grey;
                var textRect = GUILayoutUtility.GetLastRect();
                var position = new Rect(textRect.x + textMargin, textRect.y, textRect.width, textRect.height - 3);
                EditorGUI.LabelField(position, placeholder);
                GUI.color = guiColor;
            }
            return newText;
        }

        private string TextField(string text, string placeholder, params GUILayoutOption[] options)
        {
            EditorGUI.BeginChangeCheck();
            var newText = EditorGUILayout.TextField(text, options);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(window, "Changed Localization Window 1");
            }

            if (String.IsNullOrEmpty(text.Trim()))
            {
                const int textMargin = 2;
                var guiColor = GUI.color;
                GUI.color = Color.grey;
                var textRect = GUILayoutUtility.GetLastRect();
                var position = new Rect(textRect.x + textMargin, textRect.y, textRect.width, textRect.height);
                EditorGUI.LabelField(position, placeholder);
                GUI.color = guiColor;
            }
            return newText;
        }

        private string TextArea(string text, string placeholder, GUIStyle style, params GUILayoutOption[] options)
        {
            EditorGUI.BeginChangeCheck();
            string newText = EditorGUILayout.TextArea(text, style, options);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(window, "Changed Localization Window 2");
            }

            if (text != null && String.IsNullOrEmpty(text.Trim()))
            {
                GUIStyle placeHStyle = new GUIStyle();
                placeHStyle.fontSize = 100;
                placeHStyle.normal.textColor = Color.grey * 0.5F;

                var textRect = GUILayoutUtility.GetLastRect();
                var position = new Rect((textRect.width - 200) * 0.5f, (textRect.height - 200) * 0.5f, textRect.width, textRect.height);
                EditorGUI.LabelField(position, placeholder, placeHStyle);
            }
            return newText;
        }
        private void ClearTexts()
        {
            key = "";
            textArea = "";
            sprite = null;
            audioClip = null;
            GUI.FocusControl(null);
        }
    }
    #endregion
}