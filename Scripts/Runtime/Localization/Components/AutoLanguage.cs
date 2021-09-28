using LS.Localiser.CSV;
using UnityEngine;

namespace LS.Localiser.Runtime.Components
{
    public class AutoLanguage : MonoBehaviour
    {
        private void Awake()
        {
            LocalizationSystem.AutoSelectLanguage();
        }
    }
}
