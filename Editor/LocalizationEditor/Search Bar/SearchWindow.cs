using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LS.Localiser.CSV;
using LS.Localiser.Runtime;
using LS.Localiser.Utils;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Editor
{
    public class SearchWindow : EditorWindow
    {
        private Vector2 scrollpos = Vector2.zero;
        private Dictionary<string, LocalizationSystem.LocalizationItem> dictionary = null;
        private LocalizedSearchBarEditor parent = null;

        public void Init(LocalizedSearchBarEditor parent, Vector2 position)
        {
            SearchWindow window = this;
            window.titleContent = new GUIContent("Search key");

            this.parent = parent;

            Vector2 compos = GUIUtility.GUIToScreenPoint(position);
            Rect rect = new Rect(compos.x, compos.y - 500, EditorGUIUtility.currentViewWidth, 500.0f);
            window.position = rect;
            window.ShowPopup();
            // window.Show(/*rect, new Vector2(width, 500.0f)*/);
        }

        private void OnGUI()
        {
            // Close();
            GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));

            GUI.SetNextControlName("key");

            parent.Key.stringValue = GUILayout.TextField(parent.Key.stringValue, GUI.skin.FindStyle("ToolbarSeachTextField"));

            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                parent.Key.stringValue = "";
                // parent.Component.RefreshEditor(parent.Key.stringValue);
                //    EditorUtility.SetDirty(parent.Component);
                //    GUI.FocusControl(null);
            }

            GUILayout.EndHorizontal();

            FileUtils.ReadSettingsFile(out SystemLanguage language, out string format);

            if ((Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter))
            {
                LocalizationSystem.LocalizationItem item = LocalizationSystem.GetLocalisedValue(parent.Key.stringValue, language);

                TextLocalizerUI textUI = (parent.Component as TextLocalizerUI);
                ImageLocalizerUI imageUI = (parent.Component as ImageLocalizerUI);

                if (textUI)
                    textUI.RefreshEditor(item.text);

                if (imageUI)
                    imageUI.RefreshEditor(AssetDatabase.LoadAssetAtPath<Sprite>(item.spritePath));

                EditorUtility.SetDirty(parent.Component);
                GUI.FocusControl(null);
            }

            GUIStyle buttonStyle = new GUIStyle("ToolbarButton");
            buttonStyle.alignment = TextAnchor.MiddleLeft;
            buttonStyle.stretchHeight = true;

            EditorGUILayout.BeginVertical();
            scrollpos = EditorGUILayout.BeginScrollView(scrollpos, GUILayout.Height(500.0f));
            dictionary = LocalizationSystem.GetDictionary(language);
            if (dictionary != null && dictionary.Count > 0)
            {
                int nbKey = 0;
                foreach (KeyValuePair<string, LocalizationSystem.LocalizationItem> element in dictionary)
                {
                    if (element.Key.ToLower().Contains(parent.Key.stringValue.ToLower())
                    || element.Value.text.ToLower().Contains(parent.Key.stringValue.ToLower()))
                    {
                        EditorGUILayout.BeginHorizontal();
                        string value = Regex.Replace(element.Value.text, @"\t|\n|\r", "").Substring(0, Mathf.Min(element.Value.text.Length, 80));
                        GUI.backgroundColor = Color.white * ((nbKey % 2 == 0) ? 0.2f : 0.025f);
                        if (GUILayout.Button(element.Key + "\t\t" + value, buttonStyle, GUILayout.Height(25)))
                        {
                            parent.Key.stringValue = element.Key;

                            TextLocalizerUI textUI = (parent.Component as TextLocalizerUI);
                            ImageLocalizerUI imageUI = (parent.Component as ImageLocalizerUI);

                            if (textUI)
                            {
                                textUI.ChangeKey(element.Key);
                                textUI.RefreshEditor(element.Value.text);
                            }

                            if (imageUI)
                            {
                                imageUI.ChangeKey(element.Key);
                                imageUI.RefreshEditor(AssetDatabase.LoadAssetAtPath<Sprite>(element.Value.spritePath));
                            }

                            EditorUtility.SetDirty(parent.Component);
                            Close();
                            break;
                        }
                        EditorGUILayout.EndHorizontal();
                        nbKey++;
                    }
                    if (nbKey > 20)
                        break;
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "No keys found in " + language.ToString() + " language.", "ok");
                Close();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            //  position.width = EditorGUIUtility.currentViewWidth;
        }

        private void OnLostFocus()
        {
            Close();
        }
    }
}
