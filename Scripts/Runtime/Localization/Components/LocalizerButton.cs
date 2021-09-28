using LS.Localiser.CSV;
using UnityEngine;
using UnityEngine.UI;

namespace LS.Localiser.Runtime.Components
{
    public class LocalizerButton : MonoBehaviour
    {
        private Button btn = null;
     
        void Start()
        {
            btn = transform.GetComponent<Button>();
            btn.onClick.AddListener(() => LocalizationSystem.ChangeToNextLanguage());
        }
    }
}
