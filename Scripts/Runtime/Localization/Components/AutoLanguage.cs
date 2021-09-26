using System.Collections;
using System.Collections.Generic;
using LS.Localiser.CSV;
using UnityEngine;

namespace LS.Localiser.Runtime
{
    public class AutoLanguage : MonoBehaviour
    {
        private void Awake()
        {
            LocalizationSystem.AutoSelectLanguage();
        }
    }
}
