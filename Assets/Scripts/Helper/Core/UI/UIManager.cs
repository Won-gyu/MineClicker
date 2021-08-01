using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace Helper
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField]
        private CanvasGroup uiCanvas;
        public CanvasGroup UICanvas
        {
            get
            {
                return uiCanvas;
            }
        }
    }
}