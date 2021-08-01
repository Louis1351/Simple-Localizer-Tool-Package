using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace LS.Localiser.Editor
{
    [System.Serializable]
    public class SpreadsheetTab
    {
        #region Private Fields
        [SerializeField]
        private TextLocalizerEditWindow window = null;
        [SerializeField]
        private MultiColumnHeaderCustom multiColumn = null;

        #endregion
        public SpreadsheetTab(TextLocalizerEditWindow _window)
        {
            window = _window;
            multiColumn = new MultiColumnHeaderCustom(_window);

        }

        #region Public Methods
        public void OnGUI()
        {
            multiColumn.OnGUI();
        }

        public void Refresh()
        {
            multiColumn.RefreshDictionaries();
        }
        #endregion
    }
}
