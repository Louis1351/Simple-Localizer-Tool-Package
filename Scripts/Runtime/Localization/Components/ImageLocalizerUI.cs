using UnityEngine;
using UnityEngine.UI;
using LS.Localiser.CSV;

namespace LS.Localiser.Runtime
{
    public class ImageLocalizerUI : LocalizerUI
    {

        #region Public Fields
        #endregion

        #region Private Fields
        [SerializeField]
        private LocalizedString localizedString = null;
        private Image image = null;
        private SpriteRenderer spriteRnd = null;

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
            image = GetComponent<Image>();
            spriteRnd = GetComponent<SpriteRenderer>();

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
            if (image)
            {
                image.sprite = localizedString.spriteValue;
            }
            if (spriteRnd)
            {
                spriteRnd.sprite = localizedString.spriteValue;
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
        public void RefreshEditor(Sprite _sprite)
        {
            if (Application.isPlaying) return;

            image = GetComponent<Image>();
            spriteRnd = GetComponent<SpriteRenderer>();

            if (image)
            {
                image.sprite = _sprite;
            }
            if (spriteRnd)
            {
                spriteRnd.sprite = _sprite;
            }
        }
#endif
        #endregion
    }
}
