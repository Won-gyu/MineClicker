using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class GameLoader : MonoBehaviour
    {
        private void Awake()
        {
            LoadAssetBundle();
            LoadInGame();
            LoadAudioManager();
        }

        private void LoadAssetBundle()
        {
            AssetBundleManager.Instance.Initialize();
        }

        private void LoadInGame()
        {
            Instantiate(AssetBundleManager.Instance.LoadAsset<GameObject>("common", "In Game"));
        }

        private void LoadAudioManager()
        {
            Instantiate(AssetBundleManager.Instance.LoadAsset<GameObject>("common", "Audio Manager")).transform.SetParent(transform);
        }
    }
}
