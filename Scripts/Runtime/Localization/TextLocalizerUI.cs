using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LS.Localiser.CSV;

namespace LS.Localiser.Runtime
{
    public class TextLocalizerUI : MonoBehaviour
    {

        #region Public Fields
        #endregion

        #region Private Fields
        [SerializeField]
        private LocalizedString localizedString = null;
        private TextMeshProUGUI textFieldMeshPro = null;
        private Text textField = null;
        public LocalizedString LocalisedString
        {
            get => localizedString;
            set => localizedString = value;
        }
        #endregion


        #region Accessors

        #endregion


        #region MonoBehaviour Methods

        void Start()
        {
            textField = GetComponent<Text>();
            textFieldMeshPro = GetComponent<TextMeshProUGUI>();

            Refresh();
        }
        #endregion


        #region Private Methods
        #endregion


        #region Public Methods
        public string ChangeKey(string _key)
        {
            localizedString.key = _key;
            Refresh();

            return localizedString.value;
        }

        public void Refresh()
        {
            if (textField)
            {
                textField.text = localizedString.value;
            }
            if (textFieldMeshPro)
            {
                textFieldMeshPro.text = localizedString.value;
            }
        }

#if UNITY_EDITOR
        public void RefreshEditor(string _text)
        {
            textField = GetComponent<Text>();
            textFieldMeshPro = GetComponent<TextMeshProUGUI>();

            if (textField)
            {
                textField.text = _text;
            }
            if (textFieldMeshPro)
            {
                textFieldMeshPro.text = _text;
            }
        }
#endif
        #endregion
    }
}
