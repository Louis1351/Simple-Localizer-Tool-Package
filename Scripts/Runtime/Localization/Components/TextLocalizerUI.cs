using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LS.Localiser.CSV;

namespace LS.Localiser.Runtime.Components
{
    public class TextLocalizerUI : MonoBehaviour, LocalizerUI
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

            return localizedString.textValue;
        }

        public void Refresh()
        {
            if (textField)
            {
                textField.text = localizedString.textValue;
            }
            if (textFieldMeshPro)
            {
                textFieldMeshPro.text = localizedString.textValue;
            }
        }

        public string GetKey()
        {
            return localizedString.key;
        }
        public Sprite GetSprite()
        {
            return localizedString.spriteValue;
        }
        public AudioClip GetClip()
        {
            return localizedString.clipValue;
        }

#if UNITY_EDITOR
        public void RefreshEditor(string _text)
        {
            if (Application.isPlaying) return;

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
