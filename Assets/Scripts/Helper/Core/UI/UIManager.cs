using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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