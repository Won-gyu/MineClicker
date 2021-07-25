using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Helper
{
    public class AssetBundleManager : MonoBehaviour
    {
        [SerializeField]
        private List<string> assetBundleNames;

        private Dictionary<string, AssetBundle> bundleDict;

        private void Awake()
        {
            LoadAssetBundles();
            var prefab = bundleDict["common"].LoadAsset<GameObject>("In Game");
            Instantiate(prefab);
        }

        private void LoadAssetBundles()
        {
            bundleDict = new Dictionary<string, AssetBundle>();
            for (int i = 0; i < assetBundleNames.Count; i++)
            {
                var loadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "AssetBundles", assetBundleNames[i]));
                if (loadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    return;
                }

                bundleDict.Add(assetBundleNames[i], loadedAssetBundle);
            }
        }
    }
}