using System.Collections;
using System.Collections.Generic;
using System.Threading;
using LS.Localiser.CSV;
using LS.Localiser.Utils;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace LS.Localiser.Editor
{
    [System.Serializable]
    public class TranslationTab
    {
        #region Private Fields
        [SerializeField]
        private TextLocalizerEditWindow window = null;
        private int popupKey = 0;
        private int popupLanguage1 = 0;
        private int popupLanguage2 = 0;
        private Dictionary<string, string> dictionary;
        #endregion

        public TranslationTab(TextLocalizerEditWindow _window)
        {
            window = _window;
        }
        #region Public Methods
        public void OnGUI()
        {
            string[] keys = null;
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;
            style.fontSize = 15;
            style.stretchHeight = true;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Source language", GUILayout.ExpandWidth(false));
            popupLanguage1 = EditorGUILayout.Popup(popupLanguage1, window.LanguageTabNames, GUILayout.Width(150), GUILayout.Height(30));

            SystemLanguage l1 = (SystemLanguage)popupLanguage1;
            dictionary = LocalizationSystem.GetDictionary(l1);

            if (dictionary != null)
            {
                keys = new string[dictionary.Count + 1];
                keys[0] = "All";
                int i = 1;

                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    keys[i] = entry.Key;
                    i++;
                }

                popupKey = EditorGUILayout.Popup(popupKey, keys, GUILayout.Width(150), GUILayout.Height(30));
            }
            EditorGUILayout.EndHorizontal();

            if (keys != null)
            {
                float estimateTime = (((popupKey == 0) ? (keys.Length - 1) : 1) * 3.0f);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Target language", GUILayout.ExpandWidth(false));
                popupLanguage2 = EditorGUILayout.Popup(popupLanguage2, window.LanguageTabNames, GUILayout.Width(150), GUILayout.Height(30));
                EditorGUILayout.EndHorizontal();

                SystemLanguage l2 = (SystemLanguage)popupLanguage2;

                if (l1 != l2)
                {
                    EditorGUILayout.EndVertical();
                    GUILayout.FlexibleSpace();

                    EditorGUILayout.HelpBox("Estimate : " + estimateTime + " sec", MessageType.Info);
                    if (GUILayout.Button("Translate", GUILayout.Height(30)))
                    {

                        if (popupKey == 0)
                        {
                            FileUtils.ClearContentLanguageFile(l2.ToString());
                            float t = 0.0f;
                            foreach (KeyValuePair<string, string> entry in dictionary)
                            {
                                t += 3.0f;
                                EditorUtility.DisplayProgressBar("Translate Progress Bar", "Translating " + entry.Key + "...", t / estimateTime);
                                Thread.Sleep(3000);

                                EditorCoroutineUtility.StartCoroutine(Translate.Process(l1, l2, entry.Value, delegate (string value)
                                {
                                    LocalizationSystem.Add(entry.Key, value, l2);
                                }), this);
                            }
                        }
                        else
                        {
                            float t = 0.0f;
                            t += 3.0f;
                            EditorUtility.DisplayProgressBar("Translate Progress Bar", "Translating...", t / estimateTime);
                            Thread.Sleep(3000);

                            EditorCoroutineUtility.StartCoroutine(Translate.Process(l1, l2, dictionary[keys[popupKey]], delegate (string value)
                            {
                                if (dictionary.ContainsKey(keys[popupKey]))
                                {
                                    LocalizationSystem.Replace(keys[popupKey], value, l2);
                                }
                                else
                                {
                                    LocalizationSystem.Add(keys[popupKey], value, l2);
                                }
                            }), this);
                        }
                        EditorUtility.ClearProgressBar();
                    }
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.HelpBox("Cannot translate the same language.", MessageType.Info);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Zero key found.", MessageType.Info);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
    }
    #endregion
}
