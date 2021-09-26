using UnityEditor;
using LS.Localiser.Runtime.Components;

namespace LS.Localiser.Editor
{
    [CustomEditor(typeof(LocalizerUI))]
    public class LocalizedSearchBarEditor : UnityEditor.Editor
    {

        #region Public Fields
        #endregion

        #region Private Fields
        private SerializedProperty key = null;
        private LocalizerUI component = null;
        private SearchWindow searchWindow = null;


        #endregion

        #region Accessors
        public SerializedProperty Key { get => key; set => key = value; }
        public SearchWindow SearchWindow { get => searchWindow; set => searchWindow = value; }
        public LocalizerUI Component { get => component; set => component = value; }

        #endregion

        #region PropertyDrawer Methods
        #endregion


        #region Private Methods



        #endregion


        #region Public Methods

        #endregion


    }
}