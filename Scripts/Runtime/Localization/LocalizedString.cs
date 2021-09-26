
using System.IO;
using UnityEngine;

namespace LS.Localiser.CSV
{
    [System.Serializable]
    public class LocalizedString
    {

        #region Public Fields
        public string key = "";
        #endregion


        #region Private Fields
        #endregion


        #region Accessors
        public string textValue
        {
            get
            {
                return LocalizationSystem.GetLocalisedValue(key).text;
            }
        }
        public Sprite spriteValue
        {
            get
            {
                AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "localizerbundle"));
                Sprite sprite = localAssetBundle.LoadAsset<Sprite>(LocalizationSystem.GetLocalisedValue(key).spritePath);
                localAssetBundle.Unload(false);
                return sprite;
                //  return LocalizationSystem.GetLocalisedValue(key).sprite;
            }
        }
        public AudioClip clipValue
        {
            get
            {
                AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "localizerbundle"));
                AudioClip clip = localAssetBundle.LoadAsset<AudioClip>(LocalizationSystem.GetLocalisedValue(key).clipPath);
                localAssetBundle.Unload(false);
                return clip;
                // return LocalizationSystem.GetLocalisedValue(key).clip;
            }
        }
        #endregion


        #region Private Methods
        #endregion


        #region Public Methods
        public LocalizedString(string key)
        {
            this.key = key;
        }

        public static implicit operator LocalizedString(string key)
        {
            return new LocalizedString(key);
        }
        #endregion
    }
}