using UnityEngine;
using UnityEditor;
using LS.Localiser.Runtime;
using LS.Localiser.CSV;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LS.Localiser.Utils;

namespace LS.Localiser.Editor
{
    [CustomEditor(typeof(TextLocalizerUI))]
    [CanEditMultipleObjects]
    public class LocalizedSearchBarEditor : UnityEditor.Editor
    {

        #region Public Fields
        #endregion

        #region Private Fields
        private SerializedProperty key = null;
        private TextLocalizerUI component = null;
        private SearchWindow searchWindow = null;


        #endregion

        #region Accessors
        public SerializedProperty Key { get => key; set => key = value; }
        public TextLocalizerUI Component { get => component; set => component = value; }
        #endregion

        #region PropertyDrawer Methods

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
          /*  GUILayout.BeginHorizontal(GUI.skin.FindStyle("Toolbar"));

            GUI.SetNextControlName("key");
            key.stringValue = GUILayout.TextField(key.stringValue, GUI.skin.FindStyle("ToolbarSeachTextField"));

            if (GUILayout.Button("", GUI.skin.FindStyle("ToolbarSeachCancelButton")))
            {
                key.stringValue = "";
                component.RefreshEditor(key.stringValue);
                EditorUtility.SetDirty(target);
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();*/

            if (GUILayout.Button("Find Key")/*key.stringValue != "" && GUI.GetNameOfFocusedControl() == "key"*/)
            {
                if (searchWindow == null)
                {
                    searchWindow = ScriptableObject.CreateInstance<SearchWindow>();
                    searchWindow.Init(this, GUILayoutUtility.GetLastRect().position);
                }
            }

      /*      if (GUI.GetNameOfFocusedControl() == "key")
            {
                if (searchWindow != null)
                {
                    searchWindow.Repaint();
                }

            }*/
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            key = serializedObject.FindProperty("localizedString").FindPropertyRelative("key");
            component = (TextLocalizerUI)target;
        }
        #endregion


        #region Private Methods



        #endregion


        #region Public Methods

        #endregion


    }
}