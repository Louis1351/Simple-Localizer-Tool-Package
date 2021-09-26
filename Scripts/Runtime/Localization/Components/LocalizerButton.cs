using LS.Localiser.CSV;
using UnityEngine;
using UnityEngine.UI;

namespace LS.Localiser.Runtime
{
    public class LocalizerButton : MonoBehaviour
    {
        private Button btn = null;
        // Start is called before the first frame update
        void Start()
        {
            btn = transform.GetComponent<Button>();
            btn.onClick.AddListener(() => LocalizationSystem.ChangeToNextLanguage());
        }
    }
}
