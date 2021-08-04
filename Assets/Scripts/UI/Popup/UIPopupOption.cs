using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace Mine
{
    public class UIPopupOption : Popup
    {
#if UNITY_EDITOR
        [Button]
        public void CloseTestPopup()
        {
            Close();
        }
#endif
    }
}