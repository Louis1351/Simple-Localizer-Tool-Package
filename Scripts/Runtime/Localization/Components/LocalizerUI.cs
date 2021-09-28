using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LS.Localiser.Runtime.Components
{
    public interface LocalizerUI
    {
        /// <summary>
        /// Change  the active key.
        /// </summary>
        /// <param name="key">key to pass.</param>
        /// <returns>Returns a key in string</returns>F
        string ChangeKey(string _key);
        /// <summary>
        /// Refreshs component.
        /// </summary>
        void Refresh();
        /// <summary>
        /// Get the active key.
        /// </summary>
        /// <returns>Returns a key in string</returns>
        string GetKey();
        /// <summary>
        ///  Get the Sprite links to the key.
        /// </summary>
        /// <returns>Returns a Sprite</returns>
        Sprite GetSprite();
        /// <summary>
        ///  Get the AudioClip links to the key.
        /// </summary>
        /// <returns>Returns a AudioClip</returns>
        AudioClip GetClip();
    }
}
