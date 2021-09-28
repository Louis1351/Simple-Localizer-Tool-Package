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
        /// <summary>
        /// Get the text value from the dictionary.
        /// </summary>
        /// <returns>Returns a string</returns>
        public string textValue
        {
            get
            {
                return LocalizationSystem.GetLocalisedValue(key).text;
            }
        }
        /// <summary>
        /// Get the sprite value from the Asset Bundle.
        /// </summary>
        /// <returns>Returns a Sprite</returns>
        public Sprite spriteValue
        {
            get
            {
                Sprite sprite = null;
                string path = LocalizationSystem.GetLocalisedValue(key).spritePath;

                if (path != "")
                {
                    if (Application.isPlaying)
                    {
                        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "localizerbundle"));
                        sprite = localAssetBundle.LoadAsset<Sprite>(path);
                        localAssetBundle.Unload(false);
                    }
                    else
                    {
#if UNITY_EDITOR
                        sprite = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
#endif
                    }
                }

                return sprite;
            }
        }
        /// <summary>
        /// Get the AudioClip value from the Asset Bundle.
        /// </summary>
        /// <returns>Returns an AudioClip</returns>
        public AudioClip clipValue
        {
            get
            {
                AudioClip clip = null;
                string path = LocalizationSystem.GetLocalisedValue(key).clipPath;

                if (path != "")
                {
                    if (Application.isPlaying)
                    {
                        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "localizerbundle"));
                        clip = localAssetBundle.LoadAsset<AudioClip>(path);
                        localAssetBundle.Unload(false);
                    }
                    else
                    {
#if UNITY_EDITOR
                        clip = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
#endif
                    }
                }

                return clip;
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