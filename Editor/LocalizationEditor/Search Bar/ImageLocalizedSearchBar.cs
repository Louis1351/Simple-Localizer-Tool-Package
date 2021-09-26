using UnityEngine;
using UnityEditor;
using LS.Localiser.CSV;
using LS.Localiser.Utils;
using LS.Localiser.Runtime.Components;

namespace LS.Localiser.Editor
{
    [CustomEditor(typeof(ImageLocalizerUI))]
    [CanEditMultipleObjects]
    public class ImageLocalizedSearchBarEditor : LocalizedSearchBarEditor
    {

        #region Public Fields
        #endregion

        #region Private Fields
        private ImageLocalizerUI component = null;
        #endregion

        #region PropertyDrawer Methods

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            string key = component.GetKey();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Item Key : " + key);
            EditorGUILayout.EndHorizontal();

            FileUtils.ReadSettingsFile(out SystemLanguage language, out string format);

            LocalizationSystem.LocalizationItem item = LocalizationSystem.GetLocalisedValue(key, language);

            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(item.spritePath);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sprite : " + ((sprite != null) ? sprite.name : "NONE"));
            EditorGUILayout.EndHorizontal();

            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(item.clipPath);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Audio Clip : " + ((clip != null) ? clip.name : "NONE"));
            EditorGUILayout.EndHorizontal();

            component.RefreshEditor(sprite);

            if (GUILayout.Button("Find Key"))
            {
                if (SearchWindow == null)
                {
                    SearchWindow = ScriptableObject.CreateInstance<SearchWindow>();
                    SearchWindow.Init(this, GUILayoutUtility.GetLastRect().position);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            Key = serializedObject.FindProperty("localizedString").FindPropertyRelative("key");
            this.component = (ImageLocalizerUI)target;
            Component = this.component;
        }
        #endregion


        #region Private Methods



        #endregion


        #region Public Methods

        #endregion


    }
}