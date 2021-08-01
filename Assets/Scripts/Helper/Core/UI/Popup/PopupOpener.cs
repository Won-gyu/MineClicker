using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Helper
{
    public class PopupOpener : MonoSingleton<PopupOpener>
    {
        public void OpenPopup<T>(string popupPrefabName, T value)
        {
            GameObject prefab = AssetBundleManager.Instance.LoadAsset<GameObject>("common", popupPrefabName);
            GameObject popup = Instantiate(prefab);
            popup.transform.parent = transform;
            popup.name = prefab.name;
            popup.GetComponent<Popup>().Open();
        }
        
        public void OpenPopup(string popupPrefabName)
        {
            OpenPopup<object>(popupPrefabName, null);
        }
    }
}