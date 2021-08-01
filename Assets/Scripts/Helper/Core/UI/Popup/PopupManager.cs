using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace Helper
{
    public interface IPopupManager
    {
        void Open(Popup popup);
        void Close(Popup popup);
        bool IsEmpty();
    }

    public class PopupManager : MonoSingleton<PopupManager>, IPopupManager
    {
        private Transform anchorSystemPopup;
        private Transform anchorContentPopup;

        private List<Popup> alivePopups = new List<Popup>();

        public int sortingOrderInterval = 10;
        public int currentSortingOrder
        {
            get { return alivePopups.Count * sortingOrderInterval + 1; }
        }

        // private Camera _uiCamera = null;
        // public Camera UICamera
        // {
        //     get
        //     {
        //         if (_uiCamera == null)
        //         {
        //             var camera = MainBlackboard.Get().GetValue<GameObject>("uiCamera");
        //             _uiCamera = camera.GetComponent<Camera>();
        //         }
                
        //         return _uiCamera;
        //     }
        // }

        public bool uiInteractableLock = false;
                
        [System.Serializable]
        public class UnityIntegerEvent : UnityEvent<Popup, int> {}

        public UnityIntegerEvent onActivate;

        public UnityIntegerEvent onInactivate;

        public void Open(Popup popup)
        {
            popup.UpdateSortingLayer(currentSortingOrder);

            Push(popup);
        }

        public void Close(Popup popup)
        {
            Pop();
        }

        public bool IsEmpty()
        {
            return alivePopups.Count <= 0;
        }

        private void Push(Popup popup)
        {
            if (!uiInteractableLock && alivePopups.Count <= 0) {
                UIManager.Instance.UICanvas.interactable = false;
            }

            onActivate.Invoke(popup, alivePopups.Count);

            alivePopups.Add(popup);
        }

        private void Pop()
        {
            if (alivePopups.Count <= 0)
                return;
                
            var popup = alivePopups[alivePopups.Count - 1];
            alivePopups.RemoveAt(alivePopups.Count - 1);

            onInactivate.Invoke(popup, alivePopups.Count);

            if (!uiInteractableLock && alivePopups.Count <= 0) {
                UIManager.Instance.UICanvas.interactable = true;
            }
        }

        public void Remove(Popup popup)
        {
            if (alivePopups.Count <= 0)
                return;

            for (int i = 0; i < alivePopups.Count; i++)
            {
                if (popup == alivePopups[i])
                {
                    alivePopups.RemoveAt(i);
                    break;
                }
            }

            onInactivate.Invoke(popup, alivePopups.Count);

            if (!uiInteractableLock && alivePopups.Count <= 0) {
                UIManager.Instance.UICanvas.interactable = true;
            }
        }

        public Popup Peek()
        {
            return alivePopups[alivePopups.Count-1];
        }

        // private void LateUpdate()
        // {
		// 	if (Keyboard.current != null)
        //     {
        //         if (Keyboard.current.escapeKey.wasReleasedThisFrame)
		// 		{
        //             if (IsEmpty())
        //                 return;
                    
        //             var popup = Peek();
        //             if (popup.enableBackButtonClose)
        //             {
        //                 popup.Close();

        //                 var popupAnimator = popup.GetComponent<Animator>();

        //                 if (popupAnimator != null)
        //                 {
        //                     popupAnimator.SetTrigger("Disappear");
        //                 }
        //                 else
        //                 {
        //                     GameObject.Destroy(popup.gameObject);
        //                 }
        //             }
        //         }
        //     }
        // }

// #if UNITY_EDITOR
//         public List<string> popupList;

//         [Button]
//         public void OpenAllPopups()
//         {
//             var popupCanvas = GameObject.Find("Popup Canvas");

//             foreach(var popupName in popupList)
//             {
//                 var bundleName = "common";
//                 var prefab = (GameObject)AssetBundleManager.LoadAsset<GameObject>(bundleName, popupName);

//                 GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
//                 go.transform.SetParent(popupCanvas.transform, false);
//                 go.transform.SetAsLastSibling();
//                 go.SetActive(true);
//             }
//         }
// #endif
    }
}